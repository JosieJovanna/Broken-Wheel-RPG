namespace BrokenWheel.Core.Settings.Registration
{
    /// <summary>
    /// An interface used only to identify the object as 'settings'.
    /// <see cref="SettingsRegistry"/> uses this interface so that only settings can be registered.
    /// Any class implementing this also has the expectation of being decorated with attributes,
    /// containing metadata for displaying the settings for editing.
    /// </summary>
    public interface ISettings
    {
        
    }
}
