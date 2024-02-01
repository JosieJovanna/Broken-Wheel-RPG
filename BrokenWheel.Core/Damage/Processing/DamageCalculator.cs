using System.Collections.Generic;
using BrokenWheel.Core.Damage.Dps;

namespace BrokenWheel.Core.Damage.Processing
{
    /// <summary>
    /// The default implementation of <see cref="IDamageCalculator"/>.
    /// </summary>
    internal class DamageCalculator : IDamageCalculator
    {
        protected readonly List<DpsCalculator> _queue = new List<DpsCalculator>();
        protected readonly List<DpsCalculator> _instants = new List<DpsCalculator>();
        protected readonly List<DamageMap> _lastTicks = new List<DamageMap>();
        protected DamageMap _dmgDone = new DamageMap();
        protected DamageMap _dmgTick = new DamageMap();
        protected double _time = 0;
        protected bool _justTicked = false;


        public void AddToQueue(IList<DpsCalculator> damages)
        {
            foreach (var dmg in damages)
                AddToQueue(dmg);
        }
        
        public void AddToQueue(DpsCalculator damage)
        {
            if (damage.GetType() != typeof(InstantDpsCalculator))
                _queue.Add(damage);
            else
                _instants.Add(damage);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }

        public List<DamageMap> DamageThisSecond()
        {
            return _justTicked
                ? _lastTicks
                : new List<DamageMap>();
        }

        public DamageMap DamageTick(double delta)
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
                deal.Add(dmg.Type, dmg.Dps());
            _instants.Clear();
        }

        private void CompleteTickIfSecondPassed(DamageMap deal)
        {
            while (_time > 1)
            {
                deal.Add(_dmgTick - _dmgDone);
                CalculateDps();
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
        
        private void CalculateDps()
        {
            _dmgTick = new DamageMap();
            _dmgDone = new DamageMap();
            foreach (var damage in _queue)
                IncludeDamage(damage);
        }

        private void IncludeDamage(DpsCalculator damage)
        {
            if (damage.IsDone)
                _queue.Remove(damage);
            else
                _dmgTick.Add(damage.Type, damage.Dps());
        }
    }
}
