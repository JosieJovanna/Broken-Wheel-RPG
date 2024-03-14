using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables
{
    /// <summary>
    /// A class which allows its consumer to subscribe and unsubscribe any number of handlers for game events
    /// of the specified type.
    /// </summary>
    /// <typeparam name="TEvent"> The type of <see cref="GameEvent"/> being listened to. </typeparam>
    public interface IEventObservable<out TEvent>
    {
        /// <summary>
        /// Adds an event handler object, if not already subscribed.
        /// If the event is categorized, any value will trigger this handler.
        /// </summary>
        /// <param name="handler"> The event handler object. </param>
        void Subscribe(IEventHandler<TEvent> handler);

        /// <summary>
        /// Adds an event handler function, if not already subscribed.
        /// If the event is categorized, any value will trigger this handler.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        void Subscribe(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Removes an event handler object, if already subscribed.
        /// If the handler is subscribed to categorized/custom category events, will not unsubscribe from those.
        /// </summary>
        /// <param name="handler"> The event handler object. </param>
        void Unsubscribe(IEventHandler<TEvent> handler);

        /// <summary>
        /// Removes an event handler function, if already subscribed.
        /// If the handler is subscribed to categorized/custom category events, will not unsubscribe from those.
        /// </summary>
        /// <param name="function"> The event handler function. </param>
        void Unsubscribe(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Subscribes a handler object to events with a certain category, if not already subscribed.
        /// </summary>
        /// <param name="category"> The category of the event. </param>
        /// <param name="handler"> The event handler object. </param>
        void SubscribeToCategory(string category, IEventHandler<TEvent> handler);

        /// <summary>
        /// Subscribes a handler function to events of a certain category, if not already subscribed.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void SubscribeToCategory(string category, EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Unsubscribes a handler object from events of a certain category, if subscribed.
        /// If there are handlers registered for all categories, will not unsubscribe from those.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="handler"> The event handler object. </param>
        void UnsubscribeFromCategory(string category, IEventHandler<TEvent> handler);

        /// <summary>
        /// Unsubscribes a handler function from events of a certain category, if subscribed.
        /// If there are handlers registered for all categories, will not unsubscribe from those.
        /// </summary>
        /// <param name="category"> The category of event. </param>
        /// <param name="function"> The event handler function. </param>
        void UnsubscribeFromCategory(string category, EventHandlerFunction<TEvent> function);
    }
}
