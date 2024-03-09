using System;

namespace BrokenWheel.Control.Behaviors
{
    /// <summary>
    /// Aiming can be held, or toggled on/off.
    /// </summary>
    public class ToggleOrHoldBehavior : AbstractActionBehavior
    {
        protected bool IsToggled { get; private set; } = false;

        protected Action ToggleOn;
        protected Action ToggleOff;

        public ToggleOrHoldBehavior(
            double holdTime,
            Action toggleOn,
            Action toggleOff)
            : base(holdTime)
        {
            ToggleOn = toggleOn;
            ToggleOff = toggleOff;
        }

        public ToggleOrHoldBehavior(
            Func<double> holdTimeGetter,
            Action toggleOn,
            Action toggleOff)
            : base(holdTimeGetter)
        {
            ToggleOn = toggleOn;
            ToggleOff = toggleOff;
        }

        protected override void Press() { } // do nothing...

        protected override void Click()
        {
            if (IsToggled)
                ToggleOff();
            else
                ToggleOn();
            IsToggled = !IsToggled;
        }

        protected override void Hold()
        {
            IsToggled = true;
            ToggleOn();
        }

        protected override void Release()
        {
            IsToggled = false;
            ToggleOff();
        }
    }
}
