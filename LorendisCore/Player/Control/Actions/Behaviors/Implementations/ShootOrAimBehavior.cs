using LorendisCore.Common.Delegates;

namespace LorendisCore.Player.Control.Actions.Behaviors.Implementations
{
    public class ShootOrAimBehavior : DelegatedActionBehavior
    {
        public ShootOrAimBehavior(double holdTime, SimpleDelegate shootDelegate, SimpleDelegate aimDelegate)
            : base(holdTime)
        {
            AddOnReleaseDelegate(shootDelegate);
            AddOnHeldDelegate(aimDelegate);
        }
    }
}
