using System;
using System.Collections.Generic;
using System.Text;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Events.Observables.Subjects;

namespace BrokenWheel.Core.Events.Aggregation.Implementation
{
    public class EventAggregator : IEventAggregator
    {
        public IEnumSwitchObservable<TEvent, TEnum> GetEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
        {
            throw new NotImplementedException();
        }

        public Observables.IObservable<TEvent> GetObservable<TEvent>() where TEvent : GameEvent
        {
            throw new NotImplementedException();
        }

        public IStringEnumSwitchObservable<TEvent, TEnum> GetStringEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible
        {
            throw new NotImplementedException();
        }

        public ISubject<TEvent> GetSubject<TEvent>() where TEvent : GameEvent
        {
            throw new NotImplementedException();
        }
    }
}
