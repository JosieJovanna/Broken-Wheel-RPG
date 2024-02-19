using System;

namespace BrokenWheel.Core.Events
{
    public abstract class GameEvent
    {
        public object Sender { get; }

        /// <summary>
        /// Creates a new game event to be delivered to subscribers.
        /// </summary>
        /// <param name="sender"> The object which sent this event. </param>
        /// <exception cref="ArgumentNullException"> When any argument is null. </exception>
        protected GameEvent(object sender)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }
    }
}
