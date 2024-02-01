using System;

namespace BrokenWheel.Core.Exceptions
{
    /// <summary>
    /// An exception thrown when a static service is not initialized.
    /// These services rely on in-engine Implementations, which communicate information back to the System in an easy-to-access, static way.
    /// </summary>
    public class StaticServiceNotInitializedException : Exception
    {
        public StaticServiceNotInitializedException(string nameOfService)
            : base($"The static service '{nameOfService}' has not been initialized during startup.")
        { }
    }
}
