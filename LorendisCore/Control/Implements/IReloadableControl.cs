namespace LorendisCore.Control.Implements
{
    public interface IReloadableControl
    {
        bool IsReloading { get; }
        bool CanReload { get; }
    }
}