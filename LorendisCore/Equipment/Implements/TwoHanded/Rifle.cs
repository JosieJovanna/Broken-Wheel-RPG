using LorendisCore.Equipment.Implements.WeaponTypes;
using LorendisCore.Settings;
using LorendisCore.Player.Control.Actions;
using LorendisCore.Player.Control.Actions.Behaviors;
using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements.TwoHanded
{
    public class Rifle : TwoHandedImplement, ITriggerWeapon
    {
        private bool _isAiming = false;

        protected IActionBehavior FireBehavior;
        protected IActionBehavior AimBehavior;
        protected IActionBehavior BashBehavior;

        public Rifle()
        {
            var holdTime = StaticSettings.Controls.HoldToToggleAim;
            AimBehavior = new ToggleOrHoldBehavior(holdTime, StartAiming, StopAiming);
            BashBehavior = new OnPressBehavior(Bash);
            FireBehavior = new OnPressBehavior(Fire);
        }

        public bool IsAiming() => _isAiming;

        public void StartAiming()
        {
            _isAiming = true;
        }

        public void StopAiming()
        {
            _isAiming = false;
        }

        public void Fire()
        {
            // pew pew
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Bash()
        {
            // pow
        }

        private void SetBehaviorMap()
        {
            Behaviors = new TwoHandedBehaviorMap
            {
                MainPrimary = FireBehavior,
                MainSecondary = AimBehavior,
                Special = BashBehavior,
                Reload = Reload
            };
        }
    }
}
