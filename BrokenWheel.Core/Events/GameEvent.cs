using System;

namespace BrokenWheel.Core.Events
{
    public abstract class GameEvent // TODO: add override where all categories are effectively the same
    {
        public object Sender { get; }

        public string Category { get; }

        /// <summary>
        /// A game event with metadata for the sending object, and the category of event.
        /// This allows handlers to listen to only certain categories of event.
        /// </summary>
        /// <param name="sender"> The object that created this event. </param>
        /// <param name="category"> The category of event. </param>
        /// <exception cref="ArgumentNullException"> If sender is null. </exception>
        /// <exception cref="InvalidOperationException"> If category is null or whitespace. </exception>
        protected GameEvent(object sender, string category)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));

            if (string.IsNullOrWhiteSpace(category))
                throw new InvalidOperationException($"{nameof(category)} cannot be null or whitespace.");
            Category = category;
        }
    }
}
