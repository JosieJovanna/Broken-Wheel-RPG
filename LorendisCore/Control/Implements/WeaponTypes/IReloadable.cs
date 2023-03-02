namespace LorendisCore.Control.Implements.WeaponTypes
{
    public interface IReloadable
    {
        bool IsReloading { get; }
        bool CanReload { get; }

        void StartReloading();
        void FinishReloading();
        void CancelReloading();
        void KeepReloading(double deltaTime);
    }
}