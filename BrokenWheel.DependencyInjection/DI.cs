using System;

namespace BrokenWheel.DependencyInjection
{
    public static class DI
    {
        private static Module _module;

        public static void Initialize()
        {

        }

        public static Module GetModule()
        {
            if (_module == null)
                throw new InvalidOperationException("Dependency Injection has not yet been initialized.");
            return _module;
        }
    }
}
