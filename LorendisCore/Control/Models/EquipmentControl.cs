using LorendisCore.Control.Implements;
using LorendisCore.Equipment.Implements;

namespace LorendisCore.Control.Models
{
    /// <summary>
    /// The object which tracks the objects allowing for the player to control <see cref="IImplementCtrl">Implements</see>,
    /// special abilities, et cetera.
    /// This does not handle equipping or unequipping, or any similar function; it is a POCO.
    /// Neither does it determine whether or not actions can be executed, such as if weapons are raised.
    /// </summary>
    public class EquipmentControl
    {
        /// <summary>
        /// The dominant hand, whether that be left or right.
        /// </summary>
        public IImplementCtrl MainHand;

        /// <summary>
        /// The non-dominant hand, whether that be left or right.
        /// </summary>
        public IImplementCtrl OffHand;

        /// <summary>
        /// The kick action, which may be either foot.
        /// </summary>
        public IImplementCtrl Kick;

        /// <summary>
        /// The special ability currently equipped.
        /// </summary>
        public IImplementCtrl Ability;
    }
}