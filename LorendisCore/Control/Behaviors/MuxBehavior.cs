using LorendisCore.Common.Delegates;

namespace LorendisCore.Control.Behaviors
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

        public MuxBehavior(ref double holdTime) : base(ref holdTime) { }

        protected override void InitialPress(bool isAltPress) => OnInitialPress?.Invoke();
        protected override void Held(bool isAltPress) => OnHeld?.Invoke();
        protected override void ReleaseClick(bool isAltPress) => OnReleaseClick?.Invoke();
        protected override void ReleaseHold(bool isAltPress) => OnReleaseHold?.Invoke();
    }
}
