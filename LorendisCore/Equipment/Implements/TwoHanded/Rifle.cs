using LorendisCore.Control;
using LorendisCore.Settings;
using LorendisCore.Control.Models;
using LorendisCore.Control.Behaviors;
using LorendisCore.Control.Implements;
using LorendisCore.Equipment.Implements.WeaponTypes;

namespace LorendisCore.Equipment.Implements.TwoHanded
{
    public class Rifle : ITwoHandedControl, ITriggerWeapon
    {
        private bool _isAiming = false;

        protected IActionBehavior FireBehavior;
        protected IActionBehavior AimBehavior;
        protected IActionBehavior BashBehavior;
        private bool _isAiming1;

        public Rifle()
        {
            var holdTime = Settings.StaticSettings.Controls.HoldToToggleAim;
            AimBehavior = new ToggleOrHoldBehavior(holdTime, StartAiming, StopAiming);
            BashBehavior = new OnPressBehavior(Bash);
            FireBehavior = new OnPressBehavior(Fire);
        }

        public bool IsAiming() => _isAiming;

        bool IAimable.IsAiming => _isAiming1;

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

        public bool TryPrimary(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAltPrimary(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public bool TrySecondary(PressData pressData)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAltSecondary(PressData pressData)
        {
            throw new System.NotImplementedException();
        }

        public bool IsReloading { get; }
        public bool CanReload { get; }
        public void StartReloading()
        {
            throw new System.NotImplementedException();
        }

        public void FinishReloading()
        {
            throw new System.NotImplementedException();
        }

        public void CancelReloading()
        {
            throw new System.NotImplementedException();
        }

        public void KeepReloading(double deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
