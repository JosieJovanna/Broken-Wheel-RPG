using System;
using System.Collections.Generic;
using System.Reflection;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.Events.Observables.Implementation
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ILogger _logger;
        private readonly IDictionary<Type, object> _subjectsByEventType = new Dictionary<Type, object>();

        public EventAggregator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent
            => GetSubject<TEvent>().AsObservable();

        /// <inheritdoc/>
        public IEventSubject<TEvent> GetSubject<TEvent>()
            where TEvent : GameEvent
            => FindOrCreateSubject<TEvent>();

        /// <inheritdoc/>
        public void Subscribe<TEvent>(EventHandlerFunction<TEvent> function)
            where TEvent : GameEvent
            => GetObservable<TEvent>().Subscribe(function);

        /// <inheritdoc/>
        public void Unsubscribe<TEvent>(EventHandlerFunction<TEvent> function)
            where TEvent : GameEvent
            => GetObservable<TEvent>().Unsubscribe(function);

        /// <inheritdoc/>
        public void SubscribeToAllHandledEvents(object handler)
            => HandleAllHandledEvents(handler, true);

        /// <inheritdoc/>
        public void UnsubscribeFromAllHandledEvents(object handler)
            => HandleAllHandledEvents(handler, false);

        /// <summary>
        /// Gets all implemented handler interfaces and extracts the generic methods 
        /// in order to programatically subscribe to all handled event types.
        /// </summary>
        private void HandleAllHandledEvents(object handler, bool isSubscribing)
        {
            foreach (var @interface in handler.GetType().GetInterfaces())
                if (@interface.IsInterface && typeof(IEventHandler<GameEvent>).Name == @interface.Name)
                    HandleByGenericEventType(handler, @interface, isSubscribing);
        }

        private void HandleByGenericEventType(object handler, Type @interface, bool isSubscribing)
        {
            foreach (var generic in @interface.GetGenericArguments())
                if (typeof(GameEvent).IsAssignableFrom(generic) && _subjectsByEventType.ContainsKey(generic))
                    HandleEventType(handler, generic, isSubscribing);
        }

        public void HandleEventType(object handler, Type eventType, bool isSubscribing)
        {
            var method = GetType().GetMethod(nameof(CastHandlerAndSubOrUnsub),
                BindingFlags.NonPublic | BindingFlags.Instance);
            var generic = method.MakeGenericMethod(eventType);
            generic.Invoke(this, new object[] { handler, isSubscribing });
        }

        private void CastHandlerAndSubOrUnsub<TEvent>(object handler, bool isSubscribing)
            where TEvent : GameEvent
        {
            var eventHandler = (IEventHandler<TEvent>)handler;
            if (isSubscribing)
                Subscribe<TEvent>(eventHandler.HandleEvent);
            else
                Unsubscribe<TEvent>(eventHandler.HandleEvent);
        }

        /// <summary>
        /// Gets a subject for the given type if it exists.
        /// </summary>
        private IEventSubject<TEvent> FindOrCreateSubject<TEvent>()
            where TEvent : GameEvent
        {
            if (TryGetSubject<TEvent>(out var subject))
                return subject;
            else
                return CreateAndKeepSubject<TEvent>();
        }

        private bool TryGetSubject<TEvent>(out IEventSubject<TEvent> subject)
            where TEvent : GameEvent
        {
            subject = default;
            try
            {
                subject = (IEventSubject<TEvent>)_subjectsByEventType[typeof(TEvent)];
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IEventSubject<TEvent> CreateAndKeepSubject<TEvent>()
            where TEvent : GameEvent
        {
            _logger.LogCategory("Events", $"Creating subject for event {typeof(TEvent).Name}...");
            var subject = new EventSubject<TEvent>();
            _subjectsByEventType.Add(typeof(TEvent), subject);
            return subject;
        }
    }
}
