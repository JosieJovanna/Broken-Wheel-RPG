using System;
using System.Collections.Generic;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class EnumSwitchSubject<TEvent, TEnum> 
        : Subject<TEvent>, IEnumSwitchSubject<TEvent, TEnum>
        where TEvent : EnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        private readonly IDictionary<TEnum, EventHandlerFunction<TEvent>> _enumSwitchHandlers
            = new Dictionary<TEnum, EventHandlerFunction<TEvent>>();

        public IEnumSwitchObservable<TEvent, TEnum> AsEnumSwitchObservable() => this;

        public void EmitEnumSwitchEvent(TEvent @event)
        {
            EmitEvent(@event);
            if (_enumSwitchHandlers.TryGetValue(@event.EnumValue, out var handlers))
                handlers.Invoke(@event);
        }

        public void SubscribeToCategory(TEnum category, IEventHandler<TEvent> handler) 
            => SubscribeToCategory(category, handler.HandleEvent);
        public void SubscribeToCategory(TEnum category, EventHandlerFunction<TEvent> function)
        {
            if (_enumSwitchHandlers.ContainsKey(category))
                _enumSwitchHandlers[category] += function;
            else
                _enumSwitchHandlers.Add(category, function);
        }

        public void UnsubscribeFromCategory(TEnum category, IEventHandler<TEvent> handler) 
            => UnsubscribeFromCategory(category, handler.HandleEvent);
        public void UnsubscribeFromCategory(TEnum category, EventHandlerFunction<TEvent> function)
        {
            if (_enumSwitchHandlers.ContainsKey(category))
                _enumSwitchHandlers[category] -= function;
        }
    }
}
