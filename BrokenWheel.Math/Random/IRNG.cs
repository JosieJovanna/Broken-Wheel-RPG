namespace BrokenWheel.Math.Random
{
    /// <summary>
    /// A <b>deterministic</b> random number generator to be used for any and all generated loot, AI,
    /// and anything else except for graphics, which <b>must</b> use a separate instance of random number generator.
    /// </summary>
    public interface IRNG // TODO: event imitting for state changes?
    {
        /// <summary>
        /// Uses an integer to determinalistically advance the random number generator's seed.
        /// Usually, the seed is advanced by input, or other things which can be consistently simulated.
        /// </summary>
        void AdvanceState(long value);

        /// <summary>
        /// Uses a double to determinalistically advance the random number generator's seed.
        /// Usually, the seed is advanced by input, or other things which can be consistently simulated.
        /// </summary>
        void AdvanceState(double value);

        /// <summary>
        /// Uses a string to determinalistically advance the random number generator's seed.
        /// Usually, the seed is advanced by input, or other things which can be consistently simulated.
        /// </summary>
        void AdvanceState(string value);

        /// <summary>
        /// Saves the current state of the generator to be later restored by calling <see cref="RestoreState"/>.
        /// Used when something needs to use RNG but doesn't want to interfere with, for example, replaying actions.
        /// </summary>
        void CacheState();

        /// <summary>
        /// Restores the state of the generator to when <see cref="CacheState"/> was last called.
        /// Used when something needs to use RNG but doesn't want to interfere with, for example, replaying actions.
        /// </summary>
        void RestoreState();

        /// <summary>
        /// Used for random weights in equasions.
        /// </summary>
        /// <returns> A float between 0 and 1, inclusive. </returns>
        float Random();

        /// <summary>
        /// Generates a random number within an inclusive range.
        /// </summary>
        /// <param name="min"> Minimum value, inclusive. </param>
        /// <param name="max"> Maximum value, inclusive. </param>
        /// <returns> A float between min and max. </returns>
        float Random(float min, float max);

        /// <summary>
        /// Generates a random number on a normal distribution.
        /// The most common value will be the mean.
        /// 68% of values will fall within ±one deviation,
        /// 95% will fall within ±two deviations, and 
        /// 99.7% will fall within ±three.
        /// </summary>
        /// <param name="mean"> The centre of the distribution. 0 by default. </param>
        /// <param name="deviation"> The amount that values vary from the mean by. 1 by default. </param>
        /// <returns> A float within the specified normal distribution. </returns>
        float Normal(float mean = 0f, float deviation = 1f);

        /// <summary>
        /// Generates a random integer.
        /// </summary>
        /// <returns> An int between min and max. </returns>
        int RandomInteger();

        /// <summary>
        /// Generates a random number between 0 and the given maximum (inclusive).
        /// </summary>
        /// <param name="max"> Maximum value, inclusive. </param>
        /// <returns> An int between 0 and max, inclusive. </returns>
        int RandomInteger(int max);

        /// <summary>
        /// Generates a random number within an inclusive range.
        /// </summary>
        /// <param name="min"> Minimum value, inclusive. </param>
        /// <param name="max"> Maximum value, inclusive. </param>
        /// <returns> An int between min and max, inclusive. </returns>
        int RandomInteger(int min, int max);
    }
}
