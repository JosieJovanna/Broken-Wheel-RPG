using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class EventSubject<TEvent> : IEventSubject<TEvent>
        where TEvent : GameEvent
    {
        private EventHandlerFunction<TEvent> _handlersForAllEvents;

        /// <inheritdoc/>
        public IEventObservable<TEvent> AsObservable() => this;

        /// <inheritdoc/>
        public virtual void Emit(TEvent @event) => EmitUncategorizedEvent(@event);

        /// <summary>
        /// Emits the general event to all universal listeners.
        /// </summary>
        protected void EmitUncategorizedEvent(TEvent @event) => _handlersForAllEvents?.Invoke(@event);

        /// <inheritdoc/>
        public void Subscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents += handler.HandleEvent;

        /// <inheritdoc/>
        public void Subscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents += function;

        /// <inheritdoc/>
        public void Unsubscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents -= handler.HandleEvent;

        /// <inheritdoc/>
        public void Unsubscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents -= function;
    }
}
