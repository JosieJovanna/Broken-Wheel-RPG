using LorendisCore.Common.Delegates;
using LorendisCore.Control.Models;

namespace LorendisCore.Control.Actions.Behaviors
{
    public class OnReleaseBehavior : IActionBehavior
    {
        protected SimpleDelegate OnRelease;

        public OnReleaseBehavior(SimpleDelegate onRelease) 
        {
            OnRelease = onRelease;
        }

        public void Execute(PressData pressData)
        {
            if (pressData.Type == PressType.Released)
                OnRelease?.Invoke();
        }
    }
}
