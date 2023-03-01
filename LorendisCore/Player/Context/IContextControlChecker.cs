using LorendisCore.Equipment.Implements;

namespace LorendisCore.Player.Context
{
    /// <summary>
    /// Depending on the player's physical state and surroundings, possible actions may change.
    /// This class is used to get what current actions may be performed in the moment.
    /// </summary>
    public interface IContextControlChecker
    {
        IImplement GetInteractControl();
        IImplement GetGrabControl();
    }
}