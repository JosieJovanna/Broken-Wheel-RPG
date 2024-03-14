using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Events.Attributes;

namespace BrokenWheel.Core.GameModes
{
    public class GameModeUpdateEvent
    {
        [DefaultEventGetter]
        public static GameModeUpdateEvent Default()
        {
            return new GameModeUpdateEvent(DebugConstants.GAMEMODE_AT_START);
        }


        public GameMode GameMode { get; }

        public GameModeUpdateEvent(GameMode gameMode)
        {
            GameMode = gameMode;
        }
    }
}
