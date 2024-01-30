using BrokenWheel.Math.Utility;

namespace BrokenWheel.Core.Stats.Processing.Implementation
{
    /// <summary>
    /// A simple implementation of <see cref="IStatisticProcessor"/>.
    /// Used for skills, proficiencies, et cetera, but not things like HP.
    /// </summary>
    internal class StatisticProcessor : IStatisticProcessor
    {
        protected readonly StatInfo Stat;
        protected int Val;
        protected int Mod;

        /// <param name="type"> The type of statistic being tracked. </param>
        /// <param name="value"> The value of the stat before modification. Value min is zero. </param>
        /// <param name="modifier"> The amount the stat is modified by for the <see cref="EffectiveValue"/>. </param>
        public StatisticProcessor(StatInfo type, int value, int modifier = 0)
        {
            Stat = type;
            Val = MathUtil.NonNeg(value);
            Mod = modifier;
        }

        public StatInfo StatInfo { get => Stat; }

        public int Value { get => Val; set => Val = MathUtil.NonNeg(value); }

        public int Modifier { get => Mod; set => Mod = value; }

        public int EffectiveValue { get => MathUtil.NonNeg(Val + Mod); }
    }
}
