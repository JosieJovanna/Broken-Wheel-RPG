using System;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Info
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class StatInfoAttribute : Attribute
    {
        public StatType Type { get; set; }
        public StatCategory Category { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplex { get; set; } = false;
        public int MaxValue { get; set; } = 100;
        public int MinValue { get; set; } = 0;
        public int DefaultValue { get; set; } = 10;

        public StatInfoAttribute(StatType type, StatCategory category)
        {
            Type = type;
            Category = category;
        }
    }
}
