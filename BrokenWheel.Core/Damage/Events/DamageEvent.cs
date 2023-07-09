using System;

namespace BrokenWheel.Core.Damage.Events
{
    public class DamageEvent<T> where T : DamageTicker
    {
        public T Result;
        
    }
}
