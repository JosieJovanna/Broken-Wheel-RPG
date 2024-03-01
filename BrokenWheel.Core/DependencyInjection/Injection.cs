using System;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.DependencyInjection
{
    public static class Injection
    {
        private static ILogger _logger;
        private static Module _module;

        public static void Initialize(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _module = new Module(_logger);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, "Static Dependency Injection Initialized.");
        }

        public static IModule GetModule()
        {
            if (_module == null)
                throw new InvalidOperationException("RPG Dependency Injection has not yet been initialized.");
            return _module;
        }
    }
}
