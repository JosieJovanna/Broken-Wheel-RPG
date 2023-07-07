using System;

namespace BrokenWheel.Core.Stats.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class MenuNameAttribute : Attribute
    {
        public readonly string MenuName;

        public MenuNameAttribute(string menuName)
        {
            MenuName = menuName ?? throw new ArgumentNullException(nameof(menuName));
        }
    }
}
