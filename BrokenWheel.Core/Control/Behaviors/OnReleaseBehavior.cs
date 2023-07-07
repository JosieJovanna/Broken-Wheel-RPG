using BrokenWheel.Core.Common.Delegates;
using BrokenWheel.Core.Control.Models;

namespace BrokenWheel.Core.Control.Behaviors
{
    public class OnReleaseBehavior : IActionBehavior
    {
        protected SimpleDelegate OnRelease;

        public OnReleaseBehavior(SimpleDelegate onRelease) 
        {
            OnRelease = onRelease;
        }

        public void Execute(PressData press)
        {
            if (press.Type == PressType.Released)
                OnRelease?.Invoke();
        }
    }
}
