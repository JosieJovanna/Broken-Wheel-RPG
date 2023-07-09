using System;
using System.Linq;
using System.Collections.Generic;

namespace BrokenWheel.Core.Settings.Registration
{
    /// <summary>
    /// A static class used for getting settings as needed.
    /// It is expected that settings are initialized during application startup.
    /// </summary>
    /// 
    /// <remarks>
    /// Implemented as a static class and not a service, because without full microsoft DI,
    /// I would need to make another static class which can fetch this service across virtually every project --
    /// just pushing the issue of having a dependency on a concrete class down the line.
    /// </remarks>
    public static class SettingsRegistry
    {
        private static readonly IDictionary<Type, ISettings> _settingsByType = new Dictionary<Type, ISettings>();

        /// <summary>
        /// Registers multiple <see cref="ISettings"/> at once to the service.
        /// Duplicate objects being registered will be ignored, but no exception is thrown.
        /// If the object being registered is a separate instance of that type, an exception will be thrown.
        /// This is because the settings objects are reference types used to keep other code using current settings,
        /// and as there is no settings updated event, they have no way of otherwise knowing.
        /// </summary>
        /// <exception cref="SettingsException"> When a separate instance of the same class is already registered. </exception>
        public static void RegisterSettings(IEnumerable<ISettings> settingsList)
        {
            if (settingsList == null)
                return;
            foreach (var settings in settingsList.Distinct())
                RegisterSettings(settings);
        }

        /// <summary>
        /// Registers a <see cref="ISettings"/> object to its type, to be fetched throughout the code.
        /// Duplicate objects being registered will be ignored, but no exception is thrown.
        /// If the object being registered is a separate instance of that type, an exception will be thrown.
        /// This is because the settings objects are reference types used to keep other code using current settings,
        /// and as there is no settings updated event, they have no way of otherwise knowing.
        /// </summary>
        /// <param name="settings"> The settings object to register. Its type will be used as the key for fetching. </param>
        /// <exception cref="SettingsException"> When a separate instance of the same class is already registered. </exception>
        public static void RegisterSettings(ISettings settings)
        {
            var type = settings.GetType();
            if (_settingsByType.ContainsKey(type))
                if (_settingsByType[type] != settings)
                    throw new SettingsException(string.Format(SettingsException.DIFFERENT_OBJECT_FORMAT, type.Name));
                else return;
            _settingsByType.Add(type, settings);
        }

        /// <summary>
        /// Attempts to get settings without letting an exception bubble up.
        /// </summary>
        /// <param name="settings"> The settings, if registered. </param>
        /// <typeparam name="T"> The <see cref="ISettings"/> type being fetched. </typeparam>
        /// <returns> Whether there was an instance of the settings registered. </returns>
        public static bool TryGetSettings<T>(out T settings) where T : class, ISettings
        {
            try
            {
                settings = (T)_settingsByType[typeof(T)];
                return true;
            }
            catch (Exception)
            {
                settings = null;
                return false;
            }
        }

        /// <summary>
        /// Attempts to get registered settings.
        /// </summary>
        /// <typeparam name="T"> The type of <see cref="ISettings"/> being fetched. </typeparam>
        /// <returns> The instance of the settings. </returns>
        /// <exception cref="SettingsException"> When the settings are not already registered. </exception>
        public static T GetSettings<T>() where T : class, ISettings
        {
            try
            {
                return (T)_settingsByType[typeof(T)];
            }
            catch (Exception ex)
            {
                var message = string.Format(SettingsException.NOT_REGISTERED_FORMAT, typeof(T).Name, ex.Message);
                throw new SettingsException(message, ex);
            }
        }
    }
}
