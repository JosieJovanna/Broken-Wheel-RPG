using System;

namespace BrokenWheel.Core.Stats.Enum
{
    public class StatType
    {
        public readonly Stat Stat;
        public readonly string Name;
        public readonly string Code;

        public StatType(Stat statType)
        {
            Stat = statType;
            Name = statType.GetName();
            Code = statType.ToString();
        }

        public StatType(string customTypeName, string customTypeCode)
        {
            if (string.IsNullOrWhiteSpace(customTypeName))
                throw new ArgumentException($"{nameof(customTypeName)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(customTypeCode))
                throw new ArgumentException($"{nameof(customTypeCode)} cannot be null or whitespace.");
            
            Stat = Stat.Custom;
            Name = customTypeName;
            Code = customTypeCode;
        }

        public bool IsCustomStat() => Stat == Stat.Custom;
    }
}
