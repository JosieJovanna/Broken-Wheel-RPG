﻿using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.Core.Stats.Events
{
    public class ComplexStatUpdatedEvent : AbstractStatUpdateEvent<ComplexStatistic>
    {
        public ComplexStatUpdatedEvent(
            object sender,
            string entityId,
            StatInfo statInfo,
            ComplexStatistic stat)
            : base(sender, entityId, statInfo, stat)
        {
        }
    }
}
