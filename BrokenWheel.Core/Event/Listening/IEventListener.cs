using System;
using BrokenWheel.Core.Event.Handling;

namespace BrokenWheel.Core.Event.Listening
{
    public interface IEventListener
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
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;

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
        
        /// <summary>
        /// Adds an event handler to listen to <see cref="GameEvent"/>s of type T.
        /// </summary>
        void SubscribeToEvent<T>(EventHandler<T> handler) where T : GameEvent;
        
        /// <summary>
        /// Removes an event handler for <see cref="GameEvent"/>s of type T.
        /// </summary>
        void UnsubscribeFromEvent<T>(EventHandler<T> handler) where T : GameEvent;

        /// <summary>
        /// Adds an event handler based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEvent<E, T>(E enumValue, EventHandler<T> handler)
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;
        
        /// <summary>
        /// Removes an event handler based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEvent<E, T>(E enumValue, EventHandler<T> handler) 
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;

        /// <summary>
        /// Adds an event handler based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEvent<E, T>(string customCode, EventHandler<T> handler)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
        
        /// <summary>
        /// Removes an event handler based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEvent<E, T>(string customCode, EventHandler<T> handler)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
        
        /// <summary>
        /// Adds an listener for <see cref="GameEvent"/>s of type T.
        /// </summary>
        void SubscribeToEvent<T>(IEventHandler<T> handler) where T : GameEvent;
        
        /// <summary>
        /// Removes a listener for <see cref="GameEvent"/>s of type T.
        /// </summary>
        void UnsubscribeFromEvent<T>(IEventHandler<T> handler) where T : GameEvent;

        /// <summary>
        /// Adds an event listener based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEvent<E, T>(E enumValue, IEventHandler<T> handler)
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;
        
        /// <summary>
        /// Removes an event listener based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEvent<E, T>(E enumValue, IEventHandler<T> handler) 
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;

        /// <summary>
        /// Adds an event listener based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEvent<E, T>(string customCode, IEventHandler<T> handler)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
        
        /// <summary>
        /// Removes an event listener based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEvent<E, T>(string customCode, IEventHandler<T> handler)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
    }
}
