using System;

namespace LorendisCore.Common.Damage
{
    /// <summary>
    ///     A type of damage event for linear damage rates; the most common type.
    ///     Durations of 0 or below throw a <see cref="ArgumentException"/>.
    ///     Damage values are in absolutes; values below 0 will be 0.
    ///     When damage calculations are made, rounds down the DPS, and applies any extra damage first.
    /// </summary>
    public class SimpleDamage : Damage
    {
        public double DPS => _dps.ToDouble();

        private readonly Fraction _dps;
        private Fraction _fractionDamage = new Fraction();

        /// <summary>
        ///     Creates a <see cref="Damage"/> that deals damage at a constant rate.
        ///     When rounding DPS, extra damage will be dealt on the first tick.
        /// </summary>
        public SimpleDamage(DamageType type, int amount, int duration)
            : base(type, amount, duration)
        {
            if (duration < 1)
                throw new ArgumentException("Damage duration must be non-zero; only instant damage may have a duration of 0.");

            _dps = new Fraction(amount, duration);
        }
        
        protected override int CalculateTick()
        {
            if (IsDone)
                return 0;
            
            var temp = _fractionDamage + _dps;
            var amount = (int) Math.Floor(temp.ToDouble());
            _fractionDamage = temp - amount;
            return amount;
        }

        public override string ToDataString() 
            => $"({Dealt}/{Total}){Type.GetName()}/({TimePassed}/{Duration})s@{_dps}DPS";

        protected override string ChildInfoString() 
            => $"{Dealt}/{Total} {Type.GetName()} damage dealt over {TimePassed}/{Duration} seconds at {_dps}DPS.";
    }
}
