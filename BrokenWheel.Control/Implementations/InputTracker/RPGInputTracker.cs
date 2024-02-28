﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Core.Utilities;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Events;
using BrokenWheel.Control.Extensions;
using System.Diagnostics;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    public class RPGInputTracker : IRPGInputTracker, IEventHandler<GameModeUpdateEvent>
    {
        private readonly ILogger _logger;
        private readonly IEventSubject<ButtonInputEvent> _buttonSubject;
        private readonly IEventSubject<MoveInputEvent> _moveSubject;
        private readonly IEventSubject<LookInputEvent> _lookSubject;
        private readonly IEventSubject<CursorInputEvent> _cursorSubject;
        private readonly MoveInputDataTracker _moveTracker;
        private readonly LookInputDataTracker _lookTracker;

        private readonly IDictionary<RPGInput, ButtonInputDataTracker> _buttonTrackersByInput;
        private readonly IList<ButtonInputDataTracker> _activeInputs;
        private readonly IList<ButtonInputDataTracker> _nonUiInputs;
        private readonly IList<ButtonInputDataTracker> _uiInputs;

        private bool _isUI = DebugConstants.DOES_GAME_START_IN_MENU;

        public RPGInputTracker(
            ILogger logger,
            IEventAggregator eventAggregator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (eventAggregator == null)
                throw new ArgumentNullException(nameof(eventAggregator));
            _buttonSubject = eventAggregator.GetSubject<ButtonInputEvent>();
            _moveSubject = eventAggregator.GetSubject<MoveInputEvent>();
            _lookSubject = eventAggregator.GetSubject<LookInputEvent>();
            _cursorSubject = eventAggregator.GetSubject<CursorInputEvent>();
            _moveTracker = new MoveInputDataTracker(this);
            _lookTracker = new LookInputDataTracker(this);
            eventAggregator.Subscribe<GameModeUpdateEvent>(HandleEvent);

            // trackers
            _buttonTrackersByInput = EnumUtil.GetAllEnumValues<RPGInput>().ToDictionary(_ => _, _ => new ButtonInputDataTracker(_));
            _activeInputs = new List<ButtonInputDataTracker>();
            _nonUiInputs = _buttonTrackersByInput.Values.Where(_ => !_.Input.IsUIInput()).ToList();
            _uiInputs = _buttonTrackersByInput.Values.Where(_ => _.Input.IsUIInput()).ToList();
        }

        public void HandleEvent(GameModeUpdateEvent gameEvent)
        {
            _isUI = gameEvent.GameMode != GameMode.Gameplay;
            _logger.LogCategory("Input", $"Is UI = {_isUI}");
            PauseInputTrackingForNonCurrentInputType();
        }

        private void PauseInputTrackingForNonCurrentInputType()
        {
            foreach (var buttonTracker in _isUI ? _uiInputs : _nonUiInputs)
                buttonTracker.Reset();
            if (_isUI)
                _moveTracker.Pause();
            else
                _moveTracker.Resume();
        }

        /// <inheritdoc/>
        public void TrackButtonInput(RPGInput input, bool isPressed) // TODO: add support for custom input
        {
            if (!input.IsDebugInput() && !IsInputAllowed(input))
                return;
            var tracker = _buttonTrackersByInput[input];
            tracker.PressType = SwitchPressType(isPressed, tracker);
            tracker.IsInputThisTick = true;
            if (tracker.PressType == PressType.Clicked)
                StartTrackingButtonInput(tracker);
        }

        private bool IsInputAllowed(RPGInput input)
        {
            var isMismatch = _isUI ^ input.IsUIInput();
            return !isMismatch;
        }

        private void StartTrackingButtonInput(ButtonInputDataTracker tracker)
        {
            _activeInputs.Add(tracker);
            tracker.HeldTimer.Restart();
        }

        private static PressType SwitchPressType(bool isPressed, ButtonInputDataTracker tracker)
        {
            switch (tracker.PressType)
            {
                case PressType.Clicked:
                    return isPressed ? PressType.Held : PressType.Released;
                case PressType.Released:
                    return isPressed ? PressType.Clicked : PressType.NotHeld;
                case PressType.Held:
                    return isPressed ? PressType.Held : PressType.Released;
                case PressType.NotHeld:
                default:
                    return isPressed ? PressType.Clicked : PressType.NotHeld;
            }
        }

        /// <inheritdoc/>
        public void TrackMoveInput(double vX, double vY)
        {
            if (_isUI)
                return;
            _moveTracker.VelocityX = vX;
            _moveTracker.VelocityY = vY;
            _moveTracker.SetIsStopped(vX == 0 && vY == 0);
        }

        /// <inheritdoc/>
        public void TrackLookInput(double vX, double vY, int posX, int posY)
        {
            (_lookTracker.VelocityX, _lookTracker.VelocityY) = (vX, vY);
            (_lookTracker.PositionX, _lookTracker.PositionY) = (posX, posY);
            _lookTracker.SetIsStopped(vX == 0 && vY == 0);
        }

        /// <inheritdoc/>
        public void ProcessInputs(double delta)
        {
            ProcessLookInput(delta);
            ProcessMoveInput(delta);
            foreach (var tracker in _activeInputs.ToArray())
                ProcessActiveButtonInput(delta, tracker);
        }

        private void ProcessLookInput(double delta)
        {
            if (!_lookTracker.IsDeadInput())
            {
                if (_isUI)
                    _cursorSubject.Emit(_lookTracker.GetCursorEvent(this));
                else
                    _lookTracker.EmitEvent(_lookSubject, delta);
            }
            _lookTracker.Tick();
        }

        private void ProcessMoveInput(double delta)
        {
            if (!_moveTracker.IsDeadInput() && !_isUI)
                _moveTracker.EmitEvent(_moveSubject, delta);
            _moveTracker.Tick();
        }

        private void ProcessActiveButtonInput(double delta, ButtonInputDataTracker tracker)
        {
            if (!tracker.IsInputThisTick)
                UpdateButtonPressType(tracker);
            if (tracker.PressType != PressType.NotHeld)
                EmitButtonInput(delta, tracker);
            tracker.IsInputThisTick = false;
        }

        private void UpdateButtonPressType(ButtonInputDataTracker tracker)
        {
            if (tracker.PressType == PressType.Clicked)
                tracker.PressType = PressType.Held;
            if (tracker.PressType == PressType.Released)
                ReleaseButtonInput(tracker);
        }

        private void ReleaseButtonInput(ButtonInputDataTracker tracker)
        {
            tracker.PressType = PressType.NotHeld;
            tracker.HeldTimer.Reset();
            _activeInputs.Remove(tracker);
        }

        private void EmitButtonInput(double delta, ButtonInputDataTracker tracker)
        {
            var inputData = tracker.GetData(delta);
            var buttonEvent = new ButtonInputEvent(this, inputData);
            _buttonSubject.Emit(buttonEvent);
        }
    }
}
