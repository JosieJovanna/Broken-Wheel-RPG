﻿using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.Core.Stats.Events
{
    public abstract class AbstractStatUpdatedEvent<T> : CustomOverrideEnumGameEvent<StatType>
        where T : StatUpdate
    {
        public StatInfo StatInfo { get; }
        public T Stat { get; }
        
        protected AbstractStatUpdatedEvent(object sender, string entityId, StatInfo statInfo, T statUpdateData) 
            : base(sender, entityId, statInfo?.Type)
        {
            Stat = statUpdateData;
            StatInfo = statInfo ?? throw new ArgumentNullException(nameof(statInfo));
            IsOverridden = statInfo.IsCustom;
            if (IsOverridden)
                OverrideCode = statInfo.Code;
        }
    }
}