using BrokenWheel.Core.Common.Delegates;
using BrokenWheel.Core.Control.Models;

namespace BrokenWheel.Core.Control.Behaviors
{
    public class OnPressBehavior : IActionBehavior
    {
        protected SimpleDelegate OnInitialPress;

        public OnPressBehavior(SimpleDelegate onInitialPress)
        {
            OnInitialPress = onInitialPress;
        }

        public void Execute(PressData press)
        {
            if (press.Type == PressType.Clicked)
                OnInitialPress?.Invoke();
        }
    }
}
