using System;
using System.Linq;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Utilities;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models;
using BrokenWheel.Control.Models.InputData;
using BrokenWheel.Control.Events;

namespace BrokenWheel.Control.Implementations
{
    public class RPGInputTracker : IRPGInputTracker
    {
        private readonly ILogger _logger;
        private readonly IDictionary<RPGInput, ButtonInputDataObject> _inputTrackers;
        private readonly IEventSubject<ButtonInputEvent> _buttonSubject;
        private readonly IEventSubject<MoveInputEvent> _moveSubject;
        private readonly IEventSubject<LookInputEvent> _lookSubject;

        private readonly IDictionary<RPGInput, ButtonInputDataObject> _inputObjectByType = FullEnumDictionary();
        private readonly IList<ButtonInputDataObject> _activeInputs = new List<ButtonInputDataObject>();
        private readonly MoveInputDataObject _moveInput = new MoveInputDataObject();
        private readonly LookInputDataObject _lookInput = new LookInputDataObject();

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
        }

        private static IDictionary<RPGInput, ButtonInputDataObject> FullEnumDictionary()
        {
            return EnumUtil.GetAllEnumValues<RPGInput>()
                .ToDictionary(_ => _, _ => new ButtonInputDataObject(_));
        }

        /// <inheritdoc/>
        public void TrackButtonInput(RPGInput input, bool isPressed) // TODO: add support for custom input
        {
            _logger.LogCategory("RPG Input", $"{input}-{(isPressed ? "pressed" : "released")}");
            var tracker = _inputTrackers[input];
            SwitchPressType(isPressed, tracker);
            AddOrRemoveActiveTracker(tracker);
        }

        private static void SwitchPressType(bool isPressed, ButtonInputDataObject tracker)
        {
            switch (tracker.PressType)
            {
                case PressType.Clicked:
                    tracker.PressType = isPressed ? PressType.Held : PressType.Released;
                    break;
                case PressType.Released:
                    tracker.PressType = isPressed ? PressType.Clicked : PressType.NotHeld;
                    break;
                case PressType.Held:
                    tracker.PressType = isPressed ? PressType.Held : PressType.Released;
                    break;
                case PressType.NotHeld:
                default:
                    tracker.PressType = isPressed ? PressType.Clicked : PressType.NotHeld;
                    break;
            }
        }

        private void AddOrRemoveActiveTracker(ButtonInputDataObject tracker)
        {
            if (tracker.PressType == PressType.NotHeld)
            {
                tracker.HeldTime = 0;
                _activeInputs.Remove(tracker);
            }
            else
            {
                _activeInputs.Add(tracker);
            }
        }

        /// <inheritdoc/>
        public void TrackMoveInput(double vX, double vY)
        {
            _moveInput.VelocityX = vX;
            _moveInput.VelocityY = vY;
        }

        /// <inheritdoc/>
        public void TrackLookInput(double vX, double vY, int posX, int posY)
        {
            _lookInput.VelocityX = vX;
            _lookInput.VelocityY = vY;
            _lookInput.PositionX = posX;
            _lookInput.PositionY = posY;
        }

        /// <inheritdoc/>
        public void ProcessInputs(double delta)
        {
            foreach (var tracker in _activeInputs)
                EmitActiveButtonInput(delta, tracker);
            EmitMoveInput(delta);
            EmitLookInput(delta);
        }

        private void EmitActiveButtonInput(double delta, ButtonInputDataObject tracker)
        {
            if (tracker.PressType != PressType.NotHeld)
                tracker.HeldTime += delta;
            var inputData = tracker.GetTick(delta);
            var buttonEvent = new ButtonInputEvent(this, inputData);
            _buttonSubject.Emit(buttonEvent);
        }

        private void EmitMoveInput(double delta)
        {
            _moveInput.HeldTime = _moveInput.IsStopped() ? 0 : _moveInput.HeldTime + delta;
            var moveData = _moveInput.GetTick(delta);
            var moveEvent = new MoveInputEvent(this, moveData);
            _moveSubject.Emit(moveEvent);
        }

        private void EmitLookInput(double delta)
        {
            _lookInput.HeldTime = _lookInput.IsStopped() ? 0 : _lookInput.HeldTime + delta;
            var lookData = _lookInput.GetTick(delta);
            var lookEvent = new LookInputEvent(this, lookData);
            _lookSubject.Emit(lookEvent);
        }
    }
}
