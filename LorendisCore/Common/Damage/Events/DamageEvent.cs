using System;

namespace LorendisCore.Common.Damage.Events
{
    public class DamageEvent<T> where T : Damage
    {
        public T Result;
        
    }
}