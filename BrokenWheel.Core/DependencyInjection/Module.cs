using System;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.DependencyInjection
{
    public sealed class Module : IModule
    {
        private readonly IDictionary<Type, object> _serviceRegistry = new Dictionary<Type, object>();
        private readonly IDictionary<Type, ISettings> _settingsRegistry = new Dictionary<Type, ISettings>();
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a module with a logger.
        /// </summary>
        /// <exception cref="ArgumentNullException"> If logger is null. </exception>
        internal Module(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceRegistry.Add(typeof(ILogger), _logger);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Initialized {typeof(Module)}.");
        }

        /// <inheritdoc/>
        public ILogger GetLogger()
        {
            return _logger;
        }

        /// <inheritdoc/>
        public TInterface GetService<TInterface>()
        {
            var type = typeof(TInterface);
            if (!_serviceRegistry.ContainsKey(type))
                ThrowNotRegistered(type, "service");

            return (TInterface)_serviceRegistry[type];
        }

        /// <inheritdoc/>
        public IModule RegisterService<TInterface, TImplementation>(TImplementation implementation)
            where TImplementation : class, TInterface
        {
            var type = ThrowArgumentExceptionIfInvalidTypes<TInterface>();
            if (_serviceRegistry.ContainsKey(type))
            {
                ThrowAlreadyRegistered(type, "service");
                return this;
            }

            _serviceRegistry.Add(type, implementation);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered `{typeof(TImplementation).FullName}` to `{type.FullName}`.");
            return this;
        }

        private Type ThrowArgumentExceptionIfInvalidTypes<TInterface>()
        {
            var typeInterface = typeof(TInterface);
            if (!typeInterface.IsInterface)
            {
                var message = $"Generic type `{typeInterface.FullName}` is not an interface.";
                _logger.LogCategoryError(LogCategory.DEPENDENCY_INJECTION, message);
                throw new ArgumentException(message);
            }
            return typeInterface;
        }

        /// <inheritdoc/>
        public TSettings GetSettings<TSettings>() where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            if (!_settingsRegistry.ContainsKey(type))
                return CreateNewSettings<TSettings>();

            return (TSettings)_serviceRegistry[type];
        }

        private TSettings CreateNewSettings<TSettings>() where TSettings : class, ISettings
        {
            var constructor = typeof(TSettings).GetConstructor(Type.EmptyTypes);
            var settings = (TSettings)constructor.Invoke(null);
            RegisterSettings(settings);
            return settings;
        }

        /// <inheritdoc/>
        public IModule RegisterSettings<TSettings>(TSettings settings) where TSettings : class, ISettings
        {
            var type = typeof(TSettings);
            if (_settingsRegistry.ContainsKey(type))
                ThrowAlreadyRegistered(type, "settings");

            _settingsRegistry.Add(type, settings);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered `{typeof(TSettings).FullName}`.");
            return this;
        }

        private void ThrowNotRegistered(Type type, string notRegistered)
        {
            var message = $"Interface `{type.FullName}` doesn't have a registered {notRegistered}.";
            _logger.LogCategoryError(LogCategory.DEPENDENCY_INJECTION, message);
            throw new InvalidOperationException(message);
        }

        private void ThrowAlreadyRegistered(Type type, string registered)
        {
            var message = $"Interface `{type.Name}` already has a registered {registered}.";
            _logger.LogCategoryError(LogCategory.DEPENDENCY_INJECTION, message);
            throw new InvalidOperationException(message);
        }
    }
}
