using System;

namespace BrokenWheel.Core.Event
{
    /// <summary>
    /// A game event differentiated by enumerable values, with the ability to override the value with a string.
    /// This is useful for custom stats, which may be added by mods, or even as trackers for different variables.
    /// It is assumed that the custom value is the default value of that enum, but it is possible to set the enum value
    /// to a different value than default, while still having an override. 
    /// </summary>
    public abstract class CustomOverrideEnumGameEvent<T> : CategorizedGameEvent<T> where T : struct, IConvertible
    {
        public bool IsOverridden { get; protected set; }
        public string OverrideCode { get; protected set; }

        protected CustomOverrideEnumGameEvent(object sender, string entityId, T? type)
            : base(sender, entityId, type)
        {
            IsOverridden = false;
        }

        /// <param name="overrideCode">
        /// If the override code is not null, will iset it as the override, ignoring enum value given.
        /// </param>
        protected CustomOverrideEnumGameEvent(object sender, string entityId, T? type, string overrideCode = null)
            : base(sender, entityId, type)
        {
            IsOverridden = false;
        }
        
        protected CustomOverrideEnumGameEvent(object sender, string entityId, string overrideCode) 
            : base(sender, entityId, default)
        {
            IsOverridden = true;
            OverrideCode = overrideCode;
        }
    }
}
