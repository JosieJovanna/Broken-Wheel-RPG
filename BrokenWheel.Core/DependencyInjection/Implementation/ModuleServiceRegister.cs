using System;
using System.Collections.Generic;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.DependencyInjection.Implementation
{
    internal partial class ModuleServiceRegister
    {
        private readonly IModule _module;
        private readonly ILogger _logger;

        private readonly IDictionary<string, object> _parameterizedInstanceRegistry = new Dictionary<string, object>();
        private readonly IDictionary<string, Func<IModule, object>> _parameterizedConstructorFxs = new Dictionary<string, Func<IModule, object>>();
        private readonly IDictionary<string, Func<IModule, object>> _parameterizedImmediateFxs = new Dictionary<string, Func<IModule, object>>();
        private bool _areImmediatesBuilt = false;
        private bool _isBuilding = false;

        public Type Type { get; }

        public ModuleServiceRegister(
            IModule module,
            ILogger logger,
            Type type,
            object defaultInstance)
            : this(module, logger, type)
        {
            _parameterizedInstanceRegistry.Add(string.Empty, defaultInstance);
            _parameterizedConstructorFxs.Add(string.Empty, (IModule _) => defaultInstance);
            _logger.LogCategory(LogCategory.DI, $"Registered default instance of `{Type.Name}`");
        }

        public ModuleServiceRegister(
            IModule module,
            ILogger logger,
            Type type)
        {
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Calls all constructors set to instantiate right away.
        /// </summary>
        public void BuildImmediates()
        {
            if (_areImmediatesBuilt || _parameterizedImmediateFxs.Count < 1)
                return;
            foreach (var fxByParam in _parameterizedImmediateFxs)
                BuildAndRegister(fxByParam.Key, fxByParam.Value);
            _areImmediatesBuilt = true;
            _logger.LogCategory(LogCategory.DI, $"Built all immediate instances of `{Type.Name}`");
        }

        /// <summary>
        /// Registers a constructor function for the given parameter.
        /// </summary>
        /// <param name="constructorFunction"> The function used to build the instance. </param>
        /// <param name="parameter"> The parameter to register as. If null or white space, will be the default implementation (null by default). </param>
        /// <param name="shouldBuildImmediately"> Whether the parameterized function should be built right away (false by default). </param>
        /// <exception cref="DependencyException"> If the given parameter already has an instance. </exception>
        public void RegisterConstructionFunction(
            Func<IModule, object> constructorFunction,
            string parameter,
            bool shouldBuildImmediately)
        {
            if (_parameterizedInstanceRegistry.ContainsKey(parameter))
                throw new DependencyException($"There is already a registered instance for param `{parameter}`");

            RegisterFx(constructorFunction, parameter, shouldBuildImmediately);
            _logger.LogCategory(LogCategory.DI, $"Registered function for type `{Type.Name}`, param `{parameter}`{(shouldBuildImmediately ? " (immediate build)" : "")}");
        }

        private void RegisterFx(
            Func<IModule, object> constructorFunction,
            string parameter,
            bool shouldBuildImmediately)
        {
            _parameterizedConstructorFxs.Add(parameter, constructorFunction);
            if (shouldBuildImmediately)
                if (_areImmediatesBuilt)
                    BuildAndRegister(parameter, constructorFunction);
                else
                    _parameterizedImmediateFxs.Add(parameter, constructorFunction);
        }

        /// <summary>
        /// Gets an instance for the given parameter.
        /// If not already built, builds it and registers the instance.
        /// If the parameter does not have a set constructor, will use the default one.
        /// </summary>
        public object GetInstance(string parameter)
        {
            if (_parameterizedInstanceRegistry.ContainsKey(parameter))
                return _parameterizedInstanceRegistry[parameter];
            return BuildAndRegister(parameter);
        }

        private object BuildAndRegister(string parameter)
        {
            var constructorFx = _parameterizedConstructorFxs.ContainsKey(parameter)
                            ? _parameterizedConstructorFxs[parameter]
                            : _parameterizedConstructorFxs[""];
            return BuildAndRegister(parameter, constructorFx);
        }

        private object BuildAndRegister(string parameter, Func<IModule, object> constructorFunction)
        {
            if (_isBuilding)
                throw new DependencyException($"Dependency injection loop in type `{Type.Name}`.");
            var instance = Build(constructorFunction);
            _parameterizedInstanceRegistry.Add(parameter, instance);
            _logger.LogCategory(LogCategory.DI, $"Built and registered `{Type.Name}` instance for param `{parameter}`");
            return instance;
        }

        private object Build(Func<IModule, object> constructorFunction)
        {
            _isBuilding = true;
            var instance = constructorFunction.Invoke(_module);
            _isBuilding = false;
            return instance;
        }
    }
}
