using System;

namespace LorendisCore.Common.Damage
{
    /// <summary>
    ///     Abstract class which causes some damage over time. Values can only be positive.
    /// </summary>
    public abstract class Damage
    {
        public DamageType Type { get; }

        #region Queries
        public int Total => _total;
        public int Duration => _duration;

        public int Dealt => _total - _remaining;
        public int Remaining => _remaining;

        public int TimePassed => _time;
        public int TimeRemaining => _duration - _time;

        public bool IsDone => IsDoneCondition();
        #endregion

        protected int _total;
        protected int _duration;
        protected int _remaining;
        protected int _time = 0;


        /// <summary>
        ///     Sets <see cref="Type"/> and internal trackers.
        /// </summary>
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
            _total = total;
            _remaining = _total;
            _duration = duration;
        }


        /// <summary> 
        ///     <b>Irreversably</b> advances the <see cref="Damage"/> by one second.
        ///     Gets one second's worth of damage from the abstract <see cref="CalculateTick"/> method, 
        ///     then tracks remaining damage and time and returns damage dealt that second.
        /// </summary>
        public int Tick()
        {
            int amount = IsDone ? 0 : CalculateTick();
            _time++;
            _remaining -= amount;
            return amount;
        }

        /// <summary>
        ///     Calculates the damage to be dealt that second.
        ///     Called only by <see cref="Tick"/>, which tracks internal values and progresses the event.
        ///     Thus, calculations done may be irreversable.
        /// </summary>
        protected abstract int CalculateTick();


        /// <summary>
        ///     The condition for <see cref="IsDone"/> to return true; can be overriden.
        /// </summary>
        protected virtual bool IsDoneCondition() => _remaining <= 0;

        #region ToString
        /// <returns>  A string verbosely describing the <see cref="Damage"/>.  </returns>
        public override string ToString() => ChildInfoString();

        /// <returns>  A string containing variables and minimal formatting.  </returns>
        public abstract string ToDataString();
        
        /// <returns>  A string containing the variables of the extending class.  </returns>
        protected abstract string ChildInfoString();
        #endregion
    }
}
