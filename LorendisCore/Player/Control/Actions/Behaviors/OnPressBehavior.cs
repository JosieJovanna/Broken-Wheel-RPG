using LorendisCore.Common.Delegates;
using LorendisCore.LorendisCore.Player.Control.Actions;

namespace LorendisCore.Player.Control.Actions.Behaviors
{
    public class OnPressBehavior : IActionBehavior
    {
        protected SimpleDelegate OnInitialPress;

        public OnPressBehavior(SimpleDelegate onInitialPress)
        {
            OnInitialPress = onInitialPress;
        }

        public void Act(ButtonPressData buttonPressData)
        {
            if (buttonPressData.GetButtonPressType() == ButtonPressType.Pressed)
                OnInitialPress?.Invoke();
        }
    }
}
