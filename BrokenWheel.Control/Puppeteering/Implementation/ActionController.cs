using System;
using BrokenWheel.Control.Events;
using BrokenWheel.Core.Events.Observables;

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
            eventAggregator.GetObservable<MoveInputEvent>().Subscribe(this);
            eventAggregator.GetObservable<LookInputEvent>().Subscribe(this);
            eventAggregator.GetObservable<ButtonInputEvent>().Subscribe(this); // TODO: method to listen to all handled events
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
        }
    }
}
