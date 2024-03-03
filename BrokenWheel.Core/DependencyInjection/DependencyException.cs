using System;

namespace BrokenWheel.Core.DependencyInjection
{
    /// <summary>
    /// An exception thrown by <see cref="Injection"/> and <see cref="IModule"/>
    /// for dependency-injection related errors.
    /// </summary>
    public class DependencyException : Exception
    {
        public DependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public DependencyException(string message)
            : base(message)
        { }
    }
}
