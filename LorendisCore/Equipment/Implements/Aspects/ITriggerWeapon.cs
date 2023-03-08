namespace LorendisCore.Equipment.Implements.Aspects
{
    public interface ITriggerWeapon : IReloadable, IAimable
    {
        void Fire();
    }
}