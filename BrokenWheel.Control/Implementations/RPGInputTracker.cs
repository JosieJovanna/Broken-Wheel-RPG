using System;
using System.Linq;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models;
using BrokenWheel.Core.Utilities;
using BrokenWheel.Control.Models.InputData;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Control.Events;

namespace BrokenWheel.Control.Implementations
{
    public class RPGInputTracker : IRPGInputTracker
    {
        private readonly ILogger _logger;
        private readonly IDictionary<RPGInput, ButtonInputDataObject> _inputTrackers;
        private readonly IList<ButtonInputDataObject> _activeInputs = new List<ButtonInputDataObject>();
        private readonly IEventSubject<ButtonInputEvent> _buttonSubject;
        private readonly IEventSubject<MoveInputEvent> _moveSubject;
        private readonly IEventSubject<LookInputEvent> _lookSubject;

        public RPGInputTracker(
            ILogger logger,
            IEventAggregator eventAggregator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _inputTrackers = EnumUtils.GetAllEnumValues<RPGInput>()
                .ToDictionary(_ => _, _ => new ButtonInputDataObject(_));

            if (eventAggregator == null)
                throw new ArgumentNullException(nameof(eventAggregator));
            _buttonSubject = eventAggregator.GetSubject<ButtonInputEvent>();
            _moveSubject = eventAggregator.GetSubject<MoveInputEvent>();
            _lookSubject = eventAggregator.GetSubject<LookInputEvent>();
        }

        public void TrackButtonInput(RPGInput input, bool isPressed)
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

        public void ProcessInputs(double delta)
        {
            foreach (var tracker in _activeInputs)
            {
                var inputData = tracker.GetTick(delta);

            }
        }

        public void TrackLookInput(LookInputData lookInput)
        {
            throw new NotImplementedException();
        }

        public void TrackMoveInput(double vX, double vY)
        {
            throw new NotImplementedException();
        }

        public void TrackLookInput(double vX, double vY, int posX, int posY)
        {
            throw new NotImplementedException();
        }
    }
}
