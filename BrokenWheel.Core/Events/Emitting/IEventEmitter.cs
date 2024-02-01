using System;

namespace BrokenWheel.Core.Events.Emitting
{
    public interface IEventEmitter
    {
        /// <summary>
        /// Emits a <see cref="GameEvent"/> to be handled by listeners.
        /// </summary>
        void EmitEvent<T>(T gameEvent) where T : GameEvent;

        /// <summary>
        /// Emits an event for listeners based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="gameEvent"> The event being emitted for listeners. </param>
        /// <param name="emitToCatchallListeners">
        /// Whether the event should be sent to listeners which are registered to the type of event, but not the specific
        /// enumerator value that is specified.
        /// </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event emit. </typeparam>
        void EmitEnumeratedEvent<E, T>(E enumValue, T gameEvent, bool emitToCatchallListeners = true)
            where E : struct, IConvertible where T : CategorizedGameEvent<E>;

        /// <summary>
        /// Emits an event for listeners based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="gameEvent"> The event being emitted for listeners. </param>
        /// <param name="emitToCatchallListeners">
        /// Whether the event should be sent to listeners which are registered to the type of event, but not the specific
        /// enumerator value that is specified.
        /// </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event emit. </typeparam>
        void EmitEnumeratedEvent<E, T>(string customCode, T gameEvent, bool emitToCatchallListeners = true)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
    }
}
