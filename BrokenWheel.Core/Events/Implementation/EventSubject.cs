using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.Core.Events.Attributes;

namespace BrokenWheel.Core.Events.Implementation
{
    internal partial class EventSubject<TEvent> : IEventSubject<TEvent>
    {
        private readonly IDictionary<string, EventHandlerFunction<TEvent>> _handlersByCategory
            = new Dictionary<string, EventHandlerFunction<TEvent>>();

        private EventHandlerFunction<TEvent> _handlersForAllEvents;

        public TEvent LastValue { get; protected set; }

        public TEvent Current { get; protected set; }

        public EventSubject()
        {
            Current = GetDefaultValueForEvent();
        }

        private TEvent GetDefaultValueForEvent()
        {
            var method = GetDefaultEventGetterMethod();
            if (method == null)
                return default;

            return TryGetDefaultValue(method);
        }

        private MethodInfo GetDefaultEventGetterMethod()
        {
            return typeof(TEvent)
                .GetMethods()
                .FirstOrDefault(method => method.CustomAttributes
                    .Any(attr => attr.AttributeType.FullName == typeof(DefaultEventGetterAttribute).FullName));
        }

        private TEvent TryGetDefaultValue(MethodInfo defaultGetter)
        {
            try
            {
                return (TEvent)defaultGetter.Invoke(null, new object[0]); // null since static; no args
            }
            catch (Exception e)
            {
                Injection.GetModule().GetLogger().LogWarning($"Event type default method {typeof(TEvent).Name}.{defaultGetter.Name} throws an exception despite being decorated with the {nameof(DefaultEventGetterAttribute)} - {e.Message}");
                return default;
            }
        }

        /// <inheritdoc/>
        public virtual void Emit(TEvent @event)
        {
            LastValue = Current;
            Current = @event;
            _handlersForAllEvents?.Invoke(@event);
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
