using LorendisCore.Equipment.Implements;

namespace LorendisCore.Player.Control
{
    /// <summary>
    /// The object which tracks controls for weapons, actions etc. that are currently available to the player.
    /// This is separate from stuff like the inventory and belt (hotbar), which are what handle the jobs of equipping,
    /// unequipping, donning and doffing, along with restraints like one-handed or two-handed not being both equippable.
    /// Should be populated by the equipment tracker which holds data for the actual concrete items.
    /// It is a POCO, and should not do anything with its data.
    /// </summary>
    public class EquipmentControl
    {
        public bool WeaponsDrawn;
        
        /// <summary>
        /// The dominant hand, whether that be left or right.
        /// </summary>
        public IImplement MainHand;

        /// <summary>
        /// The non-dominant hand, whether that be left or right.
        /// </summary>
        public IImplement OffHand;

        // TODO: kicks, armor, clothes, etc....
    }
}