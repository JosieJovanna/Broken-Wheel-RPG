using System;
using LorendisCore.Utilities;

namespace LorendisCore.Common.Stats
{
    public class SimpleStat : IStatObject
    {
        public readonly string _name;
        private int _val;
        private int _mod;

        public SimpleStat(string name, int value, int mod)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(_name)} must not be null or whitespace.");
            _name = name;
            _val = value;
            _mod = mod;
        }

        public string GetStatName() => _name;
        
        /// <summary>
        /// Gets the effective value of the stat, including the modifier.
        /// Cannot be less than zero.
        /// </summary>
        public int GetEffectiveValue() => Util.NonNeg(_val + _mod);

        /// <summary>
        /// Sets the effective value of the stat to the given value minus the modifier.
        /// </summary>
        public void SetEffectiveValue(int val) => SetValue(val - _mod);

        public int GetValue() => _val;
        public void SetValue(int val) => _val = Util.NonNeg(val);
        public void AddValue(int add) => SetValue(_val + add);

        public int GetModifier() => _mod;
        public void SetModifier(int val) => _mod = val;
        public void AddModifier(int add) => _mod += add;
    }
}
