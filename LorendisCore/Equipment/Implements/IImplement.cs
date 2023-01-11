namespace LorendisCore.Equipment.Implements
{
    /// <summary>
    /// A piece of equipment which may be held in the hand, modifying behaviors along with it.
    /// </summary>
    public interface IImplement
    {
        /// <summary>
        /// Equips the implement and changes appropriate behaviors. This implementation is handled 
        /// </summary>
        void Equip(bool offhand = false);
    }
}
