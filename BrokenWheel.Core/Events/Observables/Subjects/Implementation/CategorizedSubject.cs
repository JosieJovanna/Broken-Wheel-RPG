using System.Collections.Generic;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class CategorizedSubject<TEvent>
        : EventSubject<TEvent>, ICategorizedSubject<TEvent>
        where TEvent : CategorizedEvent
    {
        private readonly IDictionary<string, EventHandlerFunction<TEvent>> _handlersByCategory 
            = new Dictionary<string, EventHandlerFunction<TEvent>>();

        public ICategorizedObservable<TEvent> AsCategorizedObservable() => this;

        public override void Emit(TEvent @event)
        {
            EmitUncategorizedEvent(@event);
            EmitCategorizedEvent(@event);
        }

        protected void EmitCategorizedEvent(TEvent @event)
        {
            if (_handlersByCategory.TryGetValue(@event.Category, out var handlers))
                handlers.Invoke(@event);
        }

        public void SubscribeToCategory(string category, IEventHandler<TEvent> handler) 
            => SubscribeToCategory(@category, handler.HandleEvent);
        public void SubscribeToCategory(string category, EventHandlerFunction<TEvent> function)
        {
            if (_handlersByCategory.ContainsKey(category))
                _handlersByCategory[category] += function;
            else
                _handlersByCategory.Add(category, function);
        }

        public void UnsubscribeFromCategory(string category, IEventHandler<TEvent> handler) 
            => UnsubscribeFromCategory(category, handler.HandleEvent);
        public void UnsubscribeFromCategory(string category, EventHandlerFunction<TEvent> function)
        {
            if (_handlersByCategory.ContainsKey(category))
                _handlersByCategory[category] -= function;
        }
    }
}
