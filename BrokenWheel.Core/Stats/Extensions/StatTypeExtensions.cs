using System;
using System.Linq;
using BrokenWheel.Core.Stats.Attributes;
using BrokenWheel.Core.Stats.Meta;

namespace BrokenWheel.Core.Stats.Extensions
{
    public static class StatTypeExtensions
    {
        public static string GetCode(this StatType stat)
        {
            return stat.ToString();
        }
        
        public static string GetName(this StatType stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute == null 
                ? stat.ToString()
                : infoAttribute.Name;
        }

        public static string GetMenuName(this StatType stat)
        {
            var menuNameAttribute = stat.GetAttribute<MenuNameAttribute>();
            return menuNameAttribute == null 
                ? System.Enum.GetName(typeof(StatType), stat) 
                : menuNameAttribute.MenuName;
        }

        public static string GetDescription(this StatType stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute == null
                ? string.Empty
                : infoAttribute.Description;
        }


        public static bool IsComplex(this StatType stat) => stat.HasAttribute<ComplexStatAttribute>();
        public static bool IsAttribute(this StatType stat) => stat.GetCategory() == StatCategory.Attribute;

        public static bool IsSkill(this StatType stat) => stat.GetCategory() == StatCategory.Skill;
        public static bool IsProficiency(this StatType stat) => stat.GetCategory() == StatCategory.Proficiency;

        public static StatCategory GetCategory(this StatType stat)
        {
            return stat.GetAttribute<StatCategoriesAttribute>()?.Category ?? default;
        }

        public static StatSubCategory GetSubCategory(this StatType stat)
        {
            return stat.GetAttribute<StatCategoriesAttribute>()?.SubCategory ?? default;
        }

        private static bool HasAttribute<T>(this StatType stat) where T : Attribute
        {
            return stat.GetAttribute<T>() != null;
        }

        private static T GetAttribute<T>(this StatType stat) where T : Attribute
        {
            return stat.GetType()
                .GetMember(stat.ToString())
                .FirstOrDefault()
                ?.GetCustomAttributes(typeof(T), false)
                .FirstOrDefault() as T;
        }
    }
}
