using System;
using System.Linq;
using BrokenWheel.Core.Stats.Attributes;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Extensions
{
    internal static class StatExtensions
    {
        public static bool IsComplex(this Stat stat) => stat.HasAttribute<ComplexStatAttribute>();

        public static InfoAttribute GetInfoAttribute(this Stat stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute ??
                   throw new InvalidOperationException($"Given {nameof(Stat)} '{stat}' does not have information attached to it.");
        }
        
        public static StatType GetType(this Stat stat)
        {
            return stat.GetAttribute<StatCategoriesAttribute>()?.Type ?? default;
        }

        public static StatCategory GetCategory(this Stat stat)
        {
            return stat.GetAttribute<StatCategoriesAttribute>()?.Category ?? default;
        }

        private static bool HasAttribute<T>(this Stat stat) where T : Attribute
        {
            return stat.GetAttribute<T>() != null;
        }

        private static T GetAttribute<T>(this Stat stat) where T : Attribute
        {
            return stat.GetType()
                .GetMember(stat.ToString())
                .FirstOrDefault()
                ?.GetCustomAttributes(typeof(T), false)
                .FirstOrDefault() as T;
        }
    }
}
