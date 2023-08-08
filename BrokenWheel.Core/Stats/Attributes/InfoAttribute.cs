using System;

namespace BrokenWheel.Core.Stats.Attributes
{
    /// <summary>
    /// A basic attribute used to describe stats in text.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class InfoAttribute : Attribute
    {
        public readonly string Code;
        public readonly string Name;
        public readonly string Description;

        public InfoAttribute(
            string code, 
            string name, 
            string description)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"{nameof(code)} cannot be null or blank.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} cannot be null or blank.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException($"{nameof(description)} cannot be null or blank.");
            
            Code = code;
            Name = name;
            Description = description;
        }
    }
}
