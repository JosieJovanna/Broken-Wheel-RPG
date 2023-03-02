namespace LorendisCore.Control.Implements
{
    public interface IReloadableControl
    {
        bool IsReloading { get; }
        bool CanReload { get; }

        bool TryReload();
        bool AbortReload();
    }
}