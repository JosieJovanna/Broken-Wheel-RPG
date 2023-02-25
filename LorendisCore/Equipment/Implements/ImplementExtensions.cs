using System;
using System.Linq;
using System.Collections.Generic;
using LorendisCore.Equipment.Implements.WeaponTypes;

namespace LorendisCore.Equipment.Implements
{
    public static class ImplementExtensions
    {
        public static bool IsTwoHanded(this IImplement implement)
        {
            if (implement == null) return false;
            var implementsTwoHandedInterface = implement.ImplementsInterface<ITwoHandedImplement>();
            var isVersatile = implement.TryCastToInterface<IVersatileImplement>(out var versatileImplement);
            return !isVersatile
                ? implementsTwoHandedInterface 
                : versatileImplement.IsTwoHandedGrip;
        }
        
        public static bool HasSpecial(this IImplement implement)
            => implement.ImplementsInterface<ISpecial>();

        public static bool IsVersatile(this IImplement implement)
            => implement.ImplementsInterface<IVersatileImplement>();

        public static bool IsReloadable(this IImplement implement) 
            => implement.ImplementsInterface<IReloadable>();

        /// <summary>
        /// Tries casting the object to another interface which it implements, if possible. Does not throw exceptions.
        /// </summary>
        /// <param name="result">The resulting cast, which is default or null if the cast fails.</param>
        /// <typeparam name="T">An interface which is implemented by the object.</typeparam>
        /// <returns>Whether the cast was successful.</returns>
        public static bool TryCastToInterface<T>(this IImplement implement, out T result)
        {
            try
            {
                result = implement.CastToInterface<T>();
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
        public static T CastToInterface<T>(this IImplement implement)
        {
            var implementsT = implement.ImplementsInterface<T>();
            if (!implementsT)
                throw new InvalidCastException($"The Implement does not inherit interface '{typeof(T)}'.");
            return (T)implement;
        }
        
        /// <typeparam name="T">Must be an interface.</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if T is not an interface.</exception>
        public static bool ImplementsInterface<T>(this IImplement implement)
        {
            if (implement == null)
                return false;
            if (!typeof(T).IsInterface)
                throw new InvalidOperationException($"Type '{typeof(T)}' is not an interface.");
            return implement.GetInterfaces().Contains(typeof(T));
        }

        private static IEnumerable<Type> GetInterfaces(this IImplement implement)
        {
            return implement.GetType().GetInterfaces().AsEnumerable();
        }
    }
}