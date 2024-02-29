using System;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Control.Events;
using BrokenWheel.Core.GameModes;

namespace BrokenWheel.Control.Puppeteering.Implementation
{
    public class ActionController : IActionController // TODO: process on physics tick?
    {
        private readonly IMarionetteControl _control;

        public ActionController(
            IEventAggregator eventAggregator,
            IMarionetteControl marionetteControl)
        {
            _control = marionetteControl ?? throw new ArgumentNullException(nameof(marionetteControl));

            if (eventAggregator == null)
                throw new ArgumentNullException(nameof(eventAggregator));
            eventAggregator.SubscribeToAllHandledEvents(this);
        }

        public void CancelPendingActions()
        {
            // TODO...
        }

        public void CancelAllActions()
        {
            // TODO...
        }

        public void HandleEvent(MoveInputEvent gameEvent)
        {
            _control.Move(
                horizontal: (float)gameEvent.Data.VelocityX,
                vertical: (float)gameEvent.Data.VelocityY,
                delta: (float)gameEvent.Data.DeltaTime);
        }

        public void HandleEvent(LookInputEvent gameEvent)
        {
            _control.Look(
                horizontal: (float)gameEvent.Data.VelocityX,
                vertical: (float)gameEvent.Data.VelocityY,
                delta: (float)gameEvent.Data.DeltaTime);
        }

        public void HandleEvent(ButtonInputEvent gameEvent)
        {
            var data = gameEvent.Data;
            if (data.Input == Enum.RPGInput.Action && gameEvent.Data.PressType is Enum.PressType.Released)
            {
                // TODO: additional logic for strength
                _control.Jump((float)data.HeldTime);
            }
            // TODO: logic for handling each input...
        }

        public void HandleEvent(GameModeUpdateEvent gameEvent) => CancelPendingActions();
    }
}
