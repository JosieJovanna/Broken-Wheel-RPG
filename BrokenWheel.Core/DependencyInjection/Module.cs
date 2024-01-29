using System;
using System.Collections.Generic;
using BrokenWheel.Core.Constants;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.DependencyInjection
{
    public sealed class Module : IModule
    {
        private readonly IDictionary<Type, object> _registry = new Dictionary<Type, object>();
        private readonly ILogger _logger;

        internal Module(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _registry.Add(typeof(ILogger), _logger);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Initialized {typeof(Module)}.");
        }

        public ILogger GetLogger()
        {
            return _logger;
        }

        public TInterface Get<TInterface>()
        {
            var type = typeof(TInterface);
            if (!_registry.ContainsKey(type))
                throw new InvalidOperationException($"Interface `{type.FullName}` doesn't have a registered service.");

            return (TInterface)_registry[type];
        }

        public void Register<TInterface, TImplementation>(TImplementation implementation)
            where TImplementation : class
        {
            var type = ThrowArgumentExceptionIfInvalidTypes<TInterface, TImplementation>();
            if (_registry.ContainsKey(type))
                throw new InvalidOperationException($"Interface `{type.Name}` already has a registered service.");

            _registry.Add(type, implementation);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered `{typeof(TImplementation).FullName}` to `{type.FullName}`.");
        }

        private static Type ThrowArgumentExceptionIfInvalidTypes<TInterface, TImplementation>()
            where TImplementation : class
        {
            var typeInterface = typeof(TInterface);
            if (!typeInterface.IsInterface)
                throw new ArgumentException($"Generic type `{typeInterface.FullName}` is not an interface.");

            var typeImplementation = typeof(TImplementation);
            if (typeImplementation.IsAssignableFrom(typeInterface))
                throw new ArgumentException($"Generic type `{typeImplementation.FullName}` does not extend interface `{typeInterface.FullName}`.");

            return typeInterface;
        }

        public bool IsRegistered<TInterface>()
        {
            var type = typeof(TInterface);
            return _registry.ContainsKey(type);
        }
    }
}
