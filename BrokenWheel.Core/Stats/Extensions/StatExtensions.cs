using System;
using System.Linq;
using BrokenWheel.Core.Stats.Attributes;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Extensions
{
    internal static class StatExtensions
    {
        public static StatInfoAttribute GetInfoAttribute(this Stat stat)
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
