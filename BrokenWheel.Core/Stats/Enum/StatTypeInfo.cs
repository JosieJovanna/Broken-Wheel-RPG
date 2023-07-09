using System;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats.Enum
{
    public class StatTypeInfo
    {
        public readonly StatType StatType;
        public readonly string Name;
        public readonly string Code;

        public StatTypeInfo(StatType statTypeType)
        {
            StatType = statTypeType;
            Name = statTypeType.GetName();
            Code = statTypeType.ToString();
        }

        public StatTypeInfo(string customTypeName, string customTypeCode)
        {
            if (string.IsNullOrWhiteSpace(customTypeName))
                throw new ArgumentException($"{nameof(customTypeName)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(customTypeCode))
                throw new ArgumentException($"{nameof(customTypeCode)} cannot be null or whitespace.");
            
            StatType = StatType.Custom;
            Name = customTypeName;
            Code = customTypeCode;
        }

        public bool IsCustomStat() => StatType == StatType.Custom;
    }
}
