using LorendisCore.Control.Implements;

namespace LorendisCore.Control.Models
{
    /// <summary>
    /// The object which tracks the objects allowing for the player to control
    /// <see cref="IImplementControl">Implements</see>, special abilities, et cetera.
    /// This does not handle equipping or unequipping, or any similar function; it is a POCO.
    /// Neither does it determine whether or not actions can be executed, such as if weapons are raised.
    /// </summary>
    public class EquipmentControl
    {
        /// <summary>
        /// The dominant hand, whether that be left or right.
        /// </summary>
        public IImplementControl MainHand;

        /// <summary>
        /// The non-dominant hand, whether that be left or right.
        /// </summary>
        public IImplementControl OffHand;

        /// <summary>
        /// The kick action, which may be either foot.
        /// </summary>
        public IImplementControl Kick;

        /// <summary>
        /// The special ability currently equipped.
        /// </summary>
        public IImplementControl Ability;
    }
}