using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Behaviors
{
    public class OnReleaseBehavior : IActionBehavior
    {
        protected SimpleDelegate StopAimingDelegate;

        public OnReleaseBehavior(SimpleDelegate stopAimingDelegate) 
        {
            StopAimingDelegate = stopAimingDelegate;
        }

        public void Act(ButtonPressData buttonPressData)
        {
            if (!buttonPressData.IsPressed)
                StopAimingDelegate?.Invoke();
        }
    }
}
