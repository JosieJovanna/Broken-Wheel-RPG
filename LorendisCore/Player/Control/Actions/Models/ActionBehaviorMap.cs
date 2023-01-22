using LorendisCore.Settings;
using LorendisCore.Common.Delegates;
using LorendisCore.Player.Control.Actions.Behaviors;

namespace LorendisCore.Player.Control.Actions.Models
{
    /// <summary>
    /// A class which contains all of the behaviors which regularly change depending on equipment.
    /// Rather than having each behavior individually, which would mean many changes if a new one
    /// were to be added, all references to behaviors are through here.
    /// </summary>
    public class ActionBehaviorMap
    {
        private readonly MuxBehavior _reload = new MuxBehavior(StaticSettings.Controls.HoldToReadyWeapon);
        
        public IActionBehavior MainPrimary;
        public IActionBehavior MainSecondary;
        public IActionBehavior OffhandPrimary;
        public IActionBehavior OffhandSecondary;
        public IActionBehavior Special;
        
        
        public IActionBehavior MainPrimaryOverride;
        public IActionBehavior MainSecondaryOverride;
        public IActionBehavior OffhandPrimaryOverride;
        public IActionBehavior OffhandSecondaryOverride;
        public IActionBehavior SpecialOverride;

        public IActionBehavior Interact;
        public IActionBehavior Ability;
        public IActionBehavior Kick;
        public IActionBehavior Grab;

        public IActionBehavior Reload => _reload;

        public void AddReloadDelegate(SimpleDelegate onReload) => _reload.OnReleaseClick += onReload;

        public void RemoveReloadDelegate(SimpleDelegate onReload) => _reload.OnReleaseClick -= onReload;
    }
}
