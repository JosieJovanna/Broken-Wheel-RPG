using BrokenWheel.Control.Models;

namespace BrokenWheel.Control.Interfaces
{
    /// <summary>
    /// An interface for implements which have a special action.
    /// </summary>
    public interface ISpecialControl : IOneHandControl
    {
        /// <summary>
        /// Attempts to perform the special action of the implement.
        /// As two implements may have a special each, special actions should not differ by alt presses.
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        void TrySpecial(PressData press);
    }
}
