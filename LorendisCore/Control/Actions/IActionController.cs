using LorendisCore.Control.Models;

namespace LorendisCore.Control.Actions
{
    /// <summary>
    /// The object which maps actions and their input to control.
    /// It dictates which actions are performed by clicking, such as swinging swords, blocking, etc.
    /// The behavior itself may have restrictions placed upon it, such as attack rate.
    /// These behaviors can be changed: the inventory system mainly, looking at things, effects, etc.
    /// There should only ever be one controller active, however, one could feasibly an entirely new
    /// controller to temporarily replace the main one, rather than individually changing each behavior
    /// (i.e. in cases of possession or transformation).
    /// </summary>
    public interface IActionController
    {
        /// <summary> Main action, usually performed with 'E'. Opening doors, talking, etc. </summary>
        void Interact(PressData press);

        /// <summary> Grabs a nearby entity, ladder, etc. </summary>
        void Grab(PressData press);

        /// <summary> Readys weapon, reloads, etc. Likely 'R'. Holding is raising and lowering weapons, always </summary>
        void ReloadOrReady(PressData press);
        
        /// <summary> Main-hand primary action, usually the left mouse button. </summary>
        void MainHand(PressData press);

        /// <summary> Off-hand primary action, usually the right mouse button. </summary>
        void OffHand(PressData press);

        /// <summary> Special weapon attack, checking main hand first. Often stuff like an overhead attack. </summary>
        void Special(PressData press);

        /// <summary> Uses the special ability, usually class specific, or granted by an item. </summary>
        void UseAbility(PressData press);

        /// <summary> Kicks, usually just a normal kick but can be replaced or could be using other fighting style. </summary>
        void Kick(PressData press);
    }
}
