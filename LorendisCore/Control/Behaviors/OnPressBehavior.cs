using LorendisCore.Common.Delegates;
using LorendisCore.Control.Models;

namespace LorendisCore.Control.Behaviors
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
