﻿using BrokenWheel.Core.Events;

namespace BrokenWheel.Core.GameModes
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