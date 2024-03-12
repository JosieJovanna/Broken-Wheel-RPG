using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Events.Handling;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Time;
using BrokenWheel.Core.Time.Listeners;

namespace BrokenWheel.Core.DependencyInjection
{
    public class Module : IModule
    {
        private readonly ILogger _logger;
        private readonly IDictionary<Type, ISettings> _settingsRegistry = new Dictionary<Type, ISettings>();
        private readonly IDictionary<Type, object> _serviceRegistry = new Dictionary<Type, object>();
        private readonly IDictionary<Type, Func<IModule, object>> _serviceFunctions = new Dictionary<Type, Func<IModule, object>>();
        private readonly IDictionary<Type, Func<IModule, object>> _immediateServiceFunctions = new Dictionary<Type, Func<IModule, object>>();
        private readonly IList<Type> _eventHandled = new List<Type>();
        private readonly IList<Type> _timeHandled = new List<Type>();

        private bool _isCompleted = false;
        private bool _isConstructing = false;
        private Type _constructRoot = null;

        /// <summary>
        /// The default implementation for <see cref="IModule"/> which only needs a <see cref="ILogger"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> If logger is null. </exception>
        public Module(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceRegistry.Add(typeof(ILogger), _logger);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Initialized {typeof(Module)}.");
        }

        /// <inheritdoc/>
        public ILogger GetLogger() => _logger;

        #region Get Service

        /// <inheritdoc/>
        public TService GetService<TService>()
        {
            var type = typeof(TService);
            if (!_serviceRegistry.ContainsKey(type))
                GetFunctionServiceAndRegisterInstance<TService>(type);

            return Cast<TService>(_serviceRegistry[type]);
        }

        private void GetFunctionServiceAndRegisterInstance<TService>(Type type)
        {
            Func<IModule, object> function;
            if (_serviceFunctions.ContainsKey(type))
                function = _serviceFunctions[type];
            else if (_immediateServiceFunctions.ContainsKey(type))
                function = _immediateServiceFunctions[type];
            else
            {
                LogAndThrow($"Service `{type.Name}` does not have a registered instance or injection function.");
                return;
            }
            RegisterServiceInstanceFromFunction<TService>(type, function);
        }

        private void RegisterServiceInstanceFromFunction<TService>(Type type, Func<IModule, object> function)
        {
            HandleLoopLogic(type);
            var serviceImpl = CallInjectionFunction<TService>(type, function);
            UnregisterFunction(type);
            RegisterService(serviceImpl);
        }

        private void HandleLoopLogic(Type type)
        {
            if (!_isConstructing)
            {
                _isConstructing = true;
                _constructRoot = type;
            }
            else if (_constructRoot == type)
                LogAndThrow($"Circular dependency injection logic for {type.Name}.");
        }

        private TService CallInjectionFunction<TService>(Type type, Func<IModule, object> function)
        {
            var implementation = Cast<TService>(function.Invoke(this));
            UnregisterFunction(type);
            _isConstructing = false;
            _constructRoot = null;
            return implementation;
        }

        private void UnregisterFunction(Type type)
        {
            if (_serviceFunctions.ContainsKey(type))
                _serviceFunctions.Remove(type);
            if (_immediateServiceFunctions.ContainsKey(type))
                _immediateServiceFunctions.Remove(type);
        }

        /// <summary>
        /// Tries casting the given object to the specified type. Wraps exception and logs if cannot cast.
        /// </summary>
        private TCast Cast<TCast>(object instance)
        {
            try
            {
                return (TCast)instance;
            }
            catch (Exception e)
            {
                LogAndThrow($"Could not cast {instance.GetType().Name} to {typeof(TCast).Name}.", e);
                throw;
            }
        }

        #endregion

        #region Register Service

        /// <inheritdoc/>
        public void CompleteInitialRegistration()
        {
            if (_isCompleted)
                return;
            _isCompleted = true;
            BuildAllImmediates();
        }

        private void BuildAllImmediates()
        {
            var method = GetType().GetMethod(nameof(RegisterServiceInstanceFromFunction),
                BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var kvp in _immediateServiceFunctions)
            {
                var genericMethod = method.MakeGenericMethod(kvp.Key);
                genericMethod.Invoke(this, new object[] { kvp.Key, kvp.Value });
            }
        }

        /// <inheritdoc/>
        public IModule RegisterService<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, bool shouldBuildImmediately = false)
            where TImpl : class, TService
        {
            var type = typeof(TService);
            if (_serviceRegistry.ContainsKey(type))
                LogAndThrow($"Service `{type.Name}` already has a registered instance.");
            if (_serviceFunctions.ContainsKey(type))
                LogAndThrow($"Service `{type.Name}` already has a registered injection function.");
            if (_immediateServiceFunctions.ContainsKey(type))
                LogAndThrow($"Service `{type.Name}` already has a registered immediate injection function.");
            return RegisterServiceFunction<TService, TImpl>(serviceConstructor, shouldBuildImmediately);
        }

        private IModule RegisterServiceFunction<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, bool shouldBuildImmediately)
            where TImpl : class, TService
        {
            var type = typeof(TService);
            if (shouldBuildImmediately)
                RegisterImmediateServiceFunction<TService, TImpl>(serviceConstructor, type);
            else
                _serviceFunctions.Add(type, serviceConstructor);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered injection function for {type.Name}");
            return this;
        }
        private void RegisterImmediateServiceFunction<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, Type type)
            where TImpl : class, TService
        {
            if (_isCompleted)
                BuildImmediatelyAndReturnModule<TService, TImpl>(serviceConstructor, type);
            else
                BuildOnComplete<TService, TImpl>(serviceConstructor, type);
        }

        private void BuildImmediatelyAndReturnModule<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, Type type)
            where TImpl : class, TService
        {
            var instance = Cast<TImpl>(serviceConstructor.Invoke(this));
            _serviceRegistry.Add(type, instance);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered and called injection function for {type.Name}");
        }

        private void BuildOnComplete<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, Type type)
            where TImpl : class, TService
        {
            _immediateServiceFunctions.Add(type, serviceConstructor);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered and will call injection function for {type.Name}");
        }

        /// <inheritdoc/>
        private IModule RegisterService<TService>(TService implementation)
        {
            var type = typeof(TService);
            var implementationType = implementation.GetType();
            if (_serviceRegistry.ContainsKey(type))
                _logger.LogCategoryWarning(LogCategory.DEPENDENCY_INJECTION, $"Service `{type.Name}` already has a registered instance.");
            else
                RegisterInterfaceAndImplTypes(implementation, type);
            SubscribeToHandledEvents(implementation, implementationType);
            SubscribeToTimeEvents(implementation, implementationType);
            return this;
        }

        private void RegisterInterfaceAndImplTypes<TService>(TService implementation, Type type)
        {
            RegisterType(implementation, type);
            var implementationType = implementation.GetType();
            if (type != implementationType)
                RegisterType(implementation, implementationType);
        }

        private void RegisterType(object implementation, Type type)
        {
            if (_serviceRegistry.ContainsKey(type))
                return;
            _serviceRegistry.Add(type, implementation);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered `{type.Name}` to instance of {implementation.GetType().FullName}.");
        }

        private void SubscribeToHandledEvents<TService>(TService implementation, Type implementationType)
        {
            if (!DoesTypeImplement<IEventHandler<GameEvent>>(implementationType) || _eventHandled.Contains(implementationType))
                return; // implements no event handlers
            var eventAggregator = GetService<IEventAggregator>();
            eventAggregator.SubscribeToAllHandledEvents(implementation);
            _eventHandled.Add(implementationType);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Subscribed {implementationType.Name} service to all handled events.");
        }

        private static bool DoesTypeImplement<TInterface>(Type implementationType)
        {
            var implInterfaceNames = implementationType.GetInterfaces().Select(_ => _.Name);
            var allNames = string.Join("", implInterfaceNames);
            return allNames.Contains(typeof(TInterface).Name);
        }

        private void SubscribeToTimeEvents(object implementation, Type implementationType)
        {
            if (_timeHandled.Contains(implementationType) || !IsImplementationTimeListener(implementation))
                return;
            var timeService = GetService<ITimeService>();
            AddTimeEventListeners(implementation, timeService);
            _timeHandled.Add(implementationType);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Subscribed {implementationType.Name} service to all time events.");
        }

        private static bool IsImplementationTimeListener(object implementation)
        {
            return implementation is IOnTickTime
                || implementation is IOnRealTime
                || implementation is IOnCalendarTime;
        }

        private static void AddTimeEventListeners(object implementation, ITimeService timeService)
        {
            if (implementation is IOnTickTime tickTime)
                timeService.AddTickTimeFx(tickTime.OnTickTime);
            if (implementation is IOnRealTime realTime)
                timeService.AddRealTimeFx(realTime.OnRealTime);
            if (implementation is IOnCalendarTime calendarTime)
                timeService.AddCalendarTimeFx(calendarTime.OnCalendarTime);
        }

        #endregion

        #region Settings

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

        #endregion

        /// <summary>
        /// Throws an exception after logging it.
        /// </summary>
        private void LogAndThrow(string message, Exception innerException = null)
        {
            _logger.LogCategoryError(LogCategory.DEPENDENCY_INJECTION, message);
            throw new DependencyException(message, innerException);
        }
    }
}
