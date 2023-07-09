using System;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Damage
{
    public class DelayedDamageTicker : DamageTicker
    {
        public readonly int Delay;

        private readonly DamageTicker _delayedDamageTicker;
        
        public DelayedDamageTicker(DamageTicker damage, int delay) 
            : base(damage.Type, damage.Total, damage.Duration + delay)
        {
            Validate.ThrowIfNotPositive(delay, nameof(delay));
            
            Delay = delay;
            _delayedDamageTicker = damage;
        }

        protected override int CalculateTick()
        {
            return TimePassed < Delay 
                ? 0 
                : _delayedDamageTicker.Tick();
        }

        public override string ToDataString()
        {
            return $"[{TimePassed}/{Delay}:{_delayedDamageTicker.ToDataString()}]";
        }

        protected override string ChildInfoString()
        {
            return $"{_delayedDamageTicker.ToString()} after {System.Math.Min(TimePassed, Delay)}/{Delay} seconds";
        }
    }
}
