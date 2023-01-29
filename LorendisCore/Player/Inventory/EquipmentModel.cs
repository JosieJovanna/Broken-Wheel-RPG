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
        private OneHandedImplement _mainHand;
        private OneHandedImplement _offHand;

        /// <summary>
        /// Whether a two-handed weapon is equipped. When that's the case, singular hands will be null.
        /// </summary>
        public bool IsTwoHanded => TwoHand != null;
        
        /// <summary>
        /// The dominant hand, whether that be left or right. Returns null if there is a two-handed implement equipped.
        /// </summary>
        public OneHandedImplement MainHand
        {
            get => TwoHand != null ? null : _mainHand;
            set => _mainHand = value;
        }

        /// <summary>
        /// The non-dominant hand, whether that be left or right. Returns null if there is a two-handed implement equipped.
        /// </summary>
        public OneHandedImplement OffHand
        {
            get => TwoHand != null ? null : _offHand;
            set => _offHand = value;
        }
        
        public TwoHandedImplement TwoHand;
        
        // kicks, armor, clothes, etc....
    }
}