namespace BrokenWheel.Core.Control.Implements
{
    public interface IReloadableControl : IOneHandControl
    {
        bool IsReloading { get; }
        bool CanReload { get; }

        void TryReload();
        void CancelReload();
    }
}