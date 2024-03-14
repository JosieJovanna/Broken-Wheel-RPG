using BrokenWheel.Control.Events;

namespace BrokenWheel.Control.Actions
{
    /// <summary>
    /// A behavior for some kind of player action -- usually an attack.
    /// These behaviors usually act differently based on whether the button controlling it is
    /// pressed, held, or released. (Naturally, not being held or pressed does nothing.)
    /// The change in time is also required, as usually for holding a button, there is a certain 
    /// threshold. For example, aiming a gun, versus just hip-firing, takes a bit to start, and
    /// it is unlikely for the player to press the button for only one frame.
    /// </summary>
    public interface IActionBehavior
    {
        /// <summary>
        /// Executes the appropriate action (if any) based off of the given button input.
        /// </summary>
        /// <param name="data"> Button input data for this frame. </param>
        /// <param name="isModified"> Whether the modifier is held. False by default. </param>
        void Execute(ButtonInputEvent data, bool isModified = false);

        /// <summary>
        /// Action behavior settings may be changed while in the menu.
        /// Whenever this happens, this function should be called.
        /// </summary>
        void Refresh();
    }
}
