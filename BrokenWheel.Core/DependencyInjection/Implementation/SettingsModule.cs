using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.DependencyInjection.Implementation
{
    public partial class Module
    {
        private readonly IDictionary<Type, ISettings> _settingsRegistry = new Dictionary<Type, ISettings>();

        /// <inheritdoc/>
        public TSettings GetSettings<TSettings>() where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            if (!_settingsRegistry.ContainsKey(type))
                return CreateNewSettings<TSettings>();

            return Cast<TSettings>(_settingsRegistry[type]);
        }

        private TSettings CreateNewSettings<TSettings>() where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Creating default {type.Name} as there is no existing settings file...");
            var settings = ConstructSettings<TSettings>();
            RegisterSettings(settings); // TODO: recursively find children settings that are registered
            return settings;
        }

        private TSettings ConstructSettings<TSettings>() where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var instance = constructor.Invoke(null);
            return Cast<TSettings>(instance);
        }

        private void SetRegisteredChildSettings(ISettings settings)
        {
            foreach (var prop in GetSubSettingProperties(settings))
                if (_settingsRegistry.ContainsKey(prop.PropertyType))
                    TrySettingRegisteredSubSettingProperty(settings, prop);
        }

        private void TrySettingRegisteredSubSettingProperty(ISettings settings, PropertyInfo prop)
        {
            try
            {
                prop.SetValue(settings, _settingsRegistry[prop.PropertyType]);
            }
            catch (Exception e)
            {
                throw new DependencyException($"Exception ocurred while trying to set {settings.GetType().Name}.{prop.Name} to registered {prop.PropertyType.Name} value - {e.Message}", e);
            }
        }

        /// <inheritdoc/>
        public IModule RegisterSettings<TSettings>(TSettings settings) where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            if (_settingsRegistry.ContainsKey(type))
                LogAndThrow($"Settings `{type.Name}` already registered.");

            Register(type, settings);
            return this;
        }

        private void Register(Type type, ISettings settings)
        {
            _settingsRegistry.Add(type, settings);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered `{type.FullName}`.");
            RecursiveRegister(settings);
        }

        private void RecursiveRegister(ISettings settings)
        {
            foreach (var prop in GetSubSettingProperties(settings))
                if (!_settingsRegistry.ContainsKey(prop.PropertyType))
                    Register(prop.PropertyType, settings);
        }

        private IEnumerable<PropertyInfo> GetSubSettingProperties(ISettings settings)
        {
            var iSettings = typeof(ISettings);
            return settings.GetType().GetProperties()
                .Where(_ => _.PropertyType
                    .GetInterfaces()
                    .Contains(iSettings));
        }
    }
}
