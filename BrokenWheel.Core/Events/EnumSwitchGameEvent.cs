﻿using System;

namespace BrokenWheel.Core.Events
{
    public abstract class EnumSwitchGameEvent<T> : GameEvent
        where T : struct, IConvertible // since prior to .Net7.3, cannot use enum as generic
    {
        public T EnumValue { get; }

        protected EnumSwitchGameEvent(object sender, T? type)
            : base(sender)
        {
            if (!typeof(T).IsEnum)
                throw new InvalidOperationException($"Generic type {nameof(T)} must be an enum.");
            EnumValue = type ?? throw new ArgumentNullException(nameof(type));
        }
    }
}
