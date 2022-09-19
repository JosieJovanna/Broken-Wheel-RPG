using System;
using LorendisCore.Common.Stats.Attributes;

namespace LorendisCore.Common.Stats
{
    public enum Stat
    {
        /// <summary>
        /// An unspecified stat, which must be checked using string matching on the dynamic stat table.
        /// </summary>
        Unspecified,

        #region ATTRIBUTES
        // VITALS
        [Complex]
        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Health", "Represents hitpoints: one's vitality and physical condition, such as blood remaining or injuries received.")]
        HP,

        [Complex]
        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Stamina", "Represents one's energy available to perform physical actions, from movement to combat, and even some casting.")]
        SP,

        [Complex]
        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Willpower", "Represents one's mental capacity, whether that be for magic-casting, or simply more taxing physical combat.")]
        WP,

        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Luck", "One's luck, a stat which is typically obscured.")]
        Luck,

        // MOVEMENT
        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Sprinting", "The ability to run at high speeds and for sustained lengths of time. Also affects probability to trip and wall-running.")]
        Sprint,

        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Leaping", "The ability to jump vertically and horizontally. Also affects wall-jumping.")]
        Leap,

        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Climbing", "The ability to scale surfaces quickly and safely. Also affects wall-running and wall-jumping.")]
        Climb,

        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Swimming", "The ability to move through liquids. Also affects how long breath is held.")]
        Swim,
        #endregion

        #region SKILLS
        // DEFENSE
        Fortitude,
        Evasion,
        Block,
        Parry,

        // COMBAT
        OneHanded,
        TwoHanded,
        Ranged,
        Unarmed,

        // SORCERY
        Evocation,
        WildMagic,
        Conjuration,
        Faith,

        // WIZARDRY
        Transmutation,
        Illusion,
        Necromancy,
        Psionics,

        // CRAFTING
        Cooking,
        Alchemy,
        Enchantment,
        Inscription,

        // MISC
        Perception,
        Intuition,
        Charisma,
        Deftness,
        #endregion

        #region PROFICIENCIES
        // MELEE
        Sword,
        Axe,
        Blunt,
        SmallArm,
        Polearm,
        Flail,

        // RANGED
        Throwing,
        ShortBow,
        LongBow,
        TriggerWeapon,

        // UNARMED
        Punch,
        Kick,
        Grapple,
        Qi,

        // CASTING
        Self,
        Touch,
        Projectile,
        AreaOfEffect,
        #endregion
    }
}
