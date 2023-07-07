namespace BrokenWheel.Control.Interfaces
{
    public interface IReloadableControl : IOneHandControl
    {
        bool IsReloading { get; }
        bool CanReload { get; }

        void TryReload();
        void CancelReload();
    }
}
