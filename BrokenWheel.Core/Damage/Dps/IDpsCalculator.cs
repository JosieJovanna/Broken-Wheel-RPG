namespace BrokenWheel.Core.Damage.Dps
{
    internal interface IDpsCalculator
    {
        /// <summary>
        /// The type of damage being dealt.
        /// </summary>
        DamageType Type { get; }

        /// <summary>
        /// Whether the damage has all been dealt, and is finished.
        /// </summary>
        bool IsDone { get; }

        /// <summary>
        /// The total amount of damage over time that is dealt.
        /// </summary>
        int TotalDamage { get; }

        /// <summary>
        /// The amount of damage that has been dealt so far.
        /// </summary>
        int DamageDealt { get; }

        /// <summary>
        /// The amount of damage still to be dealt.
        /// </summary>
        int RemainingDamage { get; }

        /// <summary>
        /// The total number of seconds it takes to finish dealing damage.
        /// </summary>
        int Duration { get; }

        /// <summary>
        /// The amount of time that has passed since starting calculation, in seconds.
        /// </summary>
        int SecondsPassed { get; }

        /// <summary>
        /// The amount of time remaining until calculation is complete.
        /// </summary>
        int TimeRemaining { get; }

        /// <summary>
        /// The amount of damage dealt in one second. Irreversible.
        /// </summary>
        int Dps();
    }
}
