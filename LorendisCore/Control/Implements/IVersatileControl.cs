using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    /// <summary>
    /// Maps input to actions for implements which may be either one- or two-handed, toggled by special.
    /// </summary>
    public interface IVersatileControl : ITwoHandedControl
    {
        bool IsOneHandedGrip { get; }
        bool IsTwoHandedGrip { get; }
        
        /// <summary>
        /// Toggles the grip of the implement between one- and two-handed.
        /// </summary>
        /// <returns>True if two-handed, False if one-handed.</returns>
        bool TryToggleGrip(PressData press);
    }
}