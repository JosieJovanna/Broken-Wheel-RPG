using System;

namespace BrokenWheel.Core.Settings.Registration
{
    /// <summary>
    /// An abstract class to be inherited by any mod's settings.
    /// Allows the Settings slice to not reference any other projects by blindly loading/editing the settings.
    /// This chunk of settings data is then accessed using this class, whose protected methods are intended
    /// to be used in order to access these settings with properties.
    /// </summary>
    ///
    /// <remarks>
    /// Any class inheriting from this should not have set accessors, as to avoid implementations abusing an accessible setter.
    /// Registering and getting these settings will be more complicated, unfortunately.
    /// The API will need to specify a way to track these things, then get this section of data, all without the
    /// Settings slice interact with the mod namespace.
    /// </remarks>
    public class CustomSettings : ISettings
    {
        protected T GetProperty<T>(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
