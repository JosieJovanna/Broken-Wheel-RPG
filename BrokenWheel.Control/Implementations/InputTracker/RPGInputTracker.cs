﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Core.Utilities;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Events;
using BrokenWheel.Control.Extensions;
using BrokenWheel.UI.Settings;
using BrokenWheel.Core.Events;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    public class RPGInputTracker
        : IRPGInputTracker
        , IEventHandler<GameModeUpdateEvent>
    {
        private readonly ILogger _logger;
        private readonly DisplaySettings _displaySettings;

        private readonly IEventSubject<ButtonInputEvent> @_buttonSubject;
        private readonly IEventSubject<MoveInputEvent> @_moveSubject;
        private readonly IEventSubject<LookInputEvent> @_lookSubject;
        private readonly IEventSubject<CursorInputEvent> @_cursorSubject;
        private readonly IEventObservable<GameModeUpdateEvent> @_gameMode;

        private readonly IDictionary<RPGInput, ButtonInputDataTracker> _buttonTrackersByInput;
        private readonly IList<ButtonInputDataTracker> _activeInputs = new List<ButtonInputDataTracker>();
        private readonly IList<ButtonInputDataTracker> _nonUiInputs;
        private readonly IList<ButtonInputDataTracker> _uiInputs;
        private readonly LookCursorInputDataTracker _lookCursorTracker = new LookCursorInputDataTracker();
        private readonly MoveInputDataTracker _moveTracker = new MoveInputDataTracker();

        /// <summary>
        /// The default implementation of <see cref="IRPGInputTracker"/>.
        /// Uses a dictionary to track button input, with lists also referencing those trackers, 
        /// </summary>
        /// <exception cref="ArgumentNullException"> If the aggregator or logger are null. </exception>
        public RPGInputTracker(
            ILogger logger,
            DisplaySettings displaySettings,
            IEventAggregator eventAggregator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _displaySettings = displaySettings ?? throw new ArgumentNullException(nameof(displaySettings));
            // subjects
            if (eventAggregator == null)
                throw new ArgumentNullException(nameof(eventAggregator));
            @_buttonSubject = eventAggregator.GetSubject<ButtonInputEvent>();
            @_moveSubject = eventAggregator.GetSubject<MoveInputEvent>();
            @_lookSubject = eventAggregator.GetSubject<LookInputEvent>();
            @_cursorSubject = eventAggregator.GetSubject<CursorInputEvent>();
            @_gameMode = eventAggregator.GetObservable<GameModeUpdateEvent>();
            // trackers
            _buttonTrackersByInput = EnumUtil.GetAllEnumValues<RPGInput>().ToDictionary(_ => _, _ => new ButtonInputDataTracker(_));
            _nonUiInputs = _buttonTrackersByInput.Values.Where(_ => !_.Input.IsUIInput()).ToList();
            _uiInputs = _buttonTrackersByInput.Values.Where(_ => _.Input.IsUIInput()).ToList();
        }

        /// <summary>
        /// Handles <see cref="GameModeUpdateEvent"/>s to properly handle UI vs. Gameplay input.
        /// </summary>
        public void HandleEvent(GameModeUpdateEvent gameEvent)
        {
            var isUI = gameEvent.IsUI;
            _logger.LogCategory("Input", $"Is UI = {isUI}");
            PauseInputTrackingForNonCurrentInputType(isUI); // TODO: active input swapping?
        }

        private void PauseInputTrackingForNonCurrentInputType(bool isUI)
        {
            foreach (var buttonTracker in isUI ? _uiInputs : _nonUiInputs)
                buttonTracker.Reset();
            if (isUI)
                _moveTracker.Pause();
            else
                _moveTracker.Resume();
        }

        #region Input Tracking

        /// <inheritdoc/>
        public void TrackMoveInput(double vX, double vY)
        {
            if (_gameMode.Current.IsUI)
                return;
            _moveTracker.VelocityX = vX;
            _moveTracker.VelocityY = vY;
            _moveTracker.SetIsStopped(vX == 0 && vY == 0);
        }

        /// <inheritdoc/>
        public void TrackCursorLookInput(double vX, double vY, int posX, int posY)
        {
            (_lookCursorTracker.VelocityX, _lookCursorTracker.VelocityY) = (vX, vY);
            (_lookCursorTracker.PositionX, _lookCursorTracker.PositionY) = (posX, posY);
            _lookCursorTracker.SetIsStopped(vX == 0 && vY == 0);
        }

        /// <inheritdoc/>
        public void TrackButtonInput(RPGInput input, bool isPressed) // TODO: add support for custom input
        {
            if (!input.IsDebugInput() && (_gameMode.Current.IsUI ^ input.IsUIInput()))
                return;
            var tracker = _buttonTrackersByInput[input];
            tracker.PressThisTick(GetNextPressType(isPressed, tracker.PressType));
            SetTrackerActiveIfClicked(tracker);
        }

        private static PressType GetNextPressType(bool isPressed, PressType pressType)
        {
            switch (pressType)
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

        private void SetTrackerActiveIfClicked(ButtonInputDataTracker tracker)
        {
            if (tracker.PressType != PressType.Clicked)
                return;
            _activeInputs.Add(tracker);
            tracker.HeldTimer.Restart();
        }

        #endregion

        #region Input Processing

        /// <inheritdoc/>
        public void ProcessInputs(double delta)
        {
            ProcessLookCursorInput(delta);
            ProcessMoveInput(delta);
            foreach (var tracker in _activeInputs.ToArray())
                ProcessActiveButtonInput(delta, tracker);
        }

        /// <summary>
        /// Emits a cursor event when in UI, or a look event when in gameplay.
        /// </summary>
        private void ProcessLookCursorInput(double delta)
        {
            if (!_lookCursorTracker.IsDeadInput())
                EmitLookOrCursorEvent(delta);
            _lookCursorTracker.Tick();
        }

        private void EmitLookOrCursorEvent(double delta)
        {
            if (_gameMode.Current.IsUI)
                @_cursorSubject.Emit(_lookCursorTracker.GetCursorEvent(_displaySettings.UIScale));
            else
                @_lookSubject.Emit(_lookCursorTracker.GetEvent(delta));
        }

        /// <summary>
        /// Emits move events unless movement has been zero for two ticks.
        /// </summary>
        private void ProcessMoveInput(double delta)
        {
            if (!_moveTracker.IsDeadInput() && !_gameMode.Current.IsUI)
                @_moveSubject.Emit(_moveTracker.GetEvent(delta));
            _moveTracker.Tick();
        }

        /// <summary>
        /// Emits button events when button is clicked, held, or just released.
        /// </summary>
        private void ProcessActiveButtonInput(double delta, ButtonInputDataTracker tracker)
        {
            if (!tracker.IsInputThisTick)
                UpdateButtonPressType(tracker);
            if (tracker.PressType != PressType.NotHeld)
                EmitButtonInput(delta, tracker); // TODO: check logic on tracking inputs over game mode changes
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
            @_buttonSubject.Emit(tracker.GetEvent(delta));
        }

        #endregion
    }
}
