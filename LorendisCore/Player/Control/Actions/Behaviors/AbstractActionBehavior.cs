namespace LorendisCore.Player.Control.Actions.Behaviors
{
    /// <summary>
    /// The simplest type of <see cref="IActionBehavior"/>, where a different action 
    /// can be performed based on whether the button is initially pressed,
    /// when the button has been held for a certain amount of time,
    /// and when the button has been released.
    /// These actions are abstract methods which must be individually implemented.
    /// </summary>
    public abstract class AbstractActionBehavior : IActionBehavior
    {
        private readonly double _holdTime;
        private double _heldFor = 0;

        protected AbstractActionBehavior(double holdTime)
        {
            _holdTime = holdTime;
        }

        protected abstract void InitialPress();
        protected abstract void Held();
        protected abstract void ReleaseClick();
        protected abstract void ReleaseHold();
        
        public void Execute(PressData pressData)
        {
            _heldFor += pressData.DeltaTime;
            var type = pressData.Type;

            Ex(type);
        }

        private void Ex(PressType type)
        {
            switch (type)
            {
                case PressType.Clicked:
                    InitialPress();
                    break;
                case PressType.Released:
                    ReleaseClickOrHold();
                    break;
                case PressType.Held:
                    HoldIfLongEnough();
                    break;
            }
        }

        private void ReleaseClickOrHold()
        {
            if (_heldFor >= _holdTime)
                ReleaseHold();
            else
                ReleaseClick();
            _heldFor = 0;
        }

        private void HoldIfLongEnough()
        {
            if (_heldFor >= _holdTime)
                Held();
        }
    }
}
