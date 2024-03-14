using System;

namespace BrokenWheel.Core.Events.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DefaultEventGetterAttribute : Attribute
    {
    }
}
