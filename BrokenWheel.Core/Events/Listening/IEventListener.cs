using BrokenWheel.Core.Events.Abstract;
using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Listening
{
    /// <summary>
    /// A class which allows its consumer to subscribe and unsubscribe any number of handlers for game events
    /// of the specified type.
    /// </summary>
    /// <typeparam name="TEvent"> The type of <see cref="GameEvent"/> being listened to. </typeparam>
    public interface IEventListener<out TEvent> where TEvent : GameEvent
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
    }
}
