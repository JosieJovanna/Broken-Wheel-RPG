
namespace BrokenWheel.Core.Damage.Dps
{
    internal class InstantDpsCalculator : DpsCalculator
    {
        private bool _isDone;
        protected override bool IsDoneCondition() => _isDone;
        
        /// <summary>
        /// Creates an instant damage event with a duration of 0, which is to be 
        /// </summary>
        public InstantDpsCalculator(DamageType type, int amount)
            : base(type, amount, 0) { }
        
        protected override int CalculateDps()
        {
            _isDone = true;
            return TotalDamage;
        }

        public override string ToDataString()
        {
            return $"[({TotalDamage}){Type.GetName()}]";
        }

        protected override string ChildInfoString()
        {
            return $"{TotalDamage} instant {Type.GetName()} damage";
        }
    }
}
