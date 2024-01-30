using System;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.DependencyInjection
{
    public static class Injection
    {
        private static bool _isInitialized = false;
        private static ILogger _logger;
        private static Module _module;

        public static bool IsInitiated
        {
            get
            {
                return _isInitialized;
            }
        }

        public static void Initialize(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _module = new Module(_logger);
            _isInitialized = true;
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, "Static Dependency Injection Initialized.");
        }

        public static ILogger GetLogger()
        {
            if (!IsInitiated || _logger == null)
                throw new InvalidOperationException("Dependency Injection has not yet been initialized and/or the logger is null.");
            return _logger;
        }

        public static IModule GetModule()
        {
            if (_module == null)
                throw new InvalidOperationException("Dependency Injection has not yet been initialized.");
            return _module;
        }
    }
}
