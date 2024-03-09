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
        public Action OnPress;
        public Action OnClick;
        public Action OnHoldStart;
        public Action OnHold;
        public Action OnRelease;

        public MuxBehavior(double holdTime)
            : base(holdTime)
        { }

        public MuxBehavior(Func<double> holdTimeGetter)
            : base(holdTimeGetter)
        { }

        protected override void Press() => OnPress?.Invoke();
        protected override void Click() => OnClick?.Invoke();
        protected override void Held() => OnHoldStart?.Invoke();
        protected override void Hold() => OnHold?.Invoke();
        protected override void Release() => OnRelease?.Invoke();
    }
}
