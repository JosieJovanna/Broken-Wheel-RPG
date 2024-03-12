using System;
using System.Collections.Generic;
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
            var constructor = type.GetConstructor(Type.EmptyTypes);
            var settings = Cast<TSettings>(constructor.Invoke(null));
            RegisterSettings(settings);
            return settings;
        }

        /// <inheritdoc/>
        public IModule RegisterSettings<TSettings>(TSettings settings) where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            if (_settingsRegistry.ContainsKey(type))
                LogAndThrow($"Settings `{type.Name}` already registered.");

            _settingsRegistry.Add(type, settings);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered `{typeof(TSettings).FullName}`.");
            // TODO: recursively register (if not already registered) any child settings AND if the settings is already registered when instantiating, then set those child properties to the initialized settings.
            return this;
        }
    }
}
