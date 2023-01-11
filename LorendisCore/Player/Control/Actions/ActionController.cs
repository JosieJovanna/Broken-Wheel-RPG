namespace LorendisCore.Player.Control.Actions
{
    /// <summary>
    /// The default implementation of <see cref="IActionController"/>
    /// </summary>
    public class ActionController : IActionController
    {
        public ActionBehaviorMap Behaviors { get; set; }

        public ActionController()
        {
            Behaviors = new ActionBehaviorMap();
        }

        public ActionController(ActionBehaviorMap behaviors)
        {
            Behaviors = behaviors;
        }

        public void MainPrimary(ButtonPressData buttonPress) 
            => Behaviors.MainPrimary?.Act(buttonPress);

        public void MainSecondary(ButtonPressData buttonPress) 
            => Behaviors.MainSecondary?.Act(buttonPress);

        public void OffhandPrimary(ButtonPressData buttonPress) 
            => Behaviors.OffhandPrimary?.Act(buttonPress);

        public void OffhandSecondary(ButtonPressData buttonPress) 
            => Behaviors.OffhandSecondary?.Act(buttonPress);

        public void Special(ButtonPressData buttonPress) 
            => Behaviors.Special?.Act(buttonPress);

        public void Interact(ButtonPressData buttonPress) 
            => Behaviors.Interact?.Act(buttonPress);

        public void ReadyWeapon(ButtonPressData buttonPress) 
            => Behaviors.ReadyWeapon?.Act(buttonPress);

        public void UseAbility(ButtonPressData buttonPress) 
            => Behaviors.Ability?.Act(buttonPress);

        public void Kick(ButtonPressData buttonPress) 
            => Behaviors.Kick?.Act(buttonPress);

        public void Grab(ButtonPressData buttonPress)
            => Behaviors.Grab?.Act(buttonPress);
    }
}
