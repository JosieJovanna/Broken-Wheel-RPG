using LorendisCore.Equipment.Implements;

namespace LorendisCore.Player.Inventory
{
    /// <summary>
    /// The object which tracks which weapons, armor, etc. are currently used by the player.
    /// This is separate from stuff like the inventory and belt (hotbar), which are what handle the jobs of equipping,
    /// unequipping, donning and doffing, along with restraints like one-handed or two-handed not being both equippable.
    /// It is a POCO, and should not do anything with its data.
    /// </summary>
    public class EquipmentModel
    {
        /// <summary>
        /// Whether a two-handed weapon is equipped.
        /// </summary>
        public bool IsTwoHanded => TwoHand != null;

        /// <summary>
        /// The dominant hand, whether that be left or right.
        /// </summary>
        public OneHandedImplement MainHand;

        /// <summary>
        /// The non-dominant hand, whether that be left or right.
        /// </summary>
        public OneHandedImplement OffHand;
        
        /// <summary>
        /// A weapon held in both hands. 
        /// </summary>
        public TwoHandedImplement TwoHand;
        
        // TODO: kicks, armor, clothes, etc....
    }
}