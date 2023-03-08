using System;
using System.Linq;
using System.Collections.Generic;
using LorendisCore.Control.Implements;

namespace LorendisCore.Control.Extensions
{
    public static class ImplementControlExtensions
    {
        public static bool IsTwoHanded(this IBaseControl control)
        {
            if (control == null) return false;
            var implementsTwoHandedInterface = control.ImplementsInterface<ITwoHandedControl>();
            var isVersatile = control.TryCastToInterface<IVersatileControl>(out var asVersatile);
            return !isVersatile
                ? implementsTwoHandedInterface 
                : asVersatile.IsTwoHandedGrip;
        }

        /// <summary>
        /// Tries casting the object to another interface which it implements, if possible. Does not throw exceptions.
        /// </summary>
        /// <param name="result">The resulting cast, which is default or null if the cast fails.</param>
        /// <typeparam name="T">An interface which is implemented by the object.</typeparam>
        /// <returns>Whether the cast was successful.</returns>
        public static bool TryCastToInterface<T>(this IBaseControl control, out T result)
        {
            try
            {
                result = control.CastToInterface<T>();
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
        public static T CastToInterface<T>(this IBaseControl control)
        {
            var implementsT = control.ImplementsInterface<T>();
            if (!implementsT)
                throw new InvalidCastException($"The Implement does not inherit interface '{typeof(T)}'.");
            return (T)control;
        }
        
        /// <typeparam name="T">Must be an interface.</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if T is not an interface.</exception>
        public static bool ImplementsInterface<T>(this IBaseControl control)
        {
            if (control == null)
                return false;
            
            if (!typeof(T).IsInterface)
                throw new InvalidOperationException($"Type '{typeof(T)}' is not an interface.");
            return control.GetInterfaces().Contains(typeof(T));
        }

        private static IEnumerable<Type> GetInterfaces(this IBaseControl control)
        {
            return control.GetType().GetInterfaces().AsEnumerable();
        }
    }
}