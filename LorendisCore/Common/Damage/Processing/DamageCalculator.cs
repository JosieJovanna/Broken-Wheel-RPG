using System.Collections.Generic;

namespace LorendisCore.Common.Damage.Processing
{
    /// <summary>
    /// The default implementation of <see cref="IDamageCalculator"/>.
    /// </summary>
    public class DamageCalculator : IDamageCalculator
    {
        protected readonly List<Damage> _queue = new List<Damage>();
        protected readonly List<Damage> _instants = new List<Damage>();
        protected readonly List<DamageMap> _lastTicks = new List<DamageMap>();
        protected DamageMap _dmgDone = new DamageMap();
        protected DamageMap _dmgTick = new DamageMap();
        protected double _time = 0;
        protected bool _justTicked = false;


        public void AddToQueue(IEnumerable<Damage> damages)
        {
            foreach (var dmg in damages)
                AddToQueue(dmg);
        }
        
        public void AddToQueue(Damage damage)
        {
            if (damage.GetType() != typeof(InstantDamage))
                _queue.Add(damage);
            else
                _instants.Add(damage);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }

        public List<DamageMap> DpsMapIfJustTicked()
        {
            return _justTicked
                ? _lastTicks
                : new List<DamageMap>();
        }

        #region Calculation
        public DamageMap Calculate(double delta)
        {
            _time += delta;
            _justTicked = false;

            var deal = GetInitialDamageToDeal();
            return CalculateFractionAndTrack(deal);
        }

        private DamageMap GetInitialDamageToDeal()
        {
            var init = new DamageMap();
            CalculateInstantDamage(init);
            CompleteTickIfSecondPassed(init);
            return init;
        }

        private void CalculateInstantDamage(DamageMap deal)
        {
            foreach (var dmg in _instants)
                deal.Add(dmg.Type, dmg.Tick());
            _instants.Clear();
        }

        private void CompleteTickIfSecondPassed(DamageMap deal)
        {
            while (_time > 1)
            {
                deal.Add(_dmgTick - _dmgDone);
                CalculateTick();
                TrackThatJustTicked();
            }
        }

        private void TrackThatJustTicked()
        {
            _time -= 1;
            _lastTicks.Clear();
            _justTicked = true;
            _lastTicks.Add(_dmgTick);
        }

        private DamageMap CalculateFractionAndTrack(DamageMap deal)
        {
            deal.Add(_dmgTick.Fraction(_time));
            _dmgDone.Add(deal);
            return deal;
        }


        /// <summary>
        /// Calculates the damage dealt per second. 
        /// This process is irreversable.
        /// </summary>
        /// <returns>
        /// A dictionary with <see cref="DamageType"/> as the key, and the raw <see cref="Common.Damage"/> as the value. 
        /// Will not include any <see cref="DamageType"/>s without any damage to deal.
        /// </returns>
        private void CalculateTick()
        {
            _dmgTick = new DamageMap();
            _dmgDone = new DamageMap();
            foreach (var damage in _queue)
                CaculateDamage(damage);
        }

        private void CaculateDamage(Damage damage)
        {
            if (damage.IsDone)
                _queue.Remove(damage);
            else
                _dmgTick.Add(damage.Type, damage.Tick());
        }
        #endregion
    }
}
