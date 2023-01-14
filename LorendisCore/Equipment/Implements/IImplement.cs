namespace LorendisCore.Equipment.Implements
{
    /// <summary>
    /// A piece of equipment which may be held in the hand, modifying behaviors along with it.
    /// </summary>
    public interface IImplement
    {
        /// <summary>
        /// Equips the implement and changes appropriate behaviors.
        /// </summary>
        void Equip(bool offhand = false);
        
        /// <summary>
        /// Unequips the implement and resets the appropriate behaviors.
        /// </summary>
        void Unequip();
    }
}
