using System;
using BrokenWheel.Core.Events.Abstract;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Listening
{
    /// <summary>
    /// A class which allows its consumer to subscribe and unsubscribe any number of handlers for game events
    /// of the specified type. Can subscribe to all events, by enum value, or by a string value overriding the enum.
    /// </summary>
    /// <typeparam name="TEvent"> The type of <see cref="GameEvent"/> being listened to. </typeparam>
    /// <typeparam name="TEnum"> The enumerator type that the events are categorized by. </typeparam>
    public interface ICustomCategorizedEventListener<out TEvent, in TEnum> : ICategorizedEventListener<TEvent, TEnum>
        where TEvent : EnumSwitchGameEvent<TEnum>
        where TEnum : struct, IConvertible // enum
    {
        /// <summary>
        /// Subscribes a handler object to events with a certain custom category, if not already subscribed.
        /// </summary>
        /// <param name="customCategory"> The category of the event overriding the usual enum value. </param>
        /// <param name="handler"> The event handler object. </param>
        void SubscribeToCategory(string customCategory, IEventHandler<TEvent> handler);

        /// <summary>
        /// Subscribes a handler function to events with a certain custom category, if not already subscribed.
        /// </summary>
        /// <param name="customCategory"> The category of the event overriding the usual enum value. </param>
        /// <param name="function"> The event handler function. </param>
        void SubscribeToCategory(string customCategory, EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Unsubscribes a handler object from events of a certain custom category, if subscribed.
        /// </summary>
        /// <param name="customCategory"> The category of event. </param>
        /// <param name="handler"> The event handler object. </param>
        void UnsubscribeFromCategory(string customCategory, IEventHandler<TEvent> handler);

        /// <summary>
        /// Unsubscribes a handler function from events of a certain custom category, if subscribed.
        /// </summary>
        /// <param name="customCategory"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void UnsubscribeFromCategory(string customCategory, EventHandlerFunction<TEvent> function);
    }
}
