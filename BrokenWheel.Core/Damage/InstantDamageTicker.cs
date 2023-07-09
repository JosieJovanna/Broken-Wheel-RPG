
namespace BrokenWheel.Core.Damage
{
    public class InstantDamageTicker : DamageTicker
    {
        private bool _isDone;
        protected override bool IsDoneCondition() => _isDone;
        
        /// <summary>
        /// Creates an instant damage event with a duration of 0, which is to be 
        /// </summary>
        public InstantDamageTicker(DamageType type, int amount)
            : base(type, amount, 0) { }
        
        protected override int CalculateTick()
        {
            _isDone = true;
            return Total;
        }

        public override string ToDataString()
        {
            return $"[({Total}){Type.GetName()}]";
        }

        protected override string ChildInfoString()
        {
            return $"{Total} instant {Type.GetName()} damage";
        }
    }
}
