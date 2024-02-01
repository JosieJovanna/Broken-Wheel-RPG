using System;

namespace BrokenWheel.Math
{
    public sealed class FractionException : Exception
    {
        public FractionException(string message)
            : base(message) { }

        public FractionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
