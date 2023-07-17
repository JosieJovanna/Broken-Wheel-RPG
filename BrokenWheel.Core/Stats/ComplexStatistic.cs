using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.Core.Stats
{
    /// <summary>
    /// A simple implementation of <see cref="IComplexStatistic"/>.
    /// Used for HP, SP, WP, and misc. <see cref="StatType"/>s like the amount of oil.
    /// </summary>
    public class ComplexStatistic : Statistic, IComplexStatistic
    {
        /// <summary> Destination Value </summary>
        protected int Dst;
        /// <summary> Maximum </summary>
        protected int Max;
        /// <summary> Exhaustion </summary>
        protected int Exh;

        /// <param name="type"> The <see cref="StatType"/> being tracked. </param>
        /// <param name="value"> The current value of the stat; will be set between zero and <see cref="EffectiveMaximum"/>. </param>
        /// <param name="maximum"> The maximum value before modification; will be set above or equal to zero. </param>
        /// <param name="modifier"> The modifier to the maximum value. Optional; defaults to zero. </param>
        /// <param name="exhaustion"> The exhaustion of the stat, which cannot be negative. Optional; defaults to zero. </param>
        public ComplexStatistic(StatInfo type, int value, int maximum, int modifier = 0, int exhaustion = 0) 
            : base(type, value, modifier)
        {
            Max = MathUtil.NonNeg(maximum);
            Exhaustion = MathUtil.NonNeg(exhaustion);
            Val = MathUtil.NonNeg(value);
            CapValues();
        }

        public new int Value
        {
            get => Val;
            set
            {
                Val = MathUtil.NonNeg(value);
                CapValues();
            }
        }

        public new int Modifier
        {
            get => Mod;
            set
            {
                Mod = value;
                CapValues();
            }
        }

        public int DestinationValue
        {
            get => Dst;
            set
            {
                Dst = MathUtil.NonNeg(value);
                CapValues();
            }
        }

        public int Maximum
        {
            get => Max;
            set
            {
                Max = MathUtil.NonNeg(value);
                CapValues();
            }
        }

        public int Exhaustion
        {
            get => Exh;
            set
            {
                Exh = MathUtil.NonNeg(value);
                CapValues();
            }
        }

        public int EffectiveMaximum { get => MathUtil.NonNeg(Max + Mod - Exh); }

        public new int EffectiveValue { get => Val; }

        private void CapValues()
        {
            Val = System.Math.Min(Val, EffectiveMaximum);
            Dst = System.Math.Min(Dst, EffectiveMaximum);
        }
    }
}
