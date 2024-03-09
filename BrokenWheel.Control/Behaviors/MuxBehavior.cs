using System;

namespace BrokenWheel.Control.Behaviors
{
    /// <summary>
    /// A concrete <see cref="AbstractActionBehavior"/> where on press, hold, and release,
    /// different delegates can be added to be invoked.
    /// These delegates must not have data passed in or out, and is located in instances of classes
    /// which have access to the relevant data via different means.
    /// </summary>
    public class MuxBehavior : AbstractActionBehavior
    {
        public Action OnInitialPress;
        public Action OnHeld;
        public Action OnReleaseClick;
        public Action OnReleaseHold;

        public MuxBehavior(double holdTime)
            : base(holdTime)
        { }

        public MuxBehavior(Func<double> holdTimeGetter)
            : base(holdTimeGetter)
        { }

        protected override void Press() => OnInitialPress?.Invoke();
        protected override void Hold() => OnHeld?.Invoke();
        protected override void Click() => OnReleaseClick?.Invoke();
        protected override void Release() => OnReleaseHold?.Invoke();
    }
}
