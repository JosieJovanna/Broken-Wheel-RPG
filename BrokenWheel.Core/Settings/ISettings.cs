﻿namespace BrokenWheel.Core.Settings
{
    /// <summary>
    /// An interface used only to identify the object as 'settings'.
    /// <see cref="SettingsFactory"/> uses this interface so that only settings can be registered.
    /// Any class implementing this also has the expectation of being decorated with attributes,
    /// containing metadata for displaying the settings for editing.
    /// </summary>
    public interface ISettings
    {
        
    }
}
