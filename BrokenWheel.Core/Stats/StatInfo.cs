using System;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats
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
            Code = statType.GetCode();
            IsCustom = false;
            IsComplex = statType.IsComplex();
        }

        public StatInfo(string customTypeCode, string customTypeName, bool isComplex)
        {
            if (string.IsNullOrWhiteSpace(customTypeCode))
                throw new ArgumentException($"{nameof(customTypeCode)} cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(customTypeName))
                throw new ArgumentException($"{nameof(customTypeName)} cannot be null or whitespace.");

            Type = StatType.Custom;
            IsCustom = true;
            
            Name = customTypeName;
            Code = customTypeCode;
            IsComplex = isComplex;
        }

        public static StatInfo FromCode(string code)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
