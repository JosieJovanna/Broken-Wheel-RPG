using LorendisCore.Common.Delegates;

namespace LorendisCore.Control.Actions.Behaviors
{
    /// <summary>
    /// A concrete <see cref="AbstractActionBehavior"/> where on press, hold, and release,
    /// different delegates can be added to be invoked.
    /// These delegates must not have data passed in or out, and is located in instances of classes
    /// which have access to the relevant data via different means.
    /// </summary>
    public class MuxBehavior : AbstractActionBehavior
    {
        public SimpleDelegate OnInitialPress;
        public SimpleDelegate OnHeld;
        public SimpleDelegate OnReleaseClick;
        public SimpleDelegate OnReleaseHold;

        public MuxBehavior(double holdTime) : base(holdTime) { }

        protected override void InitialPress() => OnInitialPress?.Invoke();
        protected override void Held() => OnHeld?.Invoke();
        protected override void ReleaseClick() => OnReleaseClick?.Invoke();
        protected override void ReleaseHold() => OnReleaseHold?.Invoke();
    }
}
