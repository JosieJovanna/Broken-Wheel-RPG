using System;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats.Enum
{
    public class StatInfo
    {
        public StatType Type { get; }
        public string Name { get; }
        public string Code { get; }
        public bool IsCustom { get; }
        public bool IsComplex { get; }

        public StatInfo(StatType statType)
        {
            if (statType == StatType.Custom)
                throw new ArgumentException($"{nameof(statType)} cannot be Custom unless code is specified.");
            
            Type = statType;
            Name = statType.GetName();
            Code = statType.ToString();
            IsCustom = false;
            IsComplex = statType.IsComplex();
        }

        public StatInfo(string customTypeCode)
        {
            if (string.IsNullOrWhiteSpace(customTypeCode))
                throw new ArgumentException($"{nameof(customTypeCode)} cannot be null or whitespace.");

            Type = StatType.Custom;
            Name = ""; // TODO: get this from some service
            Code = customTypeCode;
            IsCustom = true;
            IsComplex = false; // TODO: get this from some service
        }
    }
}
