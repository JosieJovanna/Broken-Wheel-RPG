using System;

namespace BrokenWheel.Core.Events.Observables.Subjects
{
    internal interface IStringEnumSwitchSubject<TEvent, TEnum>
        : IStringEnumSwitchObservable<TEvent, TEnum>, IEnumSwitchSubject<TEvent, TEnum>
        where TEvent : StringEnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        void EmitStringEnumSwitch(TEvent @event);
    }
}
