using System;
using System.Linq;
using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Enum
{
    /// <summary>
    /// An internal utility for getting stat info from attribute metadata. 
    /// Outside of Core, it should be accessed by static methods which support custom stats gracefully.
    /// </summary>
    internal static class StatExtensions
    {
        /// <summary>
        /// A method for getting <see cref="StatInfo"/> from the attributes attached to the default engine <see cref="Stat"/>s.
        /// Not as efficient as getting the info from <see cref="StatInfoFactory"/>.
        /// </summary>
        /// <param name="stat"> Any stat except for custom, as custom stats have info associated by just the code, not enum value. </param>
        /// <exception cref="ArgumentException"> If parameter is <see cref="Stat.Custom"/> (the default value). </exception>
        /// <returns></returns>
        internal static StatInfo GetInfo(this Stat stat)
        {
            if (stat == Stat.Custom)
                throw new ArgumentException($"Custom {nameof(Stat)} does not have an assocciated stat info attribute.");
            var infoAttr = stat.GetStatInfoAttribute();
            return new StatInfo
            {
                IsCustom = false,
                Namespace = MiscConstants.GAME_NAMESPACE,
                Stat = stat,
                Type = infoAttr.Type,
                Category = infoAttr.Category,
                Code = infoAttr.Code,
                Name = infoAttr.Name,
                Description = infoAttr.Description,
                IsComplex = infoAttr.IsComplex,
                MaxValue = infoAttr.MaxValue,
                MinValue = infoAttr.MinValue,
                DefaultValue = infoAttr.DefaultValue,
            };
        }

        private static StatInfoAttribute GetStatInfoAttribute(this Stat stat)
        {
            if (stat.TryGetAttribute<StatInfoAttribute>(out var attribute))
                return attribute;
            throw new Exception($"Could not find {nameof(StatInfoAttribute)} attached to {nameof(Stat)} `{stat}`");
        }

        private static bool TryGetAttribute<T>(this Stat stat, out T attribute) where T : Attribute
        {
            try
            {
                attribute = stat.GetAttribute<T>();
                return true;
            }
            catch
            {
                attribute = null;
                return false;
            }
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
