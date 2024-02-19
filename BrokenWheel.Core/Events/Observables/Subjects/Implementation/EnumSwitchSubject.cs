using System;
using System.Collections.Generic;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables.Subjects.Implementation
{
    internal class EnumSwitchSubject<TEvent, TEnum>
        : EventSubject<TEvent>, IEnumSwitchSubject<TEvent, TEnum>
        where TEvent : EnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        private readonly IDictionary<TEnum, EventHandlerFunction<TEvent>> _enumSwitchHandlers
            = new Dictionary<TEnum, EventHandlerFunction<TEvent>>();

        /// <inheritdoc/>
        public IEnumSwitchObservable<TEvent, TEnum> AsEnumSwitchObservable() => this;

        /// <summary>
        /// Emits the general event to all universal listeners,
        /// and to all enum-specific listeners.
        /// </summary>
        public override void Emit(TEvent @event)
        {
            EmitUniversalEvent(@event);
            EmitEnumSwitchEvent(@event);
        }

        /// <summary>
        /// Emits events to listeners of that enum value.
        /// </summary>
        protected void EmitEnumSwitchEvent(TEvent @event)
        {
            if (_enumSwitchHandlers.TryGetValue(@event.EnumValue, out var handlers))
                handlers.Invoke(@event);
        }

        /// <inheritdoc/>
        public void SubscribeToCategory(TEnum category, IEventHandler<TEvent> handler)
            => SubscribeToCategory(category, handler.HandleEvent);

        /// <inheritdoc/>
        public void SubscribeToCategory(TEnum category, EventHandlerFunction<TEvent> function)
        {
            if (_enumSwitchHandlers.ContainsKey(category))
                _enumSwitchHandlers[category] += function;
            else
                _enumSwitchHandlers.Add(category, function);
        }

        /// <inheritdoc/>
        public void UnsubscribeFromCategory(TEnum category, IEventHandler<TEvent> handler)
            => UnsubscribeFromCategory(category, handler.HandleEvent);

        /// <inheritdoc/>
        public void UnsubscribeFromCategory(TEnum category, EventHandlerFunction<TEvent> function)
        {
            if (_enumSwitchHandlers.ContainsKey(category))
                _enumSwitchHandlers[category] -= function;
        }
    }
}
