using System;

namespace BrokenWheel.Core.Events
{
    public class GameEventArgs : EventArgs
    {
        public string EntityGuid { get; }

        public GameEventArgs(string entityGuid)
        {
            EntityGuid = entityGuid;
        }
    }
}
