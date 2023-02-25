using LorendisCore.Equipment.Implements.WeaponTypes;

namespace LorendisCore.Equipment.Implements
{
    /// <summary>
    /// Maps input to actions for implements which may be either one- or two-handed, toggled by special.
    /// </summary>
    public interface IVersatileImplement : ITwoHandedImplement, ISpecial
    {
        bool IsOneHandedGrip { get; }
        bool IsTwoHandedGrip { get; }
        
        /// <summary>
        /// Sets the grip of the implement.
        /// </summary>
        /// <param name="twoHanded">True to hold two-handed, False to hold one-handed.</param>
        void SetGrip(bool twoHanded);
        
        /// <summary>
        /// Toggles the grip of the implement between one- and two-handed.
        /// </summary>
        /// <returns>True if two-handed, False if one-handed.</returns>
        bool ToggleGrip();
    }
}