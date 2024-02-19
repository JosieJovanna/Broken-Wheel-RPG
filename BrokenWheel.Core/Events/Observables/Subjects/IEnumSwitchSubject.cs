using System;

namespace BrokenWheel.Core.Events.Observables.Subjects
{
    public interface IEnumSwitchSubject<TEvent, TEnum>
        : IEnumSwitchObservable<TEvent, TEnum>, IEventSubject<TEvent>
        where TEvent : EnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        /// <summary>
        /// Gets the subject as an observable with no emit capability.
        /// </summary>
        IEnumSwitchObservable<TEvent, TEnum> AsEnumSwitchObservable();
    }
}
