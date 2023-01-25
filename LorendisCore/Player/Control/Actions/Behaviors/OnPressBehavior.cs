using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Behaviors
{
    public class OnPressBehavior : IActionBehavior
    {
        protected SimpleDelegate OnInitialPress;

        public OnPressBehavior(SimpleDelegate onInitialPress)
        {
            OnInitialPress = onInitialPress;
        }

        public void Execute(ButtonData buttonData)
        {
            if (buttonData.Type == ButtonPressType.Pressed)
                OnInitialPress?.Invoke();
        }
    }
}
