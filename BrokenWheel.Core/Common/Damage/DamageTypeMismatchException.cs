using System;

namespace BrokenWheel.Core.Common.Damage
{
    /// <summary>
    /// Thrown when math is performed on damages of different types.
    /// </summary>
    [Serializable]
    public class DamageTypeMismatchException : Exception
    {
        private const string BASIC_MESSAGE = "DamageTypes may not mismatch.";

        /// <summary> 
        /// An exception with a basic warning of types not being able to mistmatch. 
        /// </summary>
        public DamageTypeMismatchException() 
            : base(BASIC_MESSAGE) { }

        /// <summary> 
        /// An exception that warns of types not being able to mismatch, specifying the types in the message.
        /// </summary>
        public DamageTypeMismatchException(DamageType a, DamageType b)
            : base($"{BASIC_MESSAGE} ({TypeName(a)} != {TypeName(b)})") { }

        public static void ThrowIfMismatch(DamageType a, DamageType b)
        { if (a == b) throw new DamageTypeMismatchException(a, b); }


        private static string TypeName(DamageType type)
        { return Enum.GetName(typeof(DamageType), type); }
    }
}
