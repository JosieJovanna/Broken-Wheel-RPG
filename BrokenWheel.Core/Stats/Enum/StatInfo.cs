using System;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats.Enum
{
    public class StatInfo
    {
        public StatType Type { get; }
        public string Name { get; }
        public string Code { get; }
        public bool IsCustom { get => Type == StatType.Custom; }

        public StatInfo(StatType statTypeType)
        {
            Type = statTypeType;
            Name = statTypeType.GetName();
            Code = statTypeType.ToString();
        }

        public StatInfo(string customTypeName, string customTypeCode)
        {
            if (string.IsNullOrWhiteSpace(customTypeName))
                throw new ArgumentException($"{nameof(customTypeName)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(customTypeCode))
                throw new ArgumentException($"{nameof(customTypeCode)} cannot be null or whitespace.");
            
            Type = StatType.Custom;
            Name = customTypeName;
            Code = customTypeCode;
        }
    }
}
