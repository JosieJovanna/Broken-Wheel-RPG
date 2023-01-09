using LorendisCore.Player.Control.Actions;

namespace LorendisCore.Weapons
{
    public abstract class BaseWeapon
    {
        protected IActionController Controller;

        private bool _isInOffhand = false;

        public BaseWeapon(IActionController actionController)
        {
            Controller = actionController;
        }

        public bool IsInOffhand() => _isInOffhand;

        public void Equip(bool offhanded = false)
        {
            _isInOffhand = offhanded;
        }

        protected abstract void EquipWeapon();
    }
}
