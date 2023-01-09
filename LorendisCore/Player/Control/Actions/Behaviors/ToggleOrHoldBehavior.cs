using LorendisCore.Settings;
using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Behaviors
{
    /// <summary>
    /// Aiming can be held, or toggled on/off.
    /// </summary>
    public class ToggleOrHoldBehavior : BaseActionBehavior
    {
        private bool _isHeld = false;
        private bool _isToggled = false;

        protected SimpleDelegate ToggleOn;
        protected SimpleDelegate ToggleOff;


        public ToggleOrHoldBehavior(SimpleDelegate toggleOn, SimpleDelegate toggleOff) 
            : base(StaticSettings.Controls.HoldToToggleAim)
        {
            ToggleOn = toggleOn;
            ToggleOff = toggleOff;
        }


        protected override void InitialPress() { } // do nothing...

        protected override void Held()
        {
            _isHeld = true;
            _isToggled = true;
            ToggleOn();
        }

        protected override void Release()
        {
            if (_isHeld)
                UnHold();
            else
                Toggle();
        }

        private void UnHold()
        {
            _isHeld = false;
            _isToggled = false;
            ToggleOff();
        }

        private void Toggle()
        {
            if (_isToggled)
                ToggleOff();
            else
                ToggleOn();
            _isToggled = !_isToggled;
        }
    }
}
