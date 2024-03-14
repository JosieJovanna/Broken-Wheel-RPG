using System;

namespace BrokenWheel.Core.Events
{
    /// <summary>
    /// A class which allows its consumer to subscribe and unsubscribe any number of handlers for game events
    /// of the specified type.
    /// </summary>
    /// <typeparam name="TEvent"> The type of <see cref="GameEvent"/> being listened to. </typeparam>
    public interface IEventObservable<out TEvent>
    {
        TEvent Last { get; }
        TEvent Current { get; }

        /// <summary>
        /// Adds an event handler function, if not already subscribed.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        void Subscribe(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Removes an event handler function, if already subscribed.
        /// If the function handles events conditionally, then unsubscribes from <b>all</b> conditions.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        void Unsubscribe(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Adds an event handler function, which is called if the given predicate is met.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        /// <param name="predicate"> The predicate which must be true to call the function. </param>
        /// <returns> The ID of the conditional handler, to be used for specific removals. </returns>
        int SubscribeConditional(EventHandlerFunction<TEvent> function, Func<TEvent, bool> predicate);

        /// <summary>
        /// Unsubscribes from the specific condition.
        /// </summary>
        /// <param name="id"> The specific ID of the conditional handler, returned by <see cref="SubscribeWhen"/>. </param>
        void UnsubscribeConditional(int id);
    }
}
