using LorendisCore.Settings;
using LorendisCore.Player.Control.Actions.Behaviors;

namespace LorendisCore.Equipment.Implements.TwoHanded
{
    public class Rifle : TwoHandedImplement
    {
        private bool _isAiming = false;

        protected OnPressBehavior OnFireBehavior;
        protected ToggleOrHoldBehavior AimBehavior;
        protected OnPressBehavior BashBehavior;

        public Rifle()
        {
            OnFireBehavior = new OnPressBehavior(Fire);
            AimBehavior = new ToggleOrHoldBehavior(StaticSettings.Controls.HoldToToggleAim, StartAiming, StopAiming);
            BashBehavior = new OnPressBehavior(Bash);
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

        public void Bash()
        {
            // pow
        }
    }
}
