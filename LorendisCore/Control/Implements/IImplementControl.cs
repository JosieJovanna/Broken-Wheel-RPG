using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    /// <summary>
    /// The object which maps input to items for use. Behaviors vary greatly by item.
    /// </summary>
    public interface IImplementControl
    {
        /// <summary>
        /// Attempts to perform the primary action(s) of the implement, which may depend on the type of button press.
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        bool TryPrimary(PressData press);
    }
}
