using BrokenWheel.Core.Events;

namespace BrokenWheel.Core.GameModes
{
    public class GameModeUpdateEvent : GameEvent
    {
        public GameMode GameMode { get; }

        public GameModeUpdateEvent(object sender, GameMode gameMode)
            : base(sender, gameMode.ToString())
        {
            GameMode = gameMode;
        }
    }
}
