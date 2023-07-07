using System;

namespace BrokenWheel.Core.Common.Stats.Attributes
{
    /// <summary>
    /// A basic attribute used to describe stats in text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class InfoAttribute : Attribute
    {
        public readonly string Name;
        public readonly string Description;

        public InfoAttribute(string name, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
