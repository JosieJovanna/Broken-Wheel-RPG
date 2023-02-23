using LorendisCore.Player.Control;

namespace LorendisCore.Equipment.Implements
{
    public interface TwoHandedImplement : IImplement
    {
        bool TryOffHandPrimary(PressData pressData);
        bool TryOffHandSecondary(PressData pressData);
    }
}