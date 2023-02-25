using LorendisCore.Player.Control;

namespace LorendisCore.Equipment.Implements
{
    public interface ITwoHandedImplement : IImplement
    {
        bool TrySecondary(PressData pressData);
        bool TryAltSecondary(PressData pressData);
    }
}