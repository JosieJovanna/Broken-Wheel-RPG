using System;

namespace BrokenWheel.Core.Event.Listening
{
    public interface IUniversalEventListener : IEventListener
    {
        /// <summary>
        /// Gets an <see cref="IEntityEventListener"/> for a given entity, which tracks only events related to that entity.
        /// </summary>
        IEntityEventListener GetEntityEventNexus(string entityId);
        
        /// <summary>
        /// Adds an event handler to listen to <see cref="GameEvent"/>s of type T.
        /// </summary>
        void SubscribeToEventForEntity<T>(EventHandler<T> handler, string entityId) where T : GameEvent;
        
        /// <summary>
        /// Removes an event handler for <see cref="GameEvent"/>s of type T.
        /// </summary>
        void UnsubscribeFromEventForEntity<T>(EventHandler<T> handler, string entityId) where T : GameEvent;

        /// <summary>
        /// Adds an event listener based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <param name="entityId"> The entity whose events will be listened to. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEventForEntity<E, T>(E enumValue, EventHandler<T> handler, string entityId)
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;

        /// <summary>
        /// Removes an event listener based on enum value attached to the event.
        /// </summary>
        /// <param name="enumValue"> The enumeration value to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <param name="entityId"> The entity whose events will be unsubscribed from. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEventForEntity<E, T>(E enumValue, EventHandler<T> handler, string entityId) 
            where E : struct, IConvertible where T : EnumeratedGameEvent<E>;

        /// <summary>
        /// Adds an event listener based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler for this type of event. </param>
        /// <param name="entityId"> The entity whose events will be listened to. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be listened to. </typeparam>
        void SubscribeToEnumeratedEventForEntity<E, T>(string customCode, EventHandler<T> handler, string entityId)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;

        /// <summary>
        /// Removes an event listener based on enum value attached to the event.
        /// Specifically, a custom override, present for some enums, where a default value is selected,
        /// and a string used to access some other related value, included for use in modding.
        /// </summary>
        /// <param name="customCode"> The enumeration value's string override to select. </param>
        /// <param name="handler"> The event handler currently subscribed. </param>
        /// <param name="entityId"> The entity whose events will be unsubscribed from. </param>
        /// <typeparam name="E"> The enum being used to select against. </typeparam>
        /// <typeparam name="T"> The type of enumerated event to be unsubscribed from. </typeparam>
        void UnsubscribeFromEnumeratedEventForEntity<E, T>(string customCode, EventHandler<T> handler, string entityId)
            where E : struct, IConvertible where T : CustomOverrideEnumGameEvent<E>;
    }
}
