using System;

namespace BrokenWheel.Control.Behaviors
{
    public class ModMuxBehavior : AbstractActionBehavior
    {
        public Action OnInitialPress;
        public Action OnAltInitialPress;
        public Action OnHeld;
        public Action OnAltHeld;
        public Action OnReleaseClick;
        public Action OnAltReleaseClick;
        public Action OnReleaseHold;
        public Action OnAltReleaseHold;

        public ModMuxBehavior(double holdTime)
            : base(holdTime)
        { }

        protected override void Press()
        {
            var action = IsModified ? OnAltInitialPress : OnInitialPress;
            action.Invoke();
        }

        protected override void Click()
        {
            var action = IsModified ? OnAltReleaseClick : OnReleaseClick;
            action.Invoke();
        }

        protected override void Held()
        {
            throw new NotImplementedException();
        }

        protected override void Hold()
        {
            var action = IsModified ? OnAltHeld : OnHeld;
            action.Invoke();
        }

        protected override void Release()
        {
            var action = IsModified ? OnAltReleaseHold : OnReleaseHold;
            action.Invoke();
        }
    }
}
