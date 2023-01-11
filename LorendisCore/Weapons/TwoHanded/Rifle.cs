using LorendisCore.Player.Control.Actions;
using LorendisCore.Player.Control.Actions.Behaviors;

namespace LorendisCore.Weapons.TwoHanded
{
    public class Rifle : BaseWeapon
    {
        private bool _isAiming = false;

        protected OnPressBehavior OnFireBehavior;
        protected ToggleOrHoldBehavior AimBehavior;
        protected OnPressBehavior BashBehavior;

        public Rifle(IActionController actionController) : base(actionController)
        {
            OnFireBehavior = new OnPressBehavior(Fire);
            AimBehavior = new ToggleOrHoldBehavior(StartAiming, StopAiming);
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

        protected override void EquipWeapon()
        {
            Controller.Behaviors.MainPrimary = OnFireBehavior;
            Controller.Behaviors.MainSecondary = OnFireBehavior;
            Controller.Behaviors.OffhandPrimary = AimBehavior;
            Controller.Behaviors.OffhandSecondary = AimBehavior;
            Controller.Behaviors.Special = BashBehavior;
        }
    }
}
