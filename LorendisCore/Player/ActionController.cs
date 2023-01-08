using LorendisCore.Player.Behaviors;

namespace LorendisCore.Player
{
    public class ActionController : IActionController
    {
        public IActionBehavior MainPrimaryBehavior { get; set; }
        public IActionBehavior MainSecondaryBehavior { get; set; }
        public IActionBehavior OffhandPrimaryBehavior { get; set; }
        public IActionBehavior OffhandSecondaryBehavior { get; set; }
        public IActionBehavior SpecialBehavior { get; set; }

        public IActionBehavior InteractBehavior { get; set; }
        public IActionBehavior ReadyWeaponBehavior { get; set; }
        public IActionBehavior AbilityBehavior { get; set; }
        public IActionBehavior KickBehavior { get; set; }


        public void MainPrimary(ButtonPressData buttonPress)
        {
            MainPrimaryBehavior?.Act(buttonPress);
        }

        public void MainSecondary(ButtonPressData buttonPress)
        {
            MainSecondaryBehavior?.Act(buttonPress);
        }

        public void OffhandPrimary(ButtonPressData buttonPress)
        {
            OffhandPrimaryBehavior?.Act(buttonPress);
        }

        public void OffhandSecondary(ButtonPressData buttonPress)
        {
            OffhandSecondaryBehavior?.Act(buttonPress);
        }

        public void Special(ButtonPressData buttonPress)
        {
            SpecialBehavior?.Act(buttonPress);
        }


        public void Interact(ButtonPressData buttonPress)
        {
            InteractBehavior?.Act(buttonPress);
        }

        public void ReadyWeapon(ButtonPressData buttonPress)
        {
            ReadyWeaponBehavior?.Act(buttonPress);
        }

        public void UseAbility(ButtonPressData buttonPress)
        {
            AbilityBehavior?.Act(buttonPress);
        }

        public void Kick(ButtonPressData buttonPress)
        {
            KickBehavior?.Act(buttonPress);
        }
    }
}
