using System;
using System.Collections.Generic;

namespace BrokenWheel.DependencyInjection
{
    public class Module
    {
        private readonly IDictionary<Type, object> _registry = new Dictionary<Type, object>();

        internal Module() { }

        /// <summary>
        /// Registers singleton interfaced services to be statically accessible through <see cref="DI"/>.
        /// </summary>
        /// <typeparam name="TInterface"> The interface that the implementation inherits. </typeparam>
        /// <typeparam name="TImplementation"> The implementation of the interface type. </typeparam>
        /// <param name="implementation"> The object to be registered. </param>
        public void Register<TInterface, TImplementation>(TImplementation implementation)
            where TImplementation : class
        {
            var type = ThrowArgumentExceptionIfInvalidTypes<TInterface, TImplementation>();
            if (_registry.ContainsKey(type))
                throw new InvalidOperationException($"Interface `{type.Name}` already has a registered service.");

            _registry.Add(type, implementation);
        }

        /// <summary>
        /// Gets singleton interfaced services to be statically accessible through <see cref="DI"/>.
        /// </summary>
        /// <typeparam name="TInterface"> The registered interface type. </typeparam>
        /// <returns> Instance of interface implementation. </returns>
        public TInterface Get<TInterface>()
        {
            var type = typeof(TInterface);
            if (!_registry.ContainsKey(type))
                throw new InvalidOperationException($"Interface `{type.Name}` doesn't have a registered service.");

            return (TInterface)_registry[type];
        }

        private static Type ThrowArgumentExceptionIfInvalidTypes<TInterface, TImplementation>()
            where TImplementation : class
        {
            var typeInterface = typeof(TInterface);
            if (!typeInterface.IsInterface)
                throw new ArgumentException($"Generic type `{typeInterface.Name}` is not an interface.");

            var typeImplementation = typeof(TImplementation);
            if (typeImplementation.IsAssignableFrom(typeInterface))
                throw new ArgumentException($"Generic type `{typeImplementation.Name}` does not extend interface `{typeInterface.Name}`.");

            return typeInterface;
        }
    }
}
