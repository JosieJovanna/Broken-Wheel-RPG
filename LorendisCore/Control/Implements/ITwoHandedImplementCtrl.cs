using LorendisCore.Control.Models;
using LorendisCore.Equipment.Implements;

namespace LorendisCore.Control.Implements
{
    public interface ITwoHandedImplementCtrl : IImplementCtrl
    {
        bool TrySecondary(PressData pressData);
        bool TryAltSecondary(PressData pressData);
    }
}