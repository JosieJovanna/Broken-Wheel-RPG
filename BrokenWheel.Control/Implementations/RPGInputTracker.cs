using System;
using System.Linq;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Control.Implementations
{
    public class RPGInputTracker : IRPGInputTracker
    {
        private readonly ILogger _logger;
        private readonly IDictionary<RPGInput, InputDataObject> _inputTrackers;
        private readonly IList<InputDataObject> _activeInputs = new List<InputDataObject>();

        public RPGInputTracker(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _inputTrackers = EnumUtils.GetAllEnumValues<RPGInput>()
                .ToDictionary(_ => _, _ => new InputDataObject(_));
        }

        public void RegisterInput(RPGInput input, bool isPressed)
        {
            var tracker = _inputTrackers[input];
            SwitchPressType(isPressed, tracker);
            AddOrRemoveActiveTracker(tracker);
        }

        private static void SwitchPressType(bool isPressed, InputDataObject tracker)
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

        private void AddOrRemoveActiveTracker(InputDataObject tracker)
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
    }
}
