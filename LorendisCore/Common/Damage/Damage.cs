using System;
using LorendisCore.Utilities;

namespace LorendisCore.Common.Damage
{
    /// <summary>
    /// Abstract class which causes some damage over time. Values can only be positive.
    /// </summary>
    public abstract class Damage
    {
        public readonly DamageType Type;
        public readonly int Duration;
        public readonly int Total;
        public int Remaining { get; protected set; }
        public int TimePassed { get; protected set; } = 0;
        public bool IsDone => IsDoneCondition();
        public int Dealt => Total - Remaining;
        public int TimeRemaining => IsDone 
            ? 0 
            : Duration - TimePassed;


        /// <summary>
        /// Sets <see cref="Type"/> and internal trackers.
        /// </summary>
        /// <param name="type">  The type of damage dealt.  </param>
        /// <param name="total">  
        /// Total damage dealt over the unit's lifetime. 
        /// Throws an <see cref="ArgumentException"/> if 0 or lower;
        /// such values should be filtered out during processing of damage events.
        /// </param>
        /// <param name="duration">
        /// How long it takes to deal the total damage, in seconds.
        /// Throws an <see cref="ArgumentException"/> if 0 or lower;
        /// such values should be filtered out during processing of damage events.
        /// </param>
        protected Damage(DamageType type, int total, int duration)
        {
            Type = type;
            Total = Validate.ThrowIfNotPositive(total, nameof(Total));
            Duration = Validate.ThrowIfNegative(duration, nameof(Duration));
            Remaining = Total;
        }

        /// <summary>
        /// The condition for <see cref="IsDone"/> to return true; can be overriden.
        /// </summary>
        protected virtual bool IsDoneCondition() => Remaining <= 0;

        /// <summary> 
        /// <b>Irreversibly</b> advances the <see cref="Damage"/> by one second.
        /// First, ticks time passed.
        /// Gets one second's worth of damage from the abstract <see cref="CalculateTick"/> method, 
        /// then tracks remaining damage and returns damage dealt that second.
        /// </summary>
        public virtual int Tick()
        {
            TimePassed++;
            if (IsDone)
                return 0;
            
            var amount = CalculateTick();
            Remaining -= amount;
            return amount;
        }

        /// <summary>
        /// Calculates the damage to be dealt that second.
        /// Called only by <see cref="Tick"/>, which tracks internal values and progresses the event.
        /// Thus, calculations done may be irreversible.
        /// Only called if <see cref="IsDone"/> is false.
        /// </summary>
        protected abstract int CalculateTick();


        /// <returns>  A string containing variables and minimal formatting.  </returns>
        public abstract string ToDataString();
        
        /// <returns>  A string containing the variables of the extending class.  </returns>
        protected abstract string ChildInfoString();
        
        /// <returns>  A string verbosely describing the <see cref="Damage"/>.  </returns>
        public override string ToString() => ChildInfoString();
    }
}
