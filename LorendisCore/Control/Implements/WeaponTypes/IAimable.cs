namespace LorendisCore.Control.Implements.WeaponTypes
{
    public interface IAimable
    {
        bool IsAiming { get; }
        
        void StartAiming();
        void StopAiming();
    }
}