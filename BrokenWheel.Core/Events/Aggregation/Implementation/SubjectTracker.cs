using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrokenWheel.Core.Events.Observables.Subjects;

namespace BrokenWheel.Core.Events.Aggregation.Implementation
{
    internal class SubjectTracker
    {
        private readonly IList<SubjectWrapper> _wrappers = new List<SubjectWrapper>();

        public TSubject GetSubject<TSubject, TEvent>()
            where TSubject : IEventSubject<TEvent>
            where TEvent : GameEvent
        {
            _wrappers.Where(_ => _.GetType().IsAssignableFrom(typeof(TSubject)))
        }
    }
}
