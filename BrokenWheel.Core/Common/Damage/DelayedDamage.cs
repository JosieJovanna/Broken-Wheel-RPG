using System;
using BrokenWheel.Core.Utilities;

namespace BrokenWheel.Core.Common.Damage
{
    public class DelayedDamage : Damage
    {
        public readonly int Delay;

        private readonly Damage _delayedDamage;
        
        public DelayedDamage(Damage damage, int delay) 
            : base(damage.Type, damage.Total, damage.Duration + delay)
        {
            Validate.ThrowIfNotPositive(delay, nameof(delay));
            
            Delay = delay;
            _delayedDamage = damage;
        }

        protected override int CalculateTick()
        {
            return TimePassed < Delay 
                ? 0 
                : _delayedDamage.Tick();
        }

        public override string ToDataString()
        {
            return $"[{TimePassed}/{Delay}:{_delayedDamage.ToDataString()}]";
        }

        protected override string ChildInfoString()
        {
            return $"{_delayedDamage.ToString()} after {Math.Min(TimePassed, Delay)}/{Delay} seconds";
        }
    }
}