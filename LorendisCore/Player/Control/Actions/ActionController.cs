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
            => (Behaviors.MainPrimaryOverride ?? Behaviors.MainPrimary)?.Execute(button);

        public void MainSecondary(ButtonData button) 
            => (Behaviors.MainSecondaryOverride ?? Behaviors.MainSecondary)?.Execute(button);

        public void OffhandPrimary(ButtonData button) 
            => (Behaviors.OffhandPrimaryOverride ?? Behaviors.OffhandPrimary)?.Execute(button);

        public void OffhandSecondary(ButtonData button) 
            => (Behaviors.OffhandSecondaryOverride ?? Behaviors.OffhandSecondary)?.Execute(button);

        public void Special(ButtonData button) 
            => (Behaviors.SpecialOverride ?? Behaviors.Special)?.Execute(button);

        public void Interact(ButtonData button) => Behaviors.Interact?.Execute(button);

        public void UseAbility(ButtonData button) => Behaviors.Ability?.Execute(button);

        public void Kick(ButtonData button) => Behaviors.Kick?.Execute(button);

        public void Grab(ButtonData button) => Behaviors.Grab?.Execute(button);

        public void Reload(ButtonData button) => Behaviors.Reload.Execute(button);
    }
}
