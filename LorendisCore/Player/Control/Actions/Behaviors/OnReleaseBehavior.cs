using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Behaviors
{
    public class OnReleaseBehavior : IActionBehavior
    {
        protected SimpleDelegate OnRelease;

        public OnReleaseBehavior(SimpleDelegate onRelease) 
        {
            OnRelease = onRelease;
        }

        public void Execute(ButtonData buttonData)
        {
            if (buttonData.Type == ButtonPressType.Released)
                OnRelease?.Invoke();
        }
    }
}
