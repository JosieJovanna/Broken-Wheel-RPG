using LorendisCore.Player.Control;
using LorendisCore.Player.Control.Actions.Models;

namespace LorendisCore.Equipment.Implements
{
    /// <summary>
    /// A piece of equipment which may be held in the hand, modifying behaviors along with it.
    /// </summary>
    public interface IImplement
    {
        bool TryPrimary(ButtonData buttonData);
        bool TrySecondary(ButtonData buttonData);
        bool TrySpecial(ButtonData buttonData);
    }
}
