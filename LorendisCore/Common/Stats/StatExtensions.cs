using System;
using System.Linq;
using LorendisCore.Common.Stats.Attributes;

namespace LorendisCore.Common.Stats
{
    public static class StatExtensions
    {
        public static string GetName(this Stat stat)
        {
            var menuNameAttribute = stat.GetAttribute<MenuNameAttribute>();
            return menuNameAttribute == null 
                ? Enum.GetName(typeof(Stat), stat) 
                : menuNameAttribute.MenuName;
        }

        public static string DescriptiveName(this Stat stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute == null 
                ? stat.GetName() 
                : infoAttribute.Name;
        }

        public static string Description(this Stat stat)
        {
            var infoAttribute = stat.GetAttribute<InfoAttribute>();
            return infoAttribute == null
                ? string.Empty
                : infoAttribute.Description;
        }


        public static bool IsComplex(this Stat stat) 
            => stat.HasAttribute<ComplexAttribute>();
        public static bool IsAttribute(this Stat stat) 
            => stat.HasAttribute<AttributeAttribute>();
        public static bool IsSkill(this Stat stat) 
            => stat.HasAttribute<SkillAttribute>();
        public static bool IsProficiency(this Stat stat) 
            => stat.HasAttribute<ProficiencyAttribute>();


        private static bool HasAttribute<T>(this Stat stat) where T : Attribute
        {
            return stat.GetAttribute<T>() != null;
        }

        private static T GetAttribute<T>(this Stat stat) where T : Attribute
        {
            return stat.GetType()
                .GetMember(stat.ToString()).First()
                ?.GetCustomAttributes(typeof(T), false)
                .First() as T;
        }
    }
}
