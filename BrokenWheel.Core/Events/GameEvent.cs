using System;

namespace BrokenWheel.Core.Events
{
    public abstract class GameEvent : EventArgs
    {
        public object Sender { get; }
        public string EntityId { get; }

        /// <summary>
        /// Creates a new game event to be delivered to subscribers.
        /// </summary>
        /// <param name="sender"> The object which sent this event. </param>
        /// <param name="entityId"> The ID of the game entity this event data is tied to. </param>
        /// <exception cref="ArgumentNullException"> When any argument is null. </exception>
        protected GameEvent(object sender, string entityId)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
        }
    }
}
