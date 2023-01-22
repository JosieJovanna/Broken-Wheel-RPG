namespace LorendisCore.Equipment.Implements.WeaponTypes
{
    public interface ITriggerWeapon : IReloadable, IAimable
    {
        void Fire();
    }
}