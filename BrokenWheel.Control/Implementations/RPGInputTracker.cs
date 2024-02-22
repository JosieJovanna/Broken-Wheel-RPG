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
            var tracker = _inputObjectByType[input];
            tracker.PressType = SwitchPressType(isPressed, tracker);
            tracker.IsInputThisTick = true;
            if (tracker.PressType == PressType.Clicked)
                _activeInputs.Add(tracker);
        }

        private static PressType SwitchPressType(bool isPressed, ButtonInputDataObject tracker)
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
            foreach (var tracker in _activeInputs.ToArray())
                ProcessActiveButtonInput(delta, tracker);
            ProcessMoveInput(delta);
            ProcessLookInput(delta);
        }

        private void ProcessActiveButtonInput(double delta, ButtonInputDataObject tracker)
        {
            if (!tracker.IsInputThisTick)
                TickActiveButtonInput(delta, tracker);
            if (tracker.PressType != PressType.NotHeld)
                EmitButtonInput(delta, tracker);
            tracker.IsInputThisTick = false;
        }

        private void TickActiveButtonInput(double delta, ButtonInputDataObject tracker)
        {
            if (tracker.PressType == PressType.Clicked)
                tracker.PressType = PressType.Held;
            if (tracker.PressType == PressType.Released)
                EndButtonInput(tracker);
            else
                tracker.HeldTime += delta;
        }

        private void EndButtonInput(ButtonInputDataObject tracker)
        {
            tracker.PressType = PressType.NotHeld;
            tracker.HeldTime = 0;
            _activeInputs.Remove(tracker);
        }

        private void EmitButtonInput(double delta, ButtonInputDataObject tracker)
        {
            var inputData = tracker.GetTick(delta);
            var buttonEvent = new ButtonInputEvent(this, inputData);
            _buttonSubject.Emit(buttonEvent);
        }

        private void ProcessMoveInput(double delta)
        {
            _moveInput.HeldTime = _moveInput.IsStopped() ? 0 : _moveInput.HeldTime + delta;
            var moveData = _moveInput.GetTick(delta);
            var moveEvent = new MoveInputEvent(this, moveData);
            _moveSubject.Emit(moveEvent);
        }

        private void ProcessLookInput(double delta)
        {
            _lookInput.HeldTime = _lookInput.IsStopped() ? 0 : _lookInput.HeldTime + delta;
            var lookData = _lookInput.GetTick(delta);
            var lookEvent = new LookInputEvent(this, lookData);
            _lookSubject.Emit(lookEvent);
        }
    }
}
