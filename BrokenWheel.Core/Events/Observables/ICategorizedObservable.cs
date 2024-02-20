using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables
{
    public interface ICategorizedObservable<TEvent>
        : IEventObservable<TEvent>
        where TEvent : CategorizedEvent
    {
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
