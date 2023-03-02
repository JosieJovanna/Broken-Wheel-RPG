using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    /// <summary>
    /// An interface for implements which have a special action.
    /// </summary>
    public interface ISpecial
    {
        /// <summary>
        /// Attempts to perform the special action(s) of the implement, which may depend on the type of button press.
        /// May not 
        /// </summary>
        /// <returns>Whether or not any action could be taken.</returns>
        bool TrySpecial(PressData press);
    }
}