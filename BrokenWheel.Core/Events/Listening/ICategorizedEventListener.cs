using System;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Listening
{
    /// <summary>
    /// A class which allows its consumer to subscribe and unsubscribe any number of handlers for game events
    /// of the specified type. Can subscribe to all events, or subscribe by enum value.
    /// </summary>
    /// <typeparam name="TEvent"> The type of <see cref="GameEvent"/> being listened to. </typeparam>
    /// <typeparam name="TEnum"> The enumerator type that the events are categorized by. </typeparam>
    public interface ICategorizedEventListener<out TEvent, in TEnum> : IEventListener<TEvent>
        where TEvent : CategorizedGameEvent<TEnum> 
        where TEnum : struct, IConvertible // enum
    {
        /// <summary>
        /// Subscribes a handler object to events with a certain category, if not already subscribed.
        /// If the category value is the same as is used for custom categories, subscribes to all custom categories.
        /// </summary>
        /// <param name="category"> The category of the event. </param>
        /// <param name="handler"> The event handler object. </param>
        void SubscribeToCategory(TEnum category, IEventHandler<TEvent> handler);
        
        /// <summary>
        /// Subscribes a handler function to events of a certain category, if not already subscribed.
        /// If the category value is the same as is used for custom categories, subscribes to all custom categories.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void SubscribeToCategory(TEnum category, EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Unsubscribes a handler object from events of a certain category, if subscribed.
        /// If there are handlers registered for all categories, or custom ones, will not unsubscribe from those.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="handler"> The event handler object. </param>
        void UnsubscribeFromCategory(TEnum category, IEventHandler<TEvent> handler);
        
        /// <summary>
        /// Unsubscribes a handler function from events of a certain category, if subscribed.
        /// If there are handlers registered for all categories, or custom ones, will not unsubscribe from those.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void UnsubscribeFromCategory(TEnum category, EventHandlerFunction<TEvent> function);
    }
}
