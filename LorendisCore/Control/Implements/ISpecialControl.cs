using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    /// <summary>
    /// An interface for implements which have a special action.
    /// </summary>
    public interface ISpecialControl
    {
        /// <summary>
        /// Attempts to perform the special action of the implement.
        /// As two implements may have a special each, special actions should not differ by alt presses.
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        void TrySpecial(PressData press);
    }
}