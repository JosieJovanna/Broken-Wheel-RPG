using System;
using BrokenWheel.Core.Stats.Meta;

namespace BrokenWheel.Core.Stats.Attributes
{
    /// <summary>
    /// The grouping of stats with similar areas of effect, more specific than Attribute/Skill/Proficiency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class StatCategoriesAttribute : Attribute
    {
        public readonly StatCategory Category;
        public readonly StatSubCategory SubCategory;

        public StatCategoriesAttribute(StatCategory category, StatSubCategory subCategory)
        {
            Category = category;
            SubCategory = subCategory;
        }
    }
}
