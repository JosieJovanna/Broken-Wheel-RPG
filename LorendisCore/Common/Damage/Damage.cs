using System;

namespace LorendisCore.Common.Damage
{
    /// <summary>
    ///     Abstract class which causes some damage over time. Values can only be positive.
    /// </summary>
    public abstract class Damage
    {
        public readonly DamageType Type;
        public /*readonly*/ int Total { get; protected set; } // TODO: make readonly and move polynomial math to utility class
        public /*readonly*/ int Duration { get; protected set; } // TODO: make readonly and move polynomial math to utility class
        public int Remaining { get; protected set; }
        public int TimePassed { get; protected set; } = 0;
        public int Dealt => Total - Remaining;
        public int TimeRemaining => Duration - TimePassed;
        public bool IsDone => IsDoneCondition();


        /// <summary>
        ///     Sets <see cref="Type"/> and internal trackers.
        /// </summary>
        /// <param name="type">  The type of damage dealt.  </param>
        /// <param name="total">  
        ///     Total damage dealt over the unit's lifetime. 
        ///     Throws an <see cref="ArgumentException"/> if 0 or lower.  
        /// </param>
        /// <param name="duration">
        ///     How long it takes to deal the total damage, in seconds.
        ///     Throws an <see cref="ArgumentException"/> if 0 or lower.  
        /// </param>
        protected Damage(DamageType type, int total, int duration)
        {
            if (total <= 0)
                throw new ArgumentException("Damage amount must be positive; non-damages are to be filtered out during the processing of damage events.");
            if (duration < 0)
                throw new ArgumentException("Damage duration must be positive.");

            Type = type;
            Total = total;
            Duration = duration;
            Remaining = Total;
        }


        /// <summary>
        ///     Calculates the damage to be dealt that second.
        ///     Called only by <see cref="Tick"/>, which tracks internal values and progresses the event.
        ///     Thus, calculations done may be irreversible.
        /// </summary>
        protected abstract int CalculateTick();


        /// <summary>
        ///     The condition for <see cref="IsDone"/> to return true; can be overriden.
        /// </summary>
        protected virtual bool IsDoneCondition() => Remaining <= 0;

        /// <summary> 
        ///     <b>Irreversibly</b> advances the <see cref="Damage"/> by one second.
        ///     Gets one second's worth of damage from the abstract <see cref="CalculateTick"/> method, 
        ///     then tracks remaining damage and time and returns damage dealt that second.
        /// </summary>
        public virtual int Tick()
        {
            if (IsDone)
                return 0;

            var amount = CalculateTick();
            TimePassed++;
            Remaining -= amount;
            return amount;
        }

        
        /// <returns>  A string verbosely describing the <see cref="Damage"/>.  </returns>
        public override string ToString() => ChildInfoString();

        /// <returns>  A string containing variables and minimal formatting.  </returns>
        public abstract string ToDataString();
        
        /// <returns>  A string containing the variables of the extending class.  </returns>
        protected abstract string ChildInfoString();
    }
}
