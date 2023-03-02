using LorendisCore.Control.Implements;

namespace LorendisCore.Player
{
    /// <summary>
    /// Depending on the player's physical state and surroundings, possible actions may change.
    /// This class is used to get what current actions may be performed in the moment.
    /// </summary>
    public interface IContextControlChecker
    {
        bool IsInCombat { get; }
        
        IBaseControl GetInteractControl();
        IBaseControl GetGrabControl();
    }
}