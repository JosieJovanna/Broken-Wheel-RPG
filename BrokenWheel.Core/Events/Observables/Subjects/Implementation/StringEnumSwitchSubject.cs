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

        /// <inheritdoc/>
        public IStringEnumSwitchObservable<TEvent, TEnum> AsStringEnumSwitchObservable() => this;

        /// <summary>
        /// Emits the general event to all universal listeners,
        /// emits the enum type to all enum switch listeners,
        /// and if override value, emits to string switch listeners.
        /// </summary>
        public override void Emit(TEvent @event)
        {
            EmitUniversalEvent(@event);
            EmitEnumSwitchEvent(@event);
            EmitStringEnumSwitch(@event);
        }

        /// <summary>
        /// If the <see cref="StringEnumSwitchGameEvent{T}"/> is overridden with a custom value,
        /// then invokes handlers for that specific override value.
        /// </summary>
        protected void EmitStringEnumSwitch(TEvent @event)
        {
            if (!@event.IsOverridden)
                return;
            if (_customCategoryHandlers.TryGetValue(@event.OverrideCode, out var handlers))
                handlers.Invoke(@event);
        }

        /// <inheritdoc/>
        public void SubscribeToCustomCategory(string customCategory, IEventHandler<TEvent> handler)
            => SubscribeToCustomCategory(customCategory, handler.HandleEvent);

        /// <inheritdoc/>
        public void SubscribeToCustomCategory(string customCategory, EventHandlerFunction<TEvent> function)
        {
            if (_customCategoryHandlers.ContainsKey(customCategory))
                _customCategoryHandlers[customCategory] += function;
            else
                _customCategoryHandlers.Add(customCategory, function);
        }

        /// <inheritdoc/>
        public void UnsubscribeFromCustomCategory(string customCategory, IEventHandler<TEvent> handler)
            => UnsubscribeFromCustomCategory(customCategory, handler.HandleEvent);

        /// <inheritdoc/>
        public void UnsubscribeFromCustomCategory(string customCategory, EventHandlerFunction<TEvent> function)
        {
            if (_customCategoryHandlers.ContainsKey(customCategory))
                _customCategoryHandlers[customCategory] -= function;
        }
    }
}
