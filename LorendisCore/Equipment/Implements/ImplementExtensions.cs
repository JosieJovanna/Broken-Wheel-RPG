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
            return implement.ImplementsInterface<TwoHandedImplement>();
        }

        public static bool IsReloadable(this IImplement implement)
        {
            return implement.ImplementsInterface<IReloadable>();
        }

        private static bool ImplementsInterface<T>(this IImplement implement)
        {
            return implement.GetInterfaces().Contains(typeof(T));
        }

        private static IEnumerable<Type> GetInterfaces(this IImplement implement)
        {
            return implement.GetType().GetInterfaces().AsEnumerable();
        }
    }
}