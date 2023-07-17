using System;

namespace BrokenWheel.Core.Events.Listening
{
    public interface IEventNexus
    {
        /// <summary>
        /// Adds an event handler to listen to <see cref="GameEvent"/>s of type T.
        /// </summary>
        void SubscribeToEvent<T>(EventHandler<T> handler) where T : GameEvent;
        
        /// <summary>
        /// Removes an event handler for <see cref="GameEvent"/>s of type T.
        /// </summary>
        void UnsubscribeFromEvent<T>(EventHandler<T> handler) where T : GameEvent;

        /// <summary>
        /// Adds an event listener based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEvent<E, T>(E enumValue, EventHandler<T> handler)
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;
        
        /// <summary>
        /// Removes an event listener based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEvent<E, T>(E enumValue, EventHandler<T> handler) 
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;

        /// <summary>
        /// Adds an event listener based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="enumValue"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEvent<E, T>(string customEnumValue, EventHandler<T> handler)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
        
        /// <summary>
        /// Removes an event listener based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customEnumValue"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEvent<E, T>(string customEnumValue, EventHandler<T> handler)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
    }
}
