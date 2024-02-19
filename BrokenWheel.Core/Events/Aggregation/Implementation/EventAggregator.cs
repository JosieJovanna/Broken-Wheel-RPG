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

        public IEnumSwitchObservable<TEvent, TEnum> GetEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
            => GetEnumSwitchSubject<TEvent, TEnum>().AsEnumSwitchObservable();

        public IEnumSwitchSubject<TEvent, TEnum> GetEnumSwitchSubject<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
            => FindOrCreateSubject<IEnumSwitchSubject<TEvent, TEnum>, TEvent>();

        public IStringEnumSwitchObservable<TEvent, TEnum> GetStringEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
            => GetStringEnumSwitchSubject<TEvent, TEnum>().AsStringEnumSwitchObservable();

        public IStringEnumSwitchSubject<TEvent, TEnum> GetStringEnumSwitchSubject<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
        {
            if (TryGetSubject<IStringEnumSwitchSubject<TEvent, TEnum>, TEvent>(out var subject))
                return subject;
        }

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
            var eventType = typeof(TEvent);
            if (typeof(EnumSwitchGameEvent<>).IsAssignableFrom(eventType))
                typeof(IEnumSwitchSubject<TEvent, >)
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
