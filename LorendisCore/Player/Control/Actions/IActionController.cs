using LorendisCore.Player.Control.Actions.Behaviors;

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
        IActionBehavior MainPrimaryBehavior { get; set; }
        IActionBehavior MainSecondaryBehavior { get; set; }
        IActionBehavior OffhandPrimaryBehavior { get; set; }
        IActionBehavior OffhandSecondaryBehavior { get; set; }
        IActionBehavior SpecialBehavior { get; set; }

        IActionBehavior InteractBehavior { get; set; }
        IActionBehavior ReadyWeaponBehavior { get; set; }
        IActionBehavior AbilityBehavior { get; set; }
        IActionBehavior KickBehavior { get; set; }
        IActionBehavior GrabBehavior { get; set; }


        /// <summary> Main-hand primary action, usually the left mouse button. </summary>
        void MainPrimary(ButtonPressData buttonPress);

        /// <summary> Main-hand secondary action, usually shift + LMB. </summary>
        void MainSecondary(ButtonPressData buttonPress);

        /// <summary> Off-hand primary action, usually the right mouse button. </summary>
        void OffhandPrimary(ButtonPressData buttonPress);

        /// <summary> Off-hand secondary action, usually shift + RMB. </summary>
        void OffhandSecondary(ButtonPressData buttonPress);

        /// <summary> Special weapon attack, which may be either hand. Often stuff like an overhead attack. </summary>
        void Special(ButtonPressData buttonPress);

        /// <summary> Main action, usually performed with 'E'. Opening doors, talking, etc. </summary>
        void Interact(ButtonPressData buttonPress);

        /// <summary> Readys weapon, reloads, etc. Likely 'R'. </summary>
        void ReadyWeapon(ButtonPressData buttonPress);

        /// <summary> Uses the special ability, usually class specific, or granted by an item. </summary>
        void UseAbility(ButtonPressData buttonPress);

        /// <summary> Kicks, usually just a normal kick but can be replaced or could be using other fighting style. </summary>
        void Kick(ButtonPressData buttonPress);

        /// <summary> Grabs a nearby entity, ladder, etc. </summary>
        void Grab(ButtonPressData buttonPress);
    }
}
