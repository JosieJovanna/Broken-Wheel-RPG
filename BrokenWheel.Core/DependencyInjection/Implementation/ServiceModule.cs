using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Time.Listeners;
using BrokenWheel.Core.Time;

namespace BrokenWheel.Core.DependencyInjection.Implementation
{
    public partial class Module
    {
        private readonly IDictionary<Type, ModuleServiceRegister> _registry = new Dictionary<Type, ModuleServiceRegister>();
        private readonly IList<string> _typeAndParamIsAutoHandled = new List<string>();
        private bool _isCompleted = false;

        /// <inheritdoc/>
        public IModule RegisterService<TService, TImpl>(
            Func<IModule, TImpl> serviceConstructor,
            bool shouldBuildImmediately = false,
            string parameter = null)
            where TImpl : class, TService
        {
            parameter = NormalizeParameter(parameter);
            var type = typeof(TService);
            var register = GetServiceRegister(type);
            register.RegisterConstructionFunction(serviceConstructor, parameter, shouldBuildImmediately);
            return this;
        }

        private ModuleServiceRegister GetServiceRegister(Type type)
        {
            if (_registry.ContainsKey(type))
                return _registry[type];

            var register = new ModuleServiceRegister(this, Logger, type);
            _registry.Add(type, register);
            return register;
        }

        /// <inheritdoc/>
        public void CompleteInitialRegistration()
        {
            if (_isCompleted)
                return;
            foreach (var regByType in _registry)
                BuildRegistryAndRegisterImmediates(regByType.Key, regByType.Value);
            _isCompleted = true;
            Logger.LogCategoryGood(LogCategory.DI, $"Built and registered all immediate services");
        }

        private void BuildRegistryAndRegisterImmediates(Type serviceType, ModuleServiceRegister registry)
        {
            var immediates = registry.BuildImmediates();
            foreach (var serviceByParam in immediates)
                HandleTimeAndEvents(serviceByParam.Value, serviceByParam.Key);
        }

        /// <inheritdoc/>
        public TService GetService<TService>(string parameter = null)
        {
            parameter = NormalizeParameter(parameter);
            var type = typeof(TService);
            if (!_registry.ContainsKey(type))
                throw new DependencyException($"Type `{type.Name}` has no registered services.");

            return GetService<TService>(parameter, type);
        }

        private TService GetService<TService>(string parameter, Type type)
        {
            var instance = _registry[type].GetInstance(parameter);
            Logger.LogCategory(LogCategory.DI, $"Got instance of type `{type.Name}` param `{parameter}`");
            HandleTimeAndEvents(instance, parameter);
            return Cast<TService>(instance);
        }

        private void HandleTimeAndEvents(object instance, string parameter)
        {
            var typeAndParam = $"{instance.GetType().FullName}.'{parameter}'";
            if (_typeAndParamIsAutoHandled.Contains(typeAndParam))
                return;
            HandleTimeAndEvents(instance);
            _typeAndParamIsAutoHandled.Add(typeAndParam);
        }

        private void HandleTimeAndEvents(object instance)
        {
            var implType = instance.GetType();
            SubscribeToHandledEvents(instance, implType);
            SubscribeToTimeEvents(instance, implType);
        }

        private void SubscribeToHandledEvents<TService>(TService implementation, Type implementationType)
        {
            if (!IsEventHandler(implementationType))
                return; // implements no event handlers
            GetEventAggregator().SubscribeToAllHandledEvents(implementation);
            Logger.LogCategory(LogCategory.DI, $"Subscribed `{implementationType.Name}` service to all handled events");
        }

        private static bool IsEventHandler(Type implementationType)
        {
            var implInterfaceNames = implementationType.GetInterfaces().Select(_ => _.Name);
            var allNames = string.Join("", implInterfaceNames);
            return allNames.Contains(typeof(EventHandler<object>).Name);
        }

        private void SubscribeToTimeEvents(object implementation, Type implementationType)
        {
            if (!IsImplementationTimeListener(implementation))
                return;
            AddTimeEventListeners(implementation, GetTimeService());
            Logger.LogCategory(LogCategory.DI, $"Subscribed `{implementationType.Name}` service to all time events.");
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

        private static string NormalizeParameter(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                return string.Empty;
            return parameter.Trim();
        }
    }
}
