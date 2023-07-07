using System;

namespace BrokenWheel.Core.Common.Stats.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ComplexAttribute : Attribute
    { }


    [AttributeUsage(AttributeTargets.Field)]
    public class AttributeAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Field)]
    public class SkillAttribute : Attribute
    { }

    [AttributeUsage(AttributeTargets.Field)]
    public class ProficiencyAttribute : Attribute
    { }
}
