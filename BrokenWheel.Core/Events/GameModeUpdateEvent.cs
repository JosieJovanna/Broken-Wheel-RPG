using BrokenWheel.Core.Enum;
using BrokenWheel.Core.Events.Abstract;

namespace BrokenWheel.Core.Events
{
    public class GameModeUpdateEvent : GameEvent
    {
        public GameMode GameMode { get; }

        public GameModeUpdateEvent(object sender, string entityId, GameMode gameMode) : base(sender, entityId)
        {
            GameMode = gameMode;
        }
    }
}
