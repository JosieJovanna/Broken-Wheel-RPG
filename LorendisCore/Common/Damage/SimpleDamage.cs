using System;
using System.Text;

namespace LorendisCore.Common.Damage
{
    /// <summary>
    ///   A type of damage event for linear damage rates; the most common type.
    ///   Durations of 0 or below throw a <see cref="ArgumentException"/>.
    ///   Damage values are in absolutes; values below 0 will be 0.
    ///   When damage calculations are made, rounds down the DPS, and applies any extra damage first.
    /// </summary>
    public class SimpleDamage : Damage
    {
        /// <summary>
        ///   Damage dealt on the first tick.
        /// </summary>
        public int OneTimeDamage => _oneTime;
        public int DPS => _dps;

        protected int _oneTime; // TODO: remove
        protected int _dps; // TODO: store as float and add each tick, dealing when it rolls over to full

        /// <summary>
        ///   Creates a <see cref="Damage"/> that deals damage at a constant rate.
        ///   When rounding DPS, extra damage will be dealt on the first tick.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="amount"></param>
        /// <param name="duration"></param>
        public SimpleDamage(DamageType type, int amount, int duration)
            : base(type, amount, duration)
        {
            if (duration < 1)
                throw new ArgumentException("Damage duration must be non-zero; only instant damage may have a duration of 0.");

            _dps = (int)Math.Floor((float)amount / duration);
            _oneTime = amount - _dps * duration;
        }


        protected override int CalculateTick()
        {
            return _time == 0 
                ? _dps + _oneTime 
                : _dps;
        }


        public override string ToDataString() 
            => $"({Dealt}/{_total}){Type.GetName()}/({_time}/{_duration})s@{_dps}/s";

        protected override string ChildInfoString() 
            => $"{Dealt}/{Total} {Type.GetName()} damage dealt over {TimePassed}/{Duration} seconds at {_dps}DPS.";
    }
}
