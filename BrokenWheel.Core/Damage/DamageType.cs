using System;
using System.Collections.Generic;
using System.Linq;

namespace BrokenWheel.Core.Damage
{
    public static class DamageTypeExtensions
    {
        public static string GetName(this DamageType type)
        {
            return Enum.GetName(typeof(DamageType), type);
        }

        public static List<DamageType> GetAll()
        {
            return Enum.GetValues(typeof(DamageType))
                .Cast<DamageType>()
                .ToList();
        }
    }

    public enum DamageType
    {
        /****   BASIC   ****/
        /// <summary>  Normal HP damage.  </summary>
        Generic,
        /// <summary>  Normal SP damage.  </summary>
        Stamina,
        /// <summary>  Normal WP damage.  </summary>
        Willpower,

        /****  PHYSICAL  ****/
        /// <summary>  Damage from bludgeoning weapons; good against dense enemies.  </summary>
        Blunt,
        /// <summary>  Damage from blades. Most common form.  </summary>
        Slash,
        /// <summary>  Damage from thrusts and various ammunitions; cuts through armor.  </summary>
        Pierce,
        /// <summary>  Works towards stunning and knocking back.  </summary>
        Stun,
        /// <summary>  Damage left after wounds, often lingering.  </summary>
        Bleed,
        /// <summary>  XXXXXX  </summary>
        Explosive,

        /**** ELEMENTAL ****/
        /// <summary>  Holy sun-related damage.  </summary>
        Radiant,
        /// <summary>  Immediate natural damage.  </summary>
        Venom,
        /// <summary>  Causes burns.  </summary>
        Fire,
        /// <summary>  Causes corrosion.  </summary>
        Corrosion,
        /// <summary>  Causes electrification. </summary>
        Shock,
        /// <summary>  Causes freezing. </summary>
        Frost,

        /****  MAGICAL  ****/
        /// <summary>  XXXXXX  </summary>
        Necrotic,
        /// <summary>  Vague damage dealt by Wizardry, Sorcery, and Qi.  </summary>
        Magic,
        /// <summary>  XXXXXX  </summary>
        Psychic
    }
}
