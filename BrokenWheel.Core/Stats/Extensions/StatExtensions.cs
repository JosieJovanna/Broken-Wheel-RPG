using System;
using System.Linq;
using BrokenWheel.Core.Stats.Attributes;

namespace BrokenWheel.Core.Stats.Extensions
{
    public static class StatExtensions
    {
        public static string GetName(this StatType stat)
        {
            var menuNameAttribute = stat.GetAttribute<MenuNameAttribute>();
            return menuNameAttribute == null 
                ? System.Enum.GetName(typeof(StatType), stat) 
                : menuNameAttribute.MenuName;
        }

        public static string DescriptiveName(this StatType stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute == null 
                ? stat.GetName() 
                : infoAttribute.Name;
        }

        public static string Description(this StatType stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute == null
                ? string.Empty
                : infoAttribute.Description;
        }


        public static bool IsComplex(this StatType stat) 
            => stat.HasAttribute<ComplexAttribute>();
        public static bool IsAttribute(this StatType stat) 
            => stat.HasAttribute<AttributeAttribute>();
        public static bool IsSkill(this StatType stat) 
            => stat.HasAttribute<SkillAttribute>();
        public static bool IsProficiency(this StatType stat) 
            => stat.HasAttribute<ProficiencyAttribute>();


        private static bool HasAttribute<T>(this StatType stat) where T : Attribute
        {
            return stat.GetAttribute<T>() != null;
        }

        private static T GetAttribute<T>(this StatType stat) where T : Attribute
        {
            return stat.GetType()
                .GetMember(stat.ToString()).FirstOrDefault()
                ?.GetCustomAttributes(typeof(T), false)
                .FirstOrDefault() as T;
        }
    }
}
