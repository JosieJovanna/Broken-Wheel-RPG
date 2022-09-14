using System.Collections.Generic;

namespace LorendisCore.Common.Damage.Processing
{
    /// <summary>
    ///     Calculated damage per second and interpolates it so that it can be applied by tick.
    ///     Keeping track of progress through a second is handled by the calculator.
    ///     Will lump every type of damage into one struct so that they can be applied neatly by a <see cref="DamageCalculator"/>.
    ///     Used in the process of processing and applying damages, which is managed by a <see cref="DamageEngine"/>.
    /// </summary>
    public interface DamageCalculator
    {
        /// <summary>
        ///     Adds damage to the group to be calculated to completion.
        ///     <see cref="Damage"/>s will not leave the queue until they deal all damage.
        /// </summary>
        void AddToQueue(Damage damage);

        /// <summary>
        ///     Clears the damage queue and stops processing.
        /// </summary>
        void ClearQueue();

        /// <summary>
        ///     Returns a (potentially-empty but non-null) list of <see cref="DamageMap"/>s.
        ///     If during the last time <see cref="Calculate(double)"/> was run a full second passed,
        ///     (or in rare cases, more than one, thus the list), the damage map of the full second is returned.
        ///     This is so that when calculating frame-by-frame, the full DPS numbers can be tracked.
        /// </summary>
        List<DamageMap> DpsMapIfJustTicked();

        /// <summary>
        ///     Calculates the damage for the game tick, interpolating DPS based on the fraction of the second.
        ///     This process is irreversable.
        /// </summary>
        /// <param name="delta"> The fraction of a second that has passed since the last tick. </param>
        /// <returns> 
        ///     A dictionary with <see cref="DamageType"/> as the key, and the raw damage amount as the value. 
        ///     Will not include any <see cref="DamageType"/>s without any damage to deal.
        /// </returns>
        DamageMap Calculate(double delta);
    }
}
