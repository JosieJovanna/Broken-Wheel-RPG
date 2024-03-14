using System.Collections.Generic;

namespace BrokenWheel.Core.Events.Implementation
{
    internal partial class EventSubject<TEvent> : IEventSubject<TEvent>
    {
        private readonly IDictionary<string, EventHandlerFunction<TEvent>> _handlersByCategory
            = new Dictionary<string, EventHandlerFunction<TEvent>>();

        private EventHandlerFunction<TEvent> _handlersForAllEvents;

        /// <inheritdoc/>
        public virtual void Emit(TEvent @event)
        {
            _handlersForAllEvents?.Invoke(@event);
        }

        public virtual void EmitCategorizedEvent(TEvent @event, string category)
        {
            if (_handlersByCategory.TryGetValue(category, out var handlers)) // TODO: change this to be separate class? 
                handlers.Invoke(@event);
        }

        /// <inheritdoc/>
        public IEventObservable<TEvent> AsObservable() => this;

        /// <inheritdoc/>
        public void Subscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents += handler.HandleEvent;

        /// <inheritdoc/>
        public void Subscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents += function;

        /// <inheritdoc/>
        public void Unsubscribe(IEventHandler<TEvent> handler) => _handlersForAllEvents -= handler.HandleEvent;

        /// <inheritdoc/>
        public void Unsubscribe(EventHandlerFunction<TEvent> function) => _handlersForAllEvents -= function;

        /// <inheritdoc/>
        public void SubscribeToCategory(string category, IEventHandler<TEvent> handler)
            => SubscribeToCategory(@category, handler.HandleEvent);

        /// <inheritdoc/>
        public void SubscribeToCategory(string category, EventHandlerFunction<TEvent> function)
        {
            if (_handlersByCategory.ContainsKey(category))
                _handlersByCategory[category] += function;
            else
                _handlersByCategory.Add(category, function);
        }

        /// <inheritdoc/>
        public void UnsubscribeFromCategory(string category, IEventHandler<TEvent> handler)
            => UnsubscribeFromCategory(category, handler.HandleEvent);

        /// <inheritdoc/>
        public void UnsubscribeFromCategory(string category, EventHandlerFunction<TEvent> function)
        {
            if (_handlersByCategory.ContainsKey(category))
                _handlersByCategory[category] -= function;
        }
    }
}
