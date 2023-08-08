using System;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Attributes
{
    /// <summary>
    /// The grouping of stats with similar areas of effect, more specific than Attribute/Skill/Proficiency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class StatCategoriesAttribute : Attribute
    {
        public readonly StatType Type;
        public readonly StatCategory Category;

        public StatCategoriesAttribute(StatType type, StatCategory category)
        {
            Type = type;
            Category = category;
        }
    }
}
