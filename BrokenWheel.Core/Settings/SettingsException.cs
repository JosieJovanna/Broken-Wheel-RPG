using System;

namespace BrokenWheel.Core.Settings
{
    /// <summary>
    /// An exception thrown by <see cref="SettingsFactory"/> when a <see cref="ISettings"/> class is not registered,
    /// and a the game tries to access it. Will only be thrown if settings are not properly instantiated/loaded.
    /// </summary>
    public class SettingsException : Exception
    {
        internal const string NOT_REGISTERED_FORMAT =
            "Exception occurred while getting '{0}' (all settings should be registered at startup) - {1}";

        internal const string DIFFERENT_OBJECT_FORMAT =
            "'{0}' type is already registered with a different object. " +
            "The object must remain consistent in order for changes to be reflected. " +
            "(When attempting to register the same object another time, no exception will be thrown.";

        /// <param name="message"> The reason for the exception being thrown. </param>
        /// <param name="innerException"> The inner exception, if there is any. </param>
        public SettingsException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
