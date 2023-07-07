using System;

namespace BrokenWheel.Core.Common.Damage.Events
{
    public class DamageEvent<T> where T : Damage
    {
        public T Result;
        
    }
}