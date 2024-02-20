using System;
using System.Collections.Generic;
using System.Text;

namespace BrokenWheel.Core.Events
{
    public abstract class UncategorizedGameEvent : GameEvent
    {
        public const string CATEGORY = "UNCATEGORIZED";
        protected UncategorizedGameEvent(object sender)
            : base(sender, CATEGORY)
        {
        }
    }
}
