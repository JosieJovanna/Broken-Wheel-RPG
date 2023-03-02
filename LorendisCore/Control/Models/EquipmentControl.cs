using LorendisCore.Control.Implements;
using LorendisCore.Equipment;

namespace LorendisCore.Control.Models
{
    /// <summary>
    /// The object which tracks the objects allowing for the player to control
    /// <see cref="IBaseControl">Implements</see>, special abilities, et cetera.
    /// This does not handle equipping or unequipping, or any similar function; it is a POCO.
    /// Neither does it determine whether or not actions can be executed, such as if weapons are raised.
    /// </summary>
    public class EquipmentControl
    {
        /// <summary>
        /// The object in charge of tracking what items are equipped.
        /// </summary>
        public IEquipment Equipment;
        
        /// <summary>
        /// The dominant hand, whether that be left or right.
        /// </summary>
        public IBaseControl MainHand;

        /// <summary>
        /// The non-dominant hand, whether that be left or right.
        /// </summary>
        public IBaseControl OffHand;

        /// <summary>
        /// The kick action, which may be either foot.
        /// </summary>
        public IBaseControl Kick;

        /// <summary>
        /// The special ability currently equipped.
        /// </summary>
        public IBaseControl Ability;
    }
}