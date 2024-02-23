using BrokenWheel.Control.Actions;
using BrokenWheel.Control.Enum;
using BrokenWheel.Control.Models;
using BrokenWheel.Core;

namespace BrokenWheel.Control.Behaviors
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
