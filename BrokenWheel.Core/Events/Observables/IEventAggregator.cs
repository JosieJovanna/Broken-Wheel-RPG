using BrokenWheel.Core.Events.Handling;

namespace BrokenWheel.Core.Events.Observables
{
    /// <summary>
    /// An object responsible for tracking event subjects and observables.
    /// Also grants the ability to quickly subscribe to handled events by passing in an <see cref="IEventHandler{TEvent}"/>
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Gets the readonly observable for the given event type, to subscibe/unsubscribe.
        /// </summary>
        IEventObservable<TEvent> GetObservable<TEvent>();

        /// <summary>
        /// Gets the subject for the given event type, to subscribe/unsubscribe and emit.
        /// </summary>
        IEventSubject<TEvent> GetSubject<TEvent>();

        /// <summary>
        /// Subscribes an event handling function to the given event type.
        /// </summary>
        void Subscribe<TEvent>(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Unsubscribes an event handling function from the given event type.
        /// </summary>
        void Unsubscribe<TEvent>(EventHandlerFunction<TEvent> function);

        /// <summary>
        /// Automatically subscribes to all events that the given object can handle.
        /// Does so for all events specified in implemented <see cref="IEventHandler{TEvent}"/> interfaces.
        /// </summary>
        void SubscribeToAllHandledEvents(object handler);

        /// <summary>
        /// Automatically unsubscribes from all events that the given object can handle.
        /// Does so for all events specified in implemented <see cref="IEventHandler{TEvent}"/> interfaces.
        /// </summary>
        void UnsubscribeFromAllHandledEvents(object handler);
    }
}
