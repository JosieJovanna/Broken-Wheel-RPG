using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class EventSubject<TEvent> : IEventSubject<TEvent>
        where TEvent : GameEvent
    {
        private EventHandlerFunction<TEvent> _handlersForAllEvents;

        public IEventObservable<TEvent> AsObservable() => this;

        public virtual void Emit(TEvent @event) => EmitEvent(@event);

        protected void EmitEvent(TEvent @event) => _handlersForAllEvents?.Invoke(@event);

        public void Subscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents += handler.HandleEvent;

        public void Subscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents += function;

        public void Unsubscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents -= handler.HandleEvent;

        public void Unsubscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents -= function;
    }
}
