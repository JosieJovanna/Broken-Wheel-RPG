using LorendisCore.Common.Delegates;

namespace LorendisCore.Control.Behaviors
{
    /// <summary>
    /// Aiming can be held, or toggled on/off.
    /// </summary>
    public class ToggleOrHoldBehavior : AbstractActionBehavior
    {
        private bool _isToggled = false;

        protected SimpleDelegate ToggleOn;
        protected SimpleDelegate ToggleOff;

        public ToggleOrHoldBehavior(double holdTime) : base(holdTime) { }
        
        public ToggleOrHoldBehavior(double holdTime, SimpleDelegate toggleOn, SimpleDelegate toggleOff) : base(holdTime)
        {
            ToggleOn = toggleOn;
            ToggleOff = toggleOff;
        }

        protected override void InitialPress(bool isAltPress) { } // do nothing...

        protected override void Held(bool isAltPress)
        {
            _isToggled = true;
            ToggleOn();
        }

        protected override void ReleaseClick(bool isAltPress)
        {
            if (_isToggled)
                ToggleOff();
            else
                ToggleOn();
            _isToggled = !_isToggled;
        }

        protected override void ReleaseHold(bool isAltPress)
        {
            _isToggled = false;
            ToggleOff();
        }
    }
}
