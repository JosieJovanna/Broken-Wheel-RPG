using System;
using System.Collections.Generic;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Events.Observables.Subjects;
using BrokenWheel.Core.Events.Observables.Subjects.Implementation;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.Events.Aggregation.Implementation
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ILogger _logger;
        private readonly SubjectTracker _subjectTracker = new SubjectTracker();
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
            => FindOrCreateSubject<IEventSubject<TEvent>, TEvent>();

        public ICategorizedObservable<TEvent> GetCategorizedObservable<TEvent>()
            where TEvent : CategorizedEvent
            => GetCategorizedSubject<TEvent>().AsCategorizedObservable();

        public ICategorizedSubject<TEvent> GetCategorizedSubject<TEvent>()
            where TEvent : CategorizedEvent
            => FindOrCreateSubject<ICategorizedSubject<TEvent>, TEvent>();

        /// <summary>
        /// Gets a subject for the given type if it exists.
        /// </summary>
        private TSubject FindOrCreateSubject<TSubject, TEvent>()
            where TSubject : IEventSubject<TEvent>
            where TEvent : GameEvent
        {
            if (TryGetSubject<TSubject, TEvent>(out var subject))
                return subject;
            else
                return CreateAndKeepSubject<TSubject, TEvent>();
        }

        private bool TryGetSubject<TSubject, TEvent>(out TSubject subject)
            where TSubject : IEventSubject<TEvent>
            where TEvent : GameEvent
        {
            subject = default;
            try
            {
                subject = (TSubject)_subjectsByEventType[typeof(TEvent)];
                return true;
            }
            catch
            {
                return false;
            }
        }

        private TSubject CreateAndKeepSubject<TSubject, TEvent>()
            where TSubject : IEventSubject<TEvent>
            where TEvent : GameEvent
        {
            _logger.LogCategory("Events", $"Creating subject {typeof(TSubject).FullName}...");
            var subject = CreateSubject<TSubject, TEvent>();
            _subjectsByEventType.Add(typeof(TEvent), subject);
            return subject;
        }

        private TSubject CreateSubject<TSubject, TEvent>()
            where TSubject : IEventSubject<TEvent>
            where TEvent : GameEvent
        {
            if (typeof(CategorizedEvent).IsAssignableFrom(typeof(TEvent)))
                return new CategorizedSubject<TEvent>();
        }

        /*private TSubject CreateSubject<TSubject, TEvent>()
            where TSubject : IEventSubject<TEvent>
            where TEvent : GameEvent
        {
            var type = typeof(TSubject);
            var emptyConstructor = type.GetConstructor(Type.EmptyTypes);
            var subject = emptyConstructor.Invoke(null);
            return (TSubject)subject;
        }*/
    }
}
