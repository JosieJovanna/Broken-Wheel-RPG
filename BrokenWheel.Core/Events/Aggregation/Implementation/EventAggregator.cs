using System;
using System.Collections.Generic;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Events.Observables.Subjects;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.Events.Aggregation.Implementation
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ILogger _logger;
        private readonly IDictionary<Type, SubjectWrapper> _subjectsByType = new Dictionary<Type, SubjectWrapper>();

        public EventAggregator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Observables.IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent
            => GetSubject<TEvent>().AsObservable();
        public IEventSubject<TEvent> GetSubject<TEvent>() where TEvent : GameEvent
        {
            throw new NotImplementedException();
        }

        public IEnumSwitchObservable<TEvent, TEnum> GetEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
            => GetEnumSwitchSubject<TEvent, TEnum>().AsEnumSwitchObservable();
        public IEnumSwitchSubject<TEvent, TEnum> GetEnumSwitchSubject<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
        {
            throw new NotImplementedException();
        }

        public IStringEnumSwitchObservable<TEvent, TEnum> GetStringEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
            => GetStringEnumSwitchSubject<TEvent, TEnum>().AsStringEnumSwitchObservable();
        public IStringEnumSwitchSubject<TEvent, TEnum> GetStringEnumSwitchSubject<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
        {
            /*var test = new SubjectWrapper();

            if (test.TryGetAs<IStringEnumSwitchSubject<TEvent, TEnum>, TEvent>(out var subject))
                return subject;
            else*/

            throw new NotImplementedException();
        }

        private TSubject CreateSubject<TSubject, TEvent>()
            where TSubject : IEventSubject<TEvent>
        {

        }

        private class SubjectWrapper
        {
            public Type SubjectType { get; set; }
            private readonly IEventSubject<GameEvent> _subject;

            public SubjectWrapper(IEventSubject<GameEvent> subject)
            {
                _subject = subject ?? throw new ArgumentNullException(nameof(subject));
                SubjectType = _subject.GetType();
            }

            public bool TryGetAs<TSubject, TEvent>(out TSubject subject)
                where TSubject : IEventSubject<TEvent>
                where TEvent : GameEvent
            {
                subject = default;
                try
                {
                    subject = (TSubject)_subject;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
