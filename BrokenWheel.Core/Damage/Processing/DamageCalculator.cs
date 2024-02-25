using System.Collections.Generic;
using BrokenWheel.Core.Damage.Dps;

namespace BrokenWheel.Core.Damage.Processing
{
    /// <summary>
    /// The default implementation of <see cref="IDamageCalculator"/>.
    /// </summary>
    internal class DamageCalculator : IDamageCalculator
    {
        protected readonly List<DpsCalculator> Queue = new List<DpsCalculator>();
        protected readonly List<DpsCalculator> Instants = new List<DpsCalculator>();
        protected readonly List<DamageMap> LastTicks = new List<DamageMap>();
        protected DamageMap DmgDone = new DamageMap();
        protected DamageMap DmgTick = new DamageMap();
        protected double Time = 0;
        protected bool IsJustTicked = false;


        public void AddToQueue(IList<DpsCalculator> damages)
        {
            foreach (var dmg in damages)
                AddToQueue(dmg);
        }

        public void AddToQueue(DpsCalculator damage)
        {
            if (damage.GetType() != typeof(InstantDpsCalculator))
                Queue.Add(damage);
            else
                Instants.Add(damage);
        }

        public void ClearQueue()
        {
            Queue.Clear();
        }

        public List<DamageMap> DamageThisSecond()
        {
            return IsJustTicked
                ? LastTicks
                : new List<DamageMap>();
        }

        public DamageMap DamageTick(double delta)
        {
            Time += delta;
            IsJustTicked = false;

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
            foreach (var dmg in Instants)
                deal.Add(dmg.Type, dmg.Dps());
            Instants.Clear();
        }

        private void CompleteTickIfSecondPassed(DamageMap deal)
        {
            while (Time > 1)
            {
                deal.Add(DmgTick - DmgDone);
                CalculateDps();
                TrackThatJustTicked();
            }
        }

        private void TrackThatJustTicked()
        {
            Time -= 1;
            LastTicks.Clear();
            IsJustTicked = true;
            LastTicks.Add(DmgTick);
        }

        private DamageMap CalculateFractionAndTrack(DamageMap deal)
        {
            deal.Add(DmgTick.Fraction(Time));
            DmgDone.Add(deal);
            return deal;
        }

        private void CalculateDps()
        {
            DmgTick = new DamageMap();
            DmgDone = new DamageMap();
            foreach (var damage in Queue)
                IncludeDamage(damage);
        }

        private void IncludeDamage(DpsCalculator damage)
        {
            if (damage.IsDone)
                Queue.Remove(damage);
            else
                DmgTick.Add(damage.Type, damage.Dps());
        }
    }
}
