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

        /// <summary>
        /// Emits a <see cref="StringEnumSwitchGameEvent{T}"/> to any subscribers of that particular custom or enum value.
        /// Also emits the event for subscribers that listen to any category, as well as listeners of the custom enum value.
        /// </summary>
        void EmitStringEnumSwitch(TEvent @event);
    }
}
