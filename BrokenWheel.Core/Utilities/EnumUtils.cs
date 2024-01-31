using System;
using System.Collections.Generic;

namespace BrokenWheel.Core.Utilities
{
    public static class EnumUtils
    {
        /// <summary>
        /// Gets all values for a given enum type. Useful for instantiating <see cref="Stats.Enum.Stat"/>s.
        /// </summary>
        /// <typeparam name="TEnum"> The enum whose values should be gathered. </typeparam>
        /// <exception cref="ArgumentException"> If TEnum is not an enum. </exception>
        public static IList<TEnum> GetAllEnumValues<TEnum>()
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException($"Generic type {type.FullName} is not an enumerable.");

            var list = new List<TEnum>();
            foreach (var value in type.GetEnumValues())
                list.Add((TEnum)value);
            return list;
        }
    }
}
