using System;

namespace BrokenWheel.Core.Events
{
    public abstract class CustomOverrideEnumGameEvent<T> : EnumeratedGameEvent<T> where T : struct, IConvertible
    {
        public bool IsOverridden { get; protected set; }
        public string OverrideCode { get; protected set; }

        protected CustomOverrideEnumGameEvent(object sender, string entityId, T? type)
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
