using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.Core.Events.Attributes;

namespace BrokenWheel.Core.Events.Implementation
{
    public partial class EventSubject<TEvent> : IEventSubject<TEvent>
    {
        private readonly IList<ConditionalHandler> _conditionalHandlers = new List<ConditionalHandler>();
        private EventHandlerFunction<TEvent> _handlersForAllEvents;
        private int _currentID = 0;

        public TEvent Last { get; protected set; }

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
            Last = Current;
            Current = @event;
            _handlersForAllEvents?.Invoke(@event);
            EmitConditionals(@event);
        }

        private void EmitConditionals(TEvent @event)
        {
            foreach (var conditional in _conditionalHandlers)
                if (conditional.Predicate.Invoke(@event))
                    conditional.Handler.Invoke(@event);
        }

        /// <inheritdoc/>
        public IEventObservable<TEvent> AsObservable() => this;

        /// <inheritdoc/>
        public void Subscribe(EventHandlerFunction<TEvent> function)
        {
            _handlersForAllEvents += function;
        }

        /// <inheritdoc/>
        public void Unsubscribe(EventHandlerFunction<TEvent> function)
        {
            _handlersForAllEvents -= function;
            foreach (var condHandler in _conditionalHandlers)
                condHandler.Handler -= function;
        }

        /// <inheritdoc/>
        public int SubscribeConditional(EventHandlerFunction<TEvent> function, Func<TEvent, bool> predicate)
        {
            _currentID++;
            _conditionalHandlers.Add(new ConditionalHandler(
                predicate: predicate,
                handler: function,
                id: _currentID));
            return _currentID;
        }

        /// <inheritdoc/>
        public void UnsubscribeConditional(int id)
        {
            var conditionalHandler = _conditionalHandlers.FirstOrDefault(_ => _.ID == id);
            _conditionalHandlers.Remove(conditionalHandler);
        }
    }
}
