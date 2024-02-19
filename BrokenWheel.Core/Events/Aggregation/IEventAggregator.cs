using System;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Events.Observables.Subjects;

namespace BrokenWheel.Core.Events.Aggregation
{
    public interface IEventAggregator
    {
        Observables.IEventObservable<TEvent> GetObservable<TEvent>()
            where TEvent : GameEvent;

        IEventSubject<TEvent> GetSubject<TEvent>()
            where TEvent : GameEvent;

        IEnumSwitchObservable<TEvent, TEnum> GetEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible; // enum

        IEnumSwitchSubject<TEvent, TEnum> GetEnumSwitchSubject<TEvent, TEnum>()
            where TEvent : EnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible; // enum

        IStringEnumSwitchObservable<TEvent, TEnum> GetStringEnumSwitchObservable<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible; // enum

        IStringEnumSwitchSubject<TEvent, TEnum> GetStringEnumSwitchSubject<TEvent, TEnum>()
            where TEvent : StringEnumSwitchGameEvent<TEnum>
            where TEnum : struct, IConvertible; // enum
    }
}
