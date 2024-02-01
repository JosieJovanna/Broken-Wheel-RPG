using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Utilities;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.Core.Damage.Processing
{
    /// <summary>
    /// Acts as a dictionary of <see cref="DamageType"/>s to damage amounts, with less freedom.
    /// Only positive values can be added, and it will return 0 if damage is unspecified.
    /// Extends <see cref="IEnumerable"/>
    /// </summary>
    public class DamageMap : IEnumerable<KeyValuePair<DamageType, int>>
    {
        private readonly IDictionary<DamageType, int> _map = new Dictionary<DamageType, int>();

        /// <summary>
        /// Creates an empty <see cref="DamageMap"/>.
        /// </summary>
        public DamageMap()
        { }

        /// <summary>
        /// Creates a damage map from a <see cref="DamageType"/> to amount (int) dictionary by copying values.
        /// Will not include non-positive values.
        /// </summary>
        public DamageMap(IDictionary<DamageType, int> map)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));

            foreach (var pair in map)
                if (pair.Value > 0)
                    _map.Add(pair.Key, pair.Value);
        }


        /// <summary>
        /// Whether the specified <see cref="DamageType"/> has a nonzero amount of damage.
        /// </summary>
        public bool ContainsType(DamageType key)
        {
            return _map.ContainsKey(key);
        }

        /// <summary>
        /// Gets the amount of damage for the given type, or 0 if unspecified.
        /// </summary>
        public int Get(DamageType type)
        {
            return ContainsType(type)
              ? _map[type]
              : 0;
        }


        /// <summary>
        /// Adds the damage amount to the dictionary, modifying the value instead if the key already exists.
        /// Negative amounts will not be added.
        /// </summary>
        public void Add(DamageType type, int amount)
        {
            if (amount <= 0)
                return;

            if (ContainsType(type))
                _map[type] += amount;
            else
                _map.Add(type, amount);
        }

        /// <summary>
        /// Adds another damage map's value to <b>this map</b>.
        /// Does not modify other map.
        /// </summary>
        /// <returns>  The map the action is performed on, for fluid calling.  </returns>
        public DamageMap Add(DamageMap other)
        {
            foreach (var pair in other)
                if (pair.Value > 0)
                    other.Add(pair.Key, pair.Value);
            return this;
        }

        /// <summary>
        /// Removes the value for the specified <see cref="DamageType"/>.
        /// </summary>
        public void Remove(DamageType type)
        {
            if (ContainsType(type))
                _map.Remove(type);
        }


        /// <summary>
        /// Creates a new map for interpolating damage over time.
        /// Damage is floored.
        /// </summary>
        /// <param name="deltaTime">  Fraction of a second. Must be less than or equal to one.  </param>
        /// <returns>  A new <see cref="DamageMap"/>.  </returns>
        public DamageMap Fraction(double deltaTime)
        {
            if (deltaTime > 1)
                throw new ArgumentException($"{nameof(deltaTime)} must be less than one second.");

            return new DamageMap(_map.ToDictionary(
                k => k.Key,
                v => MathUtil.LowerDoubleToInt(v.Value * deltaTime)));
        }

        /// <summary>
        /// Returns the amount of damage by type <i>present in this map</i> that has not been adequately dealt by the other.
        /// The other map is expected to have the same keys and smaller values than this one;
        /// any damage that would come out negative is set to 0.
        /// </summary>
        /// <returns>  The difference between this <see cref="DamageMap"/> and another map, as a new object.  </returns>
        public DamageMap Difference(DamageMap other)
        {
            return new DamageMap(_map.ToDictionary(
                k => k.Key,
                v => Get(v.Key) - other.Get(v.Key)));
        }


        public static DamageMap operator +(DamageMap a, DamageMap b) => new DamageMap().Add(a).Add(b);
        public static DamageMap operator -(DamageMap a, DamageMap b) => a.Difference(b);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<KeyValuePair<DamageType, int>> GetEnumerator() => _map.GetEnumerator();
    }
}
