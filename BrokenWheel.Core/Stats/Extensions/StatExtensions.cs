using System;
using System.Linq;
using BrokenWheel.Core.Stats.Attributes;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Extensions
{
    internal static class StatExtensions
    {
        public static StatInfo GetStatInfo(this Stat stat)
        {
            if (stat == Stat.Custom)
                throw new InvalidOperationException("The custom stat value has no set information.");
            var infoAttr = stat.GetStatInfoAttribute();
            return new StatInfo
            {
                Stat = stat,
                Type = infoAttr.Type,
                Category = infoAttr.Category,
                Code = infoAttr.Code,
                Name = infoAttr.Name,
                Description = infoAttr.Description,
                IsComplex = infoAttr.IsComplex,
                IsCustom = false
            };
        }

        private static StatInfoAttribute GetStatInfoAttribute(this Stat stat)
        {
            if (!HasAttribute<StatInfoAttribute>(stat))
                throw NoStatInfoException(stat);
            var infoAttribute = stat.GetAttribute<StatInfoAttribute>();
            return infoAttribute ?? throw NoStatInfoException(stat);

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

        private static InvalidOperationException NoStatInfoException(Stat stat)
        {
            return new InvalidOperationException(
                $"Given {nameof(Stat)} '{stat}' does not have information attached to it.");
        }
    }
}
