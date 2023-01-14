using LorendisCore.Player.Control.Actions.Behaviors;
using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Player.Control.Actions
{
    /// <summary>
    /// The object which is controlled by the player. Separate from reading input.
    /// This may control the player character, a beast, a foe, or any manner of things.
    /// It dictates which actions are performed by clicking, such as swinging swords, blocking, etc.
    /// The behavior itself may have restrictions placed upon it, such as attack rate.
    /// These behaviors can be changed: the inventory system mainly, looking at things, effects, etc.
    /// There should only ever be one controller active, however, one could feasably an entirely new
    /// controller to temporarily replace the main one, rather than individually changing each behavior
    /// (i.e. in cases of posession or transformation).
    /// </summary>
    public interface IActionController
    {
        ActionBehaviorMap Behaviors { get; set; }

        /// <summary> Main-hand primary action, usually the left mouse button. </summary>
        void MainPrimary(ButtonData button);

        /// <summary> Main-hand secondary action, usually shift + LMB. </summary>
        void MainSecondary(ButtonData button);

        /// <summary> Off-hand primary action, usually the right mouse button. </summary>
        void OffhandPrimary(ButtonData button);

        /// <summary> Off-hand secondary action, usually shift + RMB. </summary>
        void OffhandSecondary(ButtonData button);

        /// <summary> Special weapon attack, which may be either hand. Often stuff like an overhead attack. </summary>
        void Special(ButtonData button);

        /// <summary> Main action, usually performed with 'E'. Opening doors, talking, etc. </summary>
        void Interact(ButtonData button);

        /// <summary> Readys weapon, reloads, etc. Likely 'R'. Holding is raising and lowering weapons, always </summary>
        void Reload(ButtonData button);

        /// <summary> Uses the special ability, usually class specific, or granted by an item. </summary>
        void UseAbility(ButtonData button);

        /// <summary> Kicks, usually just a normal kick but can be replaced or could be using other fighting style. </summary>
        void Kick(ButtonData button);

        /// <summary> Grabs a nearby entity, ladder, etc. </summary>
        void Grab(ButtonData button);
    }
}
