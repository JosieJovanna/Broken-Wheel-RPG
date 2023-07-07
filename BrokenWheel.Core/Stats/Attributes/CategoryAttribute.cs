using System;

namespace BrokenWheel.Core.Stats.Attributes
{
    /// <summary>
    /// The grouping of stats with similar areas of effect, more specific than Attribute/Skill/Proficiency.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class CategoryAttribute : Attribute
    {
        public readonly StatCategory Category;

        public CategoryAttribute(StatCategory category)
        {
            Category = category;
        }
    }
}
