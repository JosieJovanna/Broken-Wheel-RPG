using LorendisCore.Control.Models;

namespace LorendisCore.Control.Implements
{
    public interface ITwoHandedControl : IBaseControl
    {
        bool TrySecondary(PressData pressData);
    }
}