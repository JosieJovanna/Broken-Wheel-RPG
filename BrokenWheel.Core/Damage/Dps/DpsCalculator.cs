using System;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Damage.Dps
{
    /// <summary>
    /// Abstract class used to 
    /// </summary>
    internal abstract class DpsCalculator : IDpsCalculator
    {
        public DamageType Type { get; }
        public bool IsDone { get => IsDoneCondition(); }
        public int TotalDamage { get; }
        public int DamageDealt { get => TotalDamage - RemainingDamage; }
        public int RemainingDamage { get; private set; }
        public int Duration { get; }
        public int SecondsPassed { get; private set; }
        public int TimeRemaining { get => IsDone ? 0 : Duration - SecondsPassed; }

        /// <exception cref="ArgumentException"> When total damage or duration are below zero. </exception>
        protected DpsCalculator(DamageType type, int totalDamage, int duration)
        {
            Type = type;
            TotalDamage = Validate.ThrowIfNotPositive(totalDamage, nameof(TotalDamage));
            Duration = Validate.ThrowIfNegative(duration, nameof(Duration));
            RemainingDamage = TotalDamage;
        }

        protected virtual bool IsDoneCondition() => RemainingDamage <= 0;

        public int Dps()
        {
            SecondsPassed++;
            if (IsDone)
                return 0;

            var amount = CalculateDps();
            RemainingDamage -= amount;
            return amount;
        }

        /// <summary>
        /// Calculates the damage to be dealt that second.
        /// Called only by <see cref="Dps"/>, which tracks time values and progresses the event.
        /// Thus, calculations done may be irreversible.
        /// Only called if not <see cref="IsDone"/>.
        /// </summary>
        protected abstract int CalculateDps();

        /// <returns>  A string containing variables and minimal formatting.  </returns>
        public abstract string ToDataString();

        /// <returns>  A string containing the variables of the extending class.  </returns>
        protected abstract string ChildInfoString();

        /// <returns>  A string verbosely describing the <see cref="DpsCalculator"/>.  </returns>
        public override string ToString() => ChildInfoString();
    }
}
