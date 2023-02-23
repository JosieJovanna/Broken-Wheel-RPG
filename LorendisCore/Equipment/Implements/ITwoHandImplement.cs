using LorendisCore.Player.Control;

namespace LorendisCore.Equipment.Implements
{
    public interface ITwoHandImplement : IImplement
    {
        bool TryOffPrimary(PressData pressData);
        bool TryOffSecondary(PressData pressData);
    }
}