namespace LorendisCore.Player.Control.Actions.Behaviors
{
    /// <summary>
    /// The simplest type of <see cref="IActionBehavior"/>, where a different action 
    /// can be performed based on whether the button is initially pressed,
    /// when the button has been held for a certain amount of time,
    /// and when the button has been released.
    /// These actions are abstract methods which must be individually implemented.
    /// </summary>
    public abstract class BaseActionBehavior : IActionBehavior
    {
        private readonly double _holdTime;
        private double _heldFor = 0;

        protected BaseActionBehavior(double holdTime)
        {
            _holdTime = holdTime;
        }

        public void Act(ButtonPressData buttonPressData)
        {
            _heldFor += buttonPressData.DeltaTime;
            var type = buttonPressData.GetButtonPressType();

            Ex(type);
        }

        private void Ex(ButtonPressType type)
        {
            switch (type)
            {
                case ButtonPressType.Pressed:
                    InitialPress();
                    break;
                case ButtonPressType.Released:
                    _heldFor = 0;
                    Release();
                    break;
                case ButtonPressType.Held:
                    if (_heldFor >= _holdTime)
                        Held();
                    break;
            }
        }

        protected abstract void InitialPress();
        protected abstract void Release();
        protected abstract void Held();
    }
}
