using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Custom
{
    /// <summary>
    /// A POCO used for deserializing custom stats.
    /// </summary>
    internal class CustomStat
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public StatType Type { get; set; } = StatType.Custom;
        public StatCategory Category { get; set; } = StatCategory.Custom;
        public bool IsComplex { get; set; } = false;

        public int DefaultValue { get; set; } = 0;
        public int DefaultMaximum { get; set; } = 100;
    }
}
