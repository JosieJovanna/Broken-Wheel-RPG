using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class Subject<TEvent> : ISubject<TEvent>
        where TEvent : GameEvent
    {
        private EventHandlerFunction<TEvent> _handlersForAllEvents;

        public IObservable<TEvent> AsObservable() => this;

        public void EmitEvent(TEvent @event) => _handlersForAllEvents?.Invoke(@event);

        public void Subscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents += handler.HandleEvent;

        public void Subscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents += function;

        public void Unsubscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents -= handler.HandleEvent;

        public void Unsubscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents -= function;
    }
}
