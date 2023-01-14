using LorendisCore.Player.Control.Actions.Models;

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

        public void MainPrimary(ButtonData button) 
            => (Behaviors.MainPrimaryOverride ?? Behaviors.MainPrimary)?.Act(button);

        public void MainSecondary(ButtonData button) 
            => (Behaviors.MainSecondaryOverride ?? Behaviors.MainSecondary)?.Act(button);

        public void OffhandPrimary(ButtonData button) 
            => (Behaviors.OffhandPrimaryOverride ?? Behaviors.OffhandPrimary)?.Act(button);

        public void OffhandSecondary(ButtonData button) 
            => (Behaviors.OffhandSecondaryOverride ?? Behaviors.OffhandSecondary)?.Act(button);

        public void Special(ButtonData button) 
            => (Behaviors.SpecialOverride ?? Behaviors.Special)?.Act(button);

        public void Interact(ButtonData button) 
            => Behaviors.Interact?.Act(button);

        public void UseAbility(ButtonData button) 
            => Behaviors.Ability?.Act(button);

        public void Kick(ButtonData button) 
            => Behaviors.Kick?.Act(button);

        public void Grab(ButtonData button)
            => Behaviors.Grab?.Act(button);

        public void Reload(ButtonData button)
        {
            
        }
    }
}
