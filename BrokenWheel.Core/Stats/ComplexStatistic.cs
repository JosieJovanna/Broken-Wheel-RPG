using BrokenWheel.Math.Utility;

namespace BrokenWheel.Core.Stats
{
    /// <summary>
    /// A simple implementation of <see cref="IComplexStatistic"/>.
    /// Used for HP, SP, WP, and misc. <see cref="Stat"/>s like the amount of oil.
    /// </summary>
    public class ComplexStatistic : Statistic, IComplexStatistic
    {
        protected int Max;
        protected int Exh;

        /// <param name="type"> The <see cref="Stat"/> being tracked. </param>
        /// <param name="value"> The current value of the stat; will be set between zero and <see cref="EffectiveMaximum"/>. </param>
        /// <param name="maximum"> The maximum value before modification; will be set above or equal to zero. </param>
        /// <param name="modifier"> The modifier to the maximum value. Optional; defaults to zero. </param>
        /// <param name="exhaustion"> The exhaustion of the stat, which cannot be negative. Optional; defaults to zero. </param>
        public ComplexStatistic(Stat type, int value, int maximum, int modifier = 0, int exhaustion = 0) 
            : base(type, value, modifier)
        {
            Max = MathUtil.NonNeg(maximum);
            Exhaustion = MathUtil.NonNeg(exhaustion);
            Val = MathUtil.NonNeg(value);
            CapStat();
        }

        public new int Value
        {
            get => Val;
            set
            {
                Val = MathUtil.NonNeg(value);
                CapStat();
            }
        }

        public new int Modifier
        {
            get => Mod;
            set
            {
                Mod = value;
                CapStat();
            }
        }

        public int Maximum
        {
            get => Max;
            set
            {
                Max = MathUtil.NonNeg(value);
                CapStat();
            }
        }

        public int Exhaustion
        {
            get => Exh;
            set
            {
                Exh = MathUtil.NonNeg(value);
                CapStat();
            }
        }

        public int EffectiveMaximum { get => MathUtil.NonNeg(Max + Mod - Exh); }

        public new int EffectiveValue { get => Val; }

        private void CapStat() => Val = System.Math.Min(Val, EffectiveMaximum);
    }
}
