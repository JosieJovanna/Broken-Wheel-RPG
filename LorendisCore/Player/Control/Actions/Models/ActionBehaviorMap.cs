namespace LorendisCore.Player.Control.Actions.Models
{
    /// <summary>
    /// A class which contains all of the behaviors which regularly change depending on equipment.
    /// Rather than having each behavior individually, which would mean many changes if a new one
    /// were to be added, all references to behaviors are through here.
    /// </summary>
    public class ActionBehaviorMap
    {
        public IActionBehavior MainPrimary;
        public IActionBehavior MainSecondary;
        public IActionBehavior OffhandPrimary;
        public IActionBehavior OffhandSecondary;
        public IActionBehavior Special;

        public IActionBehavior Interact;
        public IActionBehavior Reload; // TODO: make reload capable of reloading both sides. Or no dual wield guns?
        public IActionBehavior Ability;
        public IActionBehavior Kick;
        public IActionBehavior Grab;
    }
}
