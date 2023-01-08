namespace LorendisCore.Player.Control.Actions.Behaviors
{
    public abstract class AimAndShootBehavior : IActionBehavior
    {
        protected readonly double HoldTime;

        protected double HeldFor = 0;
        

        public AimAndShootBehavior(double holdTime)
        {
            HoldTime = holdTime;
        }


        protected abstract void Shoot();
        protected abstract void StartAiming();
        protected abstract void StopAiming();

        public void Act(ButtonPressData buttonPressData)
        {
            HeldFor += buttonPressData.DeltaTime;
            var type = buttonPressData.GetButtonPressType();

            if (!AimIfHeld(type))
                ShootIfReleased(type);
        }

        private bool AimIfHeld(ButtonPressType type)
        {
            if (type != ButtonPressType.Held || HeldFor < HoldTime)
                return false;

            StartAiming();
            return true;
        }

        private void ShootIfReleased(ButtonPressType type)
        {
            if (type != ButtonPressType.Released)
                return;

            Shoot();
            if (HeldFor >= HoldTime)
            {
                StopAiming();
                Shoot();
            }
            HeldFor = 0.0;
        }
    }
}
