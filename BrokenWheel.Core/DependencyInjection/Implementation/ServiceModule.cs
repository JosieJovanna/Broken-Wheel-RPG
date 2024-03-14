using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Time.Listeners;
using BrokenWheel.Core.Time;

namespace BrokenWheel.Core.DependencyInjection.Implementation
{
    public partial class Module
    {
        private readonly IDictionary<Type, object> _serviceRegistry = new Dictionary<Type, object>();
        private readonly IDictionary<Type, Func<IModule, object>> _serviceFunctions = new Dictionary<Type, Func<IModule, object>>();
        private readonly IDictionary<Type, Func<IModule, object>> _immediateServiceFunctions = new Dictionary<Type, Func<IModule, object>>();
        private readonly IList<Type> _eventHandled = new List<Type>();
        private readonly IList<Type> _timeHandled = new List<Type>();

        private bool _isCompleted = false;
        private bool _isConstructing = false;
        private Type _constructRoot = null;

        #region Function Registration

        /// <inheritdoc/>
        public IModule RegisterService<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, bool shouldBuildImmediately = false)
            where TImpl : class, TService
        {
            var type = typeof(TService);
            ThrowExceptionTServiceAlreadyRegistered(type);
            if (shouldBuildImmediately)
                RegisterImmediateServiceFunction<TService, TImpl>(serviceConstructor, type);
            else
                _serviceFunctions.Add(type, serviceConstructor);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered injection function for `{type.Name}`");
            return this;
        }

        private void ThrowExceptionTServiceAlreadyRegistered(Type type)
        {
            if (_serviceRegistry.ContainsKey(type))
                LogAndThrow($"Service `{type.Name}` already has a registered instance.");
            if (_serviceFunctions.ContainsKey(type))
                LogAndThrow($"Service `{type.Name}` already has a registered injection function.");
            if (_immediateServiceFunctions.ContainsKey(type))
                LogAndThrow($"Service `{type.Name}` already has a registered immediate injection function.");
        }

        private void RegisterImmediateServiceFunction<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, Type type)
            where TImpl : class, TService
        {
            if (_isCompleted)
                InvokeFunctionAndRegisterType<TService, TImpl>(serviceConstructor, type);
            else
                _immediateServiceFunctions.Add(type, serviceConstructor);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered injection function for `{type.Name}`");
        }

        private void InvokeFunctionAndRegisterType<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, Type type)
            where TImpl : class, TService
        {
            var instance = Cast<TImpl>(serviceConstructor.Invoke(this));
            _serviceRegistry.Add(type, instance);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered and called injection function for `{type.Name}`");
        }

        #endregion

        #region Get Services

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
            var method = GetType().GetMethod(nameof(CallConstructorAndRegisterInstance),
                BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var kvp in _immediateServiceFunctions)
            {
                var genericMethod = method.MakeGenericMethod(kvp.Key);
                genericMethod.Invoke(this, new object[] { kvp.Value, kvp.Key });
            }
        }

        /// <inheritdoc/>
        public TService GetService<TService>()
        {
            var type = typeof(TService);
            if (!_serviceRegistry.ContainsKey(type))
                CallConstructorAndRegisterInstance<TService>(GetServiceFunction(type), type);

            return Cast<TService>(_serviceRegistry[type]);
        }

        #endregion

        #region Injection Function Service

        private Func<IModule, object> GetServiceFunction(Type type)
        {
            if (_serviceFunctions.ContainsKey(type))
                return _serviceFunctions[type];
            else if (_immediateServiceFunctions.ContainsKey(type))
                return _immediateServiceFunctions[type];
            LogAndThrow($"Service `{type.Name}` does not have a registered instance or injection function.");
            return null;
        }

        private void CallConstructorAndRegisterInstance<TService>(Func<IModule, object> function, Type type)
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
                LogAndThrow($"Circular dependency injection logic for `{type.Name}`.");
        }

        private TService CallInjectionFunction<TService>(Type type, Func<IModule, object> function)
        {
            var implementation = Cast<TService>(function.Invoke(this));
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

        #endregion

        #region Instance Registration

        /// <summary>
        /// Registers an instance of the service and automatically subscribes it to any relevant services.
        /// </summary>
        private IModule RegisterService<TService>(TService implementation)
        {
            var type = typeof(TService);
            var implementationType = implementation.GetType();
            if (_serviceRegistry.ContainsKey(type))
                _logger.LogCategoryWarning(LogCategory.DEPENDENCY_INJECTION, $"Service `{type.Name}` already has a registered instance");
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
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Registered instance of `{implementation.GetType().FullName}` to `{type.Name}`");
        }

        private void SubscribeToHandledEvents<TService>(TService implementation, Type implementationType)
        {
            if (!IsEventHandler(implementationType) || _eventHandled.Contains(implementationType))
                return; // implements no event handlers
            var eventAggregator = GetEventAggregator();
            eventAggregator.SubscribeToAllHandledEvents(implementation);
            _eventHandled.Add(implementationType);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Subscribed `{implementationType.Name}` service to all handled events");
        }

        private static bool IsEventHandler(Type implementationType)
        {
            var implInterfaceNames = implementationType.GetInterfaces().Select(_ => _.Name);
            var allNames = string.Join("", implInterfaceNames);
            return allNames.Contains(typeof(EventHandler<object>).Name);
        }

        private void SubscribeToTimeEvents(object implementation, Type implementationType)
        {
            if (_timeHandled.Contains(implementationType) || !IsImplementationTimeListener(implementation))
                return;
            var timeService = GetTimeService();
            AddTimeEventListeners(implementation, timeService);
            _timeHandled.Add(implementationType);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Subscribed `{implementationType.Name}` service to all time events.");
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
    }
}
