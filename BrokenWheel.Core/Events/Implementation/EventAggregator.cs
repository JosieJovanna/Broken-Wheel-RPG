using System;
using System.Collections.Generic;
using System.Reflection;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.Events.Implementation
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
            => GetSubject<TEvent>().AsObservable();

        /// <inheritdoc/>
        public IEventSubject<TEvent> GetSubject<TEvent>()
            => FindOrCreateSubject<TEvent>();

        /// <inheritdoc/>
        public void Subscribe<TEvent>(EventHandlerFunction<TEvent> function)
            => GetObservable<TEvent>().Subscribe(function);

        /// <inheritdoc/>
        public void Unsubscribe<TEvent>(EventHandlerFunction<TEvent> function)
            => GetObservable<TEvent>().Unsubscribe(function);

        /// <inheritdoc/>
        public void SubscribeToAllHandledEvents(object handler)
            => SubscribeToAllHandledEvents(handler, true);

        /// <inheritdoc/>
        public void UnsubscribeFromAllHandledEvents(object handler)
            => SubscribeToAllHandledEvents(handler, false);

        /// <summary>
        /// Gets all implemented handler interfaces and extracts the generic methods 
        /// in order to programatically subscribe to all handled event types.
        /// </summary>
        private void SubscribeToAllHandledEvents(object handler, bool isSubscribing)
        {
            foreach (var interfaceType in handler.GetType().GetInterfaces())
                if (typeof(IEventHandler<object>).Name == interfaceType.Name)
                    foreach (var generic in interfaceType.GetGenericArguments())
                        SubscribeToEventType(handler, generic, isSubscribing);
        }

        private void SubscribeToEventType(object handler, Type eventType, bool isSubscribing)
        {
            var method = GetType().GetMethod(nameof(CastHandlerAndSubOrUnsub),
                BindingFlags.NonPublic | BindingFlags.Instance);
            var generic = method.MakeGenericMethod(eventType);
            generic.Invoke(this, new object[] { handler, isSubscribing });
        }

        private void CastHandlerAndSubOrUnsub<TEvent>(object handler, bool isSubscribing)
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
        {
            if (TryGetSubject<TEvent>(out var subject))
                return subject;
            else
                return CreateAndKeepSubject<TEvent>();
        }

        private bool TryGetSubject<TEvent>(out IEventSubject<TEvent> subject)
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
        {
            _logger.LogCategory("Events", $"Creating subject for event {typeof(TEvent).Name}...");
            var subject = new EventSubject<TEvent>();
            _subjectsByEventType.Add(typeof(TEvent), subject);
            return subject;
        }

        public IEventObservable<TEvent> GetObservable<TEvent>(string category)
        {
            throw new NotImplementedException();
        }

        public IEventSubject<TEvent> GetSubject<TEvent>(string category)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TEvent>(EventHandlerFunction<TEvent> function, string category)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEvent>(EventHandlerFunction<TEvent> function, string category)
        {
            throw new NotImplementedException();
        }
    }
}
