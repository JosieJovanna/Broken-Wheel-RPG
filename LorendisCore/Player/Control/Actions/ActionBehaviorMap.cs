namespace LorendisCore.Player.Control.Actions
{
    /// <summary>
    /// A class which contains all of the behaviors which regularly change depending on equipment.
    /// Rather than having each behavior individually, which would mean many changes if a new one
    /// were to be added, all references to behaviors are through here.
    /// </summary>
    public class ActionBehaviorMap
    {
        public IActionBehavior MainPrimary { get; set; }
        public IActionBehavior MainSecondary { get; set; }
        public IActionBehavior OffhandPrimary { get; set; }
        public IActionBehavior OffhandSecondary { get; set; }
        public IActionBehavior Special { get; set; }

        public IActionBehavior Interact { get; set; }
        public IActionBehavior ReadyWeapon { get; set; }
        public IActionBehavior Ability { get; set; }
        public IActionBehavior Kick { get; set; }
        public IActionBehavior Grab { get; set; }
    }
}
