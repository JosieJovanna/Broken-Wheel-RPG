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
        /// If the event is categorized, any value will trigger this handler.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        void Subscribe(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Removes an event handler function, if already subscribed.
        /// If the handler is subscribed to categorized/custom category events, will not unsubscribe from those.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        void Unsubscribe(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Subscribes a handler function to events of a certain category, if not already subscribed.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void SubscribeToCategory(string category, EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Unsubscribes a handler function from events of a certain category, if subscribed.
        /// If there are handlers registered for all categories, will not unsubscribe from those.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void UnsubscribeFromCategory(string category, EventHandlerFunction<TEvent> function);
    }
}
