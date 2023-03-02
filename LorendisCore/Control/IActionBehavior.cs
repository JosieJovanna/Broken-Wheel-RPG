using LorendisCore.Control.Models;

namespace LorendisCore.Control
{
    /// <summary>
    /// A behavior for some kind of player action -- usually an attack. Does not control movement.
    /// These behaviors usually act differently based on whether the button controlling it is
    /// pressed, held, or released. (Naturally, not being held or pressed does nothing.)
    /// The change in time is also required, as usually for holding a button, there is a certain 
    /// threshold. For example, aiming a gun, versus just hip-firing, takes a bit to start, and
    /// it is unlikely for the player to press the button for only one frame.
    /// </summary>
    public interface IActionBehavior
    {
        void Execute(PressData press);
    }
}
