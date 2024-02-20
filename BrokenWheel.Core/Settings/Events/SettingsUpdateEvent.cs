﻿using BrokenWheel.Core.Events;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.Settings.Events
{
    public class SettingsUpdateEvent<TSettings> : GameEvent where TSettings : class, ISettings
    {
        /// <summary>
        /// A reference to the edited settings file, in case the reference is lost.
        /// </summary>
        public TSettings Settings { get; }

        public SettingsUpdateEvent(object sender, string entityId, TSettings settings)
            : base(sender, entityId)
        {
            Settings = settings;
        }
    }
}