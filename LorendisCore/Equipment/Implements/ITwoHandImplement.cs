using LorendisCore.Player.Control;

namespace LorendisCore.Equipment.Implements
{
    public interface ITwoHandImplement : IImplement
    {
        bool TryOffPrimary(ButtonData buttonData);
        bool TryOffSecondary(ButtonData buttonData);
    }
}