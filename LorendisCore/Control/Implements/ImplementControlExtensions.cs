using System;
using System.Linq;
using System.Collections.Generic;

namespace LorendisCore.Control.Implements
{
    public static class ImplementControlExtensions
    {
        public static bool IsTwoHanded(this IImplementControl implementControl)
        {
            if (implementControl == null) return false;
            var implementsTwoHandedInterface = implementControl.ImplementsInterface<ITwoHandedImplementControl>();
            var isVersatile = implementControl.TryCastToInterface<IVersatileImplementControl>(out var versatileImplement);
            return !isVersatile
                ? implementsTwoHandedInterface 
                : versatileImplement.IsTwoHandedGrip;
        }
        
        public static bool HasSpecial(this IImplementControl implementControl)
            => implementControl.ImplementsInterface<ISpecialControl>();

        public static bool IsVersatile(this IImplementControl implementControl)
            => implementControl.ImplementsInterface<IVersatileImplementControl>();

        public static bool IsReloadable(this IImplementControl implementControl) 
            => implementControl.ImplementsInterface<IReloadableControl>();

        /// <summary>
        /// Tries casting the object to another interface which it implements, if possible. Does not throw exceptions.
        /// </summary>
        /// <param name="result">The resulting cast, which is default or null if the cast fails.</param>
        /// <typeparam name="T">An interface which is implemented by the object.</typeparam>
        /// <returns>Whether the cast was successful.</returns>
        public static bool TryCastToInterface<T>(this IImplementControl implementControl, out T result)
        {
            try
            {
                result = implementControl.CastToInterface<T>();
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
        
        /// <summary>
        /// Tries casting the implement to another interface, but will throw an exception if it cannot be cast.
        /// For safe casting, use <see cref="TryCastToInterface{T}"/>, which will not throw exceptions.
        /// </summary>
        /// <typeparam name="T">An interface which the object implements.</typeparam>
        /// <exception cref="InvalidCastException"></exception>
        public static T CastToInterface<T>(this IImplementControl implementControl)
        {
            var implementsT = implementControl.ImplementsInterface<T>();
            if (!implementsT)
                throw new InvalidCastException($"The Implement does not inherit interface '{typeof(T)}'.");
            return (T)implementControl;
        }
        
        /// <typeparam name="T">Must be an interface.</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if T is not an interface.</exception>
        public static bool ImplementsInterface<T>(this IImplementControl implementControl)
        {
            if (implementControl == null)
                return false;
            
            if (!typeof(T).IsInterface)
                throw new InvalidOperationException($"Type '{typeof(T)}' is not an interface.");
            return implementControl.GetInterfaces().Contains(typeof(T));
        }

        private static IEnumerable<Type> GetInterfaces(this IImplementControl implementControl)
        {
            return implementControl.GetType().GetInterfaces().AsEnumerable();
        }
    }
}