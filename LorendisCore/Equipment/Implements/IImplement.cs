using LorendisCore.Player.Control;

namespace LorendisCore.Equipment.Implements
{
    /// <summary>
    /// The object which maps input to items for use. Behaviors vary greatly by item.
    /// </summary>
    public interface IImplement
    {
        bool HasSpecial { get; }
        
        /// <summary>
        /// Attempts to perform the primary action(s) of the implement, which may depend on the type of button press.
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        bool TryPrimaryPress(PressData press);
        
        /// <summary>
        /// Attempts to perform the alternate primary action(s) of the implement,
        /// which may depend on the type of button press.
        /// If there is no alternate primary, it will use the normal primary.
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        bool TryAltPrimaryPress(PressData press);
        
        /// <summary>
        /// Attempts to perform the special action(s) of the implement, which may depend on the type of button press.
        /// May not 
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        bool TrySpecialPress(PressData press);
    }
}
