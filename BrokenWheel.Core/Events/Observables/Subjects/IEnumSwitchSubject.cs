using System;

namespace BrokenWheel.Core.Events.Observables.Subjects
{
    public interface IEnumSwitchSubject<TEvent, TEnum>
        : IEnumSwitchObservable<TEvent, TEnum>, ISubject<TEvent>
        where TEvent : EnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        /// <summary>
        /// Gets the subject as an observable with no emit capability.
        /// </summary>
        IEnumSwitchObservable<TEvent, TEnum> AsEnumSwitchObservable();

        /// <summary>
        /// Emits an <see cref="EnumSwitchGameEvent{T}"/>.
        /// All subscribers to that event's enum value are alerted.
        /// Also alerts subscribers to the general event, regardless of category.
        /// </summary>
        void EmitEnumSwitchEvent(TEvent @event);
    }
}
