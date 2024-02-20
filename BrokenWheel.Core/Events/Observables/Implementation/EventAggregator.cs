using System;
using System.Collections.Generic;
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

        public IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent
            => GetSubject<TEvent>().AsObservable();

        public IEventSubject<TEvent> GetSubject<TEvent>()
            where TEvent : GameEvent
            => FindOrCreateSubject<TEvent>();

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
