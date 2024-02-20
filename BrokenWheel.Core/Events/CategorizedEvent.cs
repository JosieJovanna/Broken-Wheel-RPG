using System;

namespace BrokenWheel.Core.Events
{
    public abstract class CategorizedEvent : GameEvent
    {
        public string Category { get; }

        protected CategorizedEvent(object sender, string category)
            : base(sender)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException($"{nameof(category)} cannot be null or whitespace.");
            Category = category;
        }

        public bool IsCategory(string category)
        {
            return Category == category;
        }
    }
}
