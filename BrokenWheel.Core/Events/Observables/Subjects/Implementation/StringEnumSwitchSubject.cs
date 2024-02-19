using System;
using System.Collections.Generic;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class StringEnumSwitchSubject<TEvent, TEnum>
        : EnumSwitchSubject<TEvent, TEnum>, IStringEnumSwitchSubject<TEvent, TEnum>
        where TEvent : StringEnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        private readonly IDictionary<string, EventHandlerFunction<TEvent>> _customCategoryHandlers
            = new Dictionary<string, EventHandlerFunction<TEvent>>();

        public IStringEnumSwitchObservable<TEvent, TEnum> AsStringEnumSwitchObservable() => this;

        public void EmitStringEnumSwitch(TEvent @event)
        {
            EmitEnumSwitchEvent(@event);
            if (_customCategoryHandlers.TryGetValue(@event.OverrideCode, out var handlers))
                handlers.Invoke(@event);
        }

        public void SubscribeToCustomCategory(string customCategory, IEventHandler<TEvent> handler)
            => SubscribeToCustomCategory(customCategory, handler.HandleEvent);
        public void SubscribeToCustomCategory(string customCategory, EventHandlerFunction<TEvent> function)
        {
            if (_customCategoryHandlers.ContainsKey(customCategory))
                _customCategoryHandlers[customCategory] += function;
            else
                _customCategoryHandlers.Add(customCategory, function);
        }

        public void UnsubscribeFromCustomCategory(string customCategory, IEventHandler<TEvent> handler)
            => UnsubscribeFromCustomCategory(customCategory, handler.HandleEvent);
        public void UnsubscribeFromCustomCategory(string customCategory, EventHandlerFunction<TEvent> function)
        {
            if (_customCategoryHandlers.ContainsKey(customCategory))
                _customCategoryHandlers[customCategory] -= function;
        }
    }
}
