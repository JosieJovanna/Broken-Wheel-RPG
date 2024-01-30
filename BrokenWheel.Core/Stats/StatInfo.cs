using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats
{
    public class StatInfo
    {
        public Stat Stat { get; internal set; }
        public StatType Type { get; internal set; }
        public StatCategory Category { get; internal set; }
        
        public bool IsCustom { get; internal set; }
        public bool IsComplex { get; internal set; }
        
        public string Code { get; internal set; }
        public string Name { get; internal set; }
        public string Description { get; internal set; }
    }
}
