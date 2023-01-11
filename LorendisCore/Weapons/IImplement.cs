using LorendisCore.Player.Control.Actions;

namespace LorendisCore.Weapons
{
    /// <summary>
    /// A piece of equipment which may be held in the hand, modifying behaviors along with it.
    /// </summary>
    public interface IImplement
    {
        /// <summary>
        /// The behaviors overridden by this implement when initially equipped.
        /// </summary>
        ActionBehaviorMap Behaviors { get; set; }

        /// <summary>
        /// Equips the implement and changes appropriate behaviors. This implementation is handled 
        /// </summary>
        void Equip();
    }
}
