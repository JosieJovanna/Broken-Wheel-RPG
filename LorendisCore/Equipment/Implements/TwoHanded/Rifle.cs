using LorendisCore.Equipment.Implements.WeaponTypes;
using LorendisCore.Player.Control;
using LorendisCore.Settings;
using LorendisCore.Player.Control.Actions;
using LorendisCore.Player.Control.Actions.Behaviors;

namespace LorendisCore.Equipment.Implements.TwoHanded
{
    public class Rifle : ITwoHandedImplement, ITriggerWeapon
    {
        private bool _isAiming = false;

        protected IActionBehavior FireBehavior;
        protected IActionBehavior AimBehavior;
        protected IActionBehavior BashBehavior;
        private bool _isAiming1;

        public Rifle()
        {
            var holdTime = StaticSettings.Controls.HoldToToggleAim;
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

        public bool TryPrimaryPress(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAltPrimaryPress(PressData press)
        {
            throw new System.NotImplementedException();
        }

        public bool TryOffHandPrimary(PressData pressData)
        {
            throw new System.NotImplementedException();
        }

        public bool TryOffHandSecondary(PressData pressData)
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
