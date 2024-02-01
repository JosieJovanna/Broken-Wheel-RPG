namespace BrokenWheel.Core.Equipment.Implements.Aspects
{
    public interface IAimable
    {
        bool IsAiming { get; }

        void StartAiming();
        void StopAiming();
    }
}
