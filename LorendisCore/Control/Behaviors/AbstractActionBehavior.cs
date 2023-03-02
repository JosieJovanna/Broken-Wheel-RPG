using LorendisCore.Control.Models;

namespace LorendisCore.Control.Behaviors
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
        private double _heldFor;
        private bool _isAltPress;
        private bool _isAltPressIsLocked;

        protected AbstractActionBehavior(double holdTime)
        {
            _holdTime = holdTime;
        }

        protected abstract void InitialPress(bool isAltPress);
        protected abstract void Held(bool isAltPress);
        protected abstract void ReleaseClick(bool isAltPress);
        protected abstract void ReleaseHold(bool isAltPress);
        
        public void Execute(PressData press)
        {
            _heldFor += press.DeltaTime;
            if (!_isAltPressIsLocked)
                _isAltPress = press.IsAltPress;
            SwitchPress(press);
        }

        private void SwitchPress(PressData press)
        {
            switch (press.Type)
            {
                case PressType.Clicked:
                    InitialPress(_isAltPress);
                    break;
                case PressType.Released:
                    ReleaseClickOrHold();
                    break;
                case PressType.Held:
                    HoldIfLongEnough();
                    break;
                case PressType.NotHeld:
                default:
                    _isAltPressIsLocked = false;
                    break;
            }
        }

        private void ReleaseClickOrHold()
        {
            if (_heldFor >= _holdTime)
                ReleaseHold(_isAltPress);
            else
                ReleaseClick(_isAltPress);
            _heldFor = 0;
            _isAltPressIsLocked = false;
        }

        private void HoldIfLongEnough()
        {
            if (_heldFor >= _holdTime)
                Held(_isAltPress);
        }
    }
}
