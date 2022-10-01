using System.Linq;
using System.Collections.Generic;
using LorendisCore.Utilities;

namespace LorendisCore.Common.Damage
{
    public class CompoundDamage : Damage
    {
        private readonly IEnumerable<Damage> _dmgList;

        public CompoundDamage(IEnumerable<Damage> damages) 
            : base(GetType(damages), GetTotal(damages), GetDuration(damages))
        {
            _dmgList = damages;
        }

        protected override int CalculateTick()
        {
            return _dmgList
                .Where(d => !d.IsDone)
                .Select(d => d.Tick())
                .Sum();
        }

        public override string ToDataString()
        {
            var joined = string.Join(",", _dmgList
                .Select(d => d.ToDataString()));
            return $"[{joined}]";
        }

        protected override string ChildInfoString()
        {
            var joined = string.Join(", ", _dmgList
                .Select(d => d.ToString()));
            return $"{Total} compound damage over {Duration} total seconds: {joined}";
        }

        #region Utility

        private static DamageType GetType(IEnumerable<Damage> damages)
        {
            Validate.ThrowIfEmpty(damages, nameof(damages));
            var type = damages.First().Type;
            foreach (var dmg in damages) 
                DamageTypeMismatchException.ThrowIfMismatch(type, dmg.Type);
            return type;
        }
        
        private static int GetTotal(IEnumerable<Damage> damages)
        {
            Validate.ThrowIfNull(damages);
            return damages
                .Select(d => d.Total)
                .Sum();
        }

        private static int GetDuration(IEnumerable<Damage> damages)
        {
            Validate.ThrowIfNull(damages);
            return damages
                .Select(d => d.Duration)
                .Max();
        }
        #endregion
    }
}