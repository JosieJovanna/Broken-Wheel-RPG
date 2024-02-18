using System;

namespace BrokenWheel.Core.Events.Observables.Subjects
{
    internal interface IEnumSwitchSubject<TEvent, TEnum>
        : IEnumSwitchObservable<TEvent, TEnum>, ISubject<TEvent>
        where TEvent : EnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        IEnumSwitchObservable<TEvent, TEnum> AsEnumSwitchObservable();

        void EmitEnumSwitch(TEvent @event);
    }
}
