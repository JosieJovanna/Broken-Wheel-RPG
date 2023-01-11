using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Behaviors
{
    /// <summary>
    /// A concrete <see cref="BaseActionBehavior"/> where on press, hold, and release,
    /// different delegates can be added to be invoked.
    /// These delegates must not have data passed in or out, and is located in instances of classes
    /// which have access to the relevant data via different means.
    /// </summary>
    public class PressHoldReleaseBehavior : BaseActionBehavior
    {
        protected SimpleDelegate OnInitialPress;
        protected SimpleDelegate OnRelease;
        protected SimpleDelegate OnHeld;

        public PressHoldReleaseBehavior(double holdTime) 
            : base(holdTime)
        { }

        public PressHoldReleaseBehavior(double holdTime,
            SimpleDelegate onInitialPress, SimpleDelegate onRelease, SimpleDelegate onHeld)
            : base(holdTime)
        {
            OnInitialPress = onInitialPress;
            OnRelease = onRelease;
            OnHeld = onHeld;
        }

        protected override void InitialPress() => OnInitialPress?.Invoke();
        protected override void Release() => OnRelease?.Invoke();
        protected override void Held() => OnHeld?.Invoke();

        public void AddOnInitialPressDelegate(SimpleDelegate del) => OnInitialPress += del;

        public void AddOnReleaseDelegate(SimpleDelegate del) => OnRelease += del;

        public void AddOnHeldDelegate(SimpleDelegate del) => OnHeld += del;
    }
}
