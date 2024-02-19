﻿using System;

namespace BrokenWheel.Core.Events
{
    /// <summary>
    /// A game event differentiated by enumerable values, with the ability to override the value with a string.
    /// This is useful for custom stats, which may be added by mods, or even as trackers for different variables.
    /// It is assumed that the custom value is the default value of that enum, but it is possible to set the enum value
    /// to a different value than default, while still having an override. 
    /// </summary>
    public abstract class StringEnumSwitchGameEvent<T> : EnumSwitchGameEvent<T> where T : struct, IConvertible
    {
        public bool IsOverridden { get; protected set; }
        public string OverrideCode { get; protected set; }

        /// <param name="overrideCode">
        /// If the override code is not null, will iset it as the override, ignoring enum value given.
        /// </param>
        protected StringEnumSwitchGameEvent(object sender, T? type, bool isOverridden = false, string overrideCode = null)
            : base(sender, type)
        {
            IsOverridden = isOverridden;
            OverrideCode = overrideCode;
        }
    }
}
