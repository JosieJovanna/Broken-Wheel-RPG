using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Damage.Dps
{
    internal class DelayedDpsCalculator : DpsCalculator
    {
        public readonly int Delay;

        private readonly DpsCalculator _delayedDpsCalculator;

        public DelayedDpsCalculator(DpsCalculator damage, int delay)
            : base(damage.Type, damage.TotalDamage, damage.Duration + delay)
        {
            Validate.ThrowIfNotPositive(delay, nameof(delay));

            Delay = delay;
            _delayedDpsCalculator = damage;
        }

        protected override int CalculateDps()
        {
            return SecondsPassed < Delay
                ? 0
                : _delayedDpsCalculator.Dps();
        }

        public override string ToDataString()
        {
            return $"[{SecondsPassed}/{Delay}:{_delayedDpsCalculator.ToDataString()}]";
        }

        protected override string ChildInfoString()
        {
            return $"{_delayedDpsCalculator.ToString()} after {System.Math.Min(SecondsPassed, Delay)}/{Delay} seconds";
        }
    }
}
