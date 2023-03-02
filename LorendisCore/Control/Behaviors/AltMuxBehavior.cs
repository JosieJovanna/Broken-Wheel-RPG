using LorendisCore.Common.Delegates;

namespace LorendisCore.Control.Behaviors
{
    public class AltMuxBehavior : AbstractActionBehavior
    {
        public SimpleDelegate OnInitialPress;
        public SimpleDelegate OnAltInitialPress;
        public SimpleDelegate OnHeld;
        public SimpleDelegate OnAltHeld;
        public SimpleDelegate OnReleaseClick;
        public SimpleDelegate OnAltReleaseClick;
        public SimpleDelegate OnReleaseHold;
        public SimpleDelegate OnAltReleaseHold;

        public AltMuxBehavior(double holdTime) : base(holdTime)
        {
        }

        protected override void InitialPress(bool isAltPress)
        {
            var action = isAltPress ? OnAltInitialPress : OnInitialPress;
            action.Invoke();
        }

        protected override void Held(bool isAltPress)
        {
            var action = isAltPress ? OnAltHeld : OnHeld;
            action.Invoke();
        }

        protected override void ReleaseClick(bool isAltPress)
        {
            var action = isAltPress ? OnAltReleaseClick : OnReleaseClick;
            action.Invoke();
        }

        protected override void ReleaseHold(bool isAltPress)
        {
            var action = isAltPress ? OnAltReleaseHold : OnReleaseHold;
            action.Invoke();
        }
    }
}