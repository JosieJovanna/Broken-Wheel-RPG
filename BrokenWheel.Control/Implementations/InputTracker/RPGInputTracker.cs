using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Events;
using BrokenWheel.Control.Extensions;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Control.Implementations.InputTracker
{
    public class RPGInputTracker : IRPGInputTracker, IEventHandler<GameModeUpdateEvent>
    {
        private readonly IEventSubject<ButtonInputEvent> _buttonSubject;
        private readonly IEventSubject<MoveInputEvent> _moveSubject;
        private readonly IEventSubject<LookInputEvent> _lookSubject;
        private readonly MoveInputDataTracker _moveTracker;
        private readonly LookInputDataTracker _lookTracker;

        private readonly IDictionary<RPGInput, ButtonInputDataTracker> _buttonTrackersByInput = FullEnumDictionary();
        private readonly IList<ButtonInputDataTracker> _activeInputs = new List<ButtonInputDataTracker>();

        private bool _isUI = false; // TODO: change to true once game starts in menu

        public RPGInputTracker(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
                throw new ArgumentNullException(nameof(eventAggregator));
            _buttonSubject = eventAggregator.GetSubject<ButtonInputEvent>();
            _moveSubject = eventAggregator.GetSubject<MoveInputEvent>();
            _lookSubject = eventAggregator.GetSubject<LookInputEvent>();
            _moveTracker = new MoveInputDataTracker(this);
            _lookTracker = new LookInputDataTracker(this);
        }

        private static IDictionary<RPGInput, ButtonInputDataTracker> FullEnumDictionary()
        {
            return EnumUtil.GetAllEnumValues<RPGInput>()
                .ToDictionary(_ => _, _ => new ButtonInputDataTracker(_));
        }

        public void HandleEvent(GameModeUpdateEvent gameEvent)
        {
            _isUI = gameEvent.GameMode != GameMode.Gameplay;
            // TODO: pause all timers for the inappropriate input type and when switching back emit released event...
        }

        /// <inheritdoc/>
        public void TrackButtonInput(RPGInput input, bool isPressed) // TODO: add support for custom input
        {
            var tracker = _buttonTrackersByInput[input];
            tracker.PressType = SwitchPressType(isPressed, tracker);
            tracker.IsInputThisTick = true;
            if (tracker.PressType == PressType.Clicked)
                StartTrackingButtonInput(tracker);
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
            foreach (var tracker in _activeInputs.ToArray())
                ProcessActiveButtonInput(delta, tracker);
            ProcessInput(delta, _lookTracker, _lookSubject);
            ProcessInput(delta, _moveTracker, _moveSubject);
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
            /*if (tracker.Input.IsUIInput() ^ _isUI) // UI input and not UI mode; not UI input and UI mode
                return;*/
            var inputData = tracker.GetData(delta);
            var buttonEvent = new ButtonInputEvent(this, inputData);
            _buttonSubject.Emit(buttonEvent);
        }

        private void ProcessInput<TEvent>(double delta, TimedTracker<TEvent> tracker, IEventSubject<TEvent> subject)
            where TEvent : GameEvent
        {
            if (!tracker.IsDeadInput())
                tracker.EmitEvent(subject, delta);
            tracker.Tick();
        }
    }
}
