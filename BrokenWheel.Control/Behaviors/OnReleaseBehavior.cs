using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Models;
using BrokenWheel.Core;

namespace BrokenWheel.Control.Behaviors
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
