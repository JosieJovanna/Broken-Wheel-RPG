namespace LorendisCore.Equipment.Implements.WeaponTypes
{
    public interface IAimable
    {
        bool IsAiming { get; }
        
        void StartAiming();
        void StopAiming();
    }
}