using System;
using BrokenWheel.Core.Utilities;
using BrokenWheel.Math;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.Core.Damage.Dps
{
    /// <summary>
    /// A type of damage event for linear damage rates; the most common type.
    /// Durations of 0 or below throw a <see cref="ArgumentException"/>.
    /// Damage values are in absolutes; values below 0 will be 0.
    /// When damage calculations are made, rounds down the DPS, and applies any extra damage first.
    /// </summary>
    internal class SimpleDpsCalculator : DpsCalculator
    {
        public double DPS { get => _dps.ToDouble(); }

        private readonly Fraction _dps;
        private Fraction _overflow = new Fraction();

        /// <summary>
        /// Creates a <see cref="DpsCalculator"/> that deals damage at a constant rate.
        /// When rounding DPS, extra damage will be dealt on the first tick.
        /// </summary>
        /// <param name="type">  The type of damage dealt.  </param>
        /// <param name="amount">
        /// The amount of damage dealt.
        /// Cannot be zero or less than zero; such values should be filtered out when processing damage events.
        /// </param>
        /// <param name="duration">
        /// The amount of time it takes for the damage to resolve.
        /// Cannot be less than 1; such values should be <see cref="InstantDpsCalculator"/>.
        /// </param>
        public SimpleDpsCalculator(DamageType type, int amount, int duration)
            : base(type, amount, duration)
        {
            Validate.ThrowIfNotPositive(duration, nameof(Duration));

            _dps = new Fraction(amount, duration);
        }
        
        protected override int CalculateDps()
        {
            var temp = _overflow + _dps;
            var amount = MathUtil.LowerDoubleToInt(temp.ToDouble());
            _overflow = temp - amount;
            return amount;
        }

        public override string ToDataString() 
            => $"[({DamageDealt}/{TotalDamage}){Type.GetName()}/({SecondsPassed}/{Duration})s@{_dps}DPS]";

        protected override string ChildInfoString() 
            => $"{DamageDealt}/{TotalDamage} {Type.GetName()} damage dealt over {SecondsPassed}/{Duration} seconds at {_dps}DPS";
    }
}
