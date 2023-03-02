using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    public interface ITwoHandedImplementControl : IImplementControl
    {
        bool TrySecondary(PressData pressData);
    }
}