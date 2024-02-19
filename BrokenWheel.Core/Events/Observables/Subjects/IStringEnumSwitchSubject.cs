using System;

namespace BrokenWheel.Core.Events.Observables.Subjects
{
    public interface IStringEnumSwitchSubject<TEvent, TEnum>
        : IStringEnumSwitchObservable<TEvent, TEnum>, IEnumSwitchSubject<TEvent, TEnum>
        where TEvent : StringEnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        /// <summary>
        /// Gets the subject as an observable with no emit capability.
        /// </summary>
        IStringEnumSwitchObservable<TEvent, TEnum> AsStringEnumSwitchObservable();
    }
}
