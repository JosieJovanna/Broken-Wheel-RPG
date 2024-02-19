using System;
using BrokenWheel.Core.Events.Observables.Subjects;

namespace BrokenWheel.Core.Events.Aggregation.Implementation
{
    /// <summary>
    /// A non-generic wrapper used to keep track of a subject and get around generic-type dictionary issues.
    /// </summary>
    internal class SubjectWrapper
    {
        public Type EventType { get; }

        private readonly object _subject;

        public SubjectWrapper(Type eventType, object subject)
        {
            EventType = eventType;
            _subject = subject ?? throw new ArgumentNullException(nameof(subject));
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
