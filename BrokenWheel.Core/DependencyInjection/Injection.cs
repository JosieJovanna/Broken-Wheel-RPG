using System;
using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.DependencyInjection
{
    public static class Injection
    {
        private static ILogger _logger;
        private static IModule _module;

        public static void SetModule(IModule module)
        {
            _module = module ?? throw new ArgumentNullException(nameof(module));
            _logger = module.GetLogger();
            _logger.LogCategory(LogCategory.DI, "Static Dependency Injection Initialized.");
        }

        public static IModule GetModule()
        {
            if (_module == null)
                throw new InvalidOperationException("RPG Dependency Injection has not yet been initialized.");
            return _module;
        }
    }
}
