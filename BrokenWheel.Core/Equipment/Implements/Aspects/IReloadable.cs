namespace BrokenWheel.Core.Equipment.Implements.Aspects
{
    public interface IReloadable
    {
        bool IsReloading { get; }
        bool CanReload { get; }

        void StartReloading();
        void FinishReloading();
        void CancelReloading();
    }
}
