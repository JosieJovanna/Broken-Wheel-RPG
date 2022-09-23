using System;
using LorendisCore.Common.Stats.Attributes;

namespace LorendisCore.Common.Stats
{
    public enum Stat
    {
        /// <summary>
        /// An unspecified stat, which must be checked using string matching on the dynamic stat table.
        /// </summary>
        Custom,

        #region ATTRIBUTES
        [Attribute]
        Level,
        [Attribute]
        Hunger,
        [Attribute]
        Thirst,
        [Attribute]
        Exhaustion,

        #region vitals
        [Complex]
        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Health Points", "Represents hitpoints: one's vitality and physical condition, such as blood remaining or injuries received.")]
        HP,

        [Complex]
        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Stamina Points", "Represents one's energy available to perform physical actions, from movement to combat, and even some casting.")]
        SP,

        [Complex]
        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Willpower Points", "Represents one's mental capacity, whether that be for magic-casting, or simply more taxing physical combat.")]
        WP,

        [Attribute]
        [Category(StatCategory.Vitals)]
        [Info("Luck", "One's luck, a stat which is typically obscured.")]
        Luck,
        #endregion

        #region movement
        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Sprinting Attribute", "The ability to run at high speeds and for sustained lengths of time. Also affects probability to trip and wall-running.")]
        Sprint,

        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Leaping Attribute", "The ability to jump vertically and horizontally. Also affects wall-jumping.")]
        Leap,

        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Climbing Attribute", "The ability to scale surfaces quickly and safely. Also affects wall-running and wall-jumping.")]
        Climb,

        [Attribute]
        [Category(StatCategory.Movement)]
        [Info("Swimming Attribute", "The ability to move through liquids. Also affects how long breath is held.")]
        Swim,
        #endregion
        #endregion

        #region SKILLS
        #region defense
        [Skill]
        [Category(StatCategory.Defense)]
        [Info("Fortitude Skill", "The ability to take direct hits and survive. Required to effectively wear heavy armor. Reduces chance to be staggered.")]
        Fortitude,

        [Skill]
        [Category(StatCategory.Defense)]
        [Info("Evasion Skill", "The ability to avoid being hit. Affects probability of being hit, as well as being crit.")]
        Evasion,

        [Skill]
        [Category(StatCategory.Defense)]
        [Info("Blocking Skill", "The ability to actively block attacks, reducing damage and stagger. Blocking can be done with shields, and most weapons.")]
        Block,

        [Skill]
        [Category(StatCategory.Defense)]
        [Info("Parrying Skill", "The ability to actively parry attacks by 'blocking' at the appropriate time. Parrying can be done whenever blocks are, though ripostes are exclusive to certain weapons, and are exclusively parries.")]
        Parry,
        #endregion

        #region combat
        [Skill]
        [Category(StatCategory.Combat)]
        [Info("One-Handed Skill", "The ability to use melee weapons one-handed; melee proficiencies further increase damage. Required for using small arms.")]
        OneHanded,

        [Skill]
        [Category(StatCategory.Combat)]
        [Info("Two-Handed Skill", "The ability to use melee weapons two-handed; melee proficiencies further increase damage. Required for using polearms.")]
        TwoHanded,

        [Skill]
        [Category(StatCategory.Combat)]
        [Info("Ranged Weapon Skill", "The ability to aim ranged weapons; ranged proficiencies further increase damage.")]
        Ranged,

        [Skill]
        [Category(StatCategory.Combat)]
        [Info("Unarmed Combat Skill", "The ability to use one's own body in combat; unarmed proficiencies further increase damage.")]
        Unarmed,
        #endregion

        #region sorcery
        [Skill]
        [Category(StatCategory.Sorcery)]
        [Info("Evocation Skill", "The sorcery of the elements, used to harness the Primordials' power directly from the body. A craft as old as time itself.")]
        Evocation,

        [Skill]
        [Category(StatCategory.Sorcery)]
        [Info("Wild Magic Skill", "The sorcery of life and growth, capable even of transforming the user. A sacred Druidic art.")]
        WildMagic,

        [Skill]
        [Category(StatCategory.Sorcery)]
        [Info("Conjuration Skill", "The sorcery of the universe, used to bend space to one's will, or even bind souls to matter. A ritualistic practice handed down from the Moon Prince.")]
        Conjuration,

        [Skill]
        [Category(StatCategory.Sorcery)]
        [Info("Faith Skill", "The sorcery of belief, to which the universe innately responds. An inherent aspect of life which may be honed and directed by those with the talent.")]
        Faith,
        #endregion

        #region wizardry
        [Skill]
        [Category(StatCategory.Wizardry)]
        [Info("Transmutation Skill", "The wizardry of the material, used to transform the physical conditions around us at will. A science gifted to the world by Iru.")]
        Transmutation,

        [Skill]
        [Category(StatCategory.Wizardry)]
        [Info("Illusion Skill", "The wizardry of the mind, used to deceive and dazzle, or even to temporarily alter reality by invoking with the illusion belief. An Elvish art.")]
        Illusion,

        [Skill]
        [Category(StatCategory.Wizardry)]
        [Info("Necromancy Skill", "The wizardry of anatomy, used to mend, reanimate, or destroy bodies. While primarily a medicinal science, it is better known for the practice of reanimating corpses. The forbidden teachings of the First Lich.")]
        Necromancy,

        [Skill]
        [Category(StatCategory.Wizardry)]
        [Info("Psionic Skill", "The ultimate form of wizardry, used to alter reality on a fundamental level. This magic harnessing raw energy is only learned by the most accomplished scholars. The forefront of magic study.")]
        Psionics,
        #endregion

        #region crafting
        [Skill]
        [Category(StatCategory.Crafting)]
        [Info("Cooking Skill", "The ability to make food, often with many benefits. Practically essential for any survivalist. Closely related to alchemy.")]
        Cooking,

        [Skill]
        [Category(StatCategory.Crafting)]
        [Info("Alchemic Skill", "The ability to mix ingredients into effective potions.")]
        Alchemy,

        [Skill]
        [Category(StatCategory.Crafting)]
        [Info("Enchantment Skill", "The ability to harness raw ingredients' inherent magical traits, imbuing equipment with their effects. The physical craft of sorcerers, crucial to Dwarven magitech.")]
        Enchantment,

        [Skill]
        [Category(StatCategory.Crafting)]
        [Info("Inscription Skill", "The ability to etch spells into equipment, actively casting themselves continuously, and thus capable of many effects. The physical craft of wizards, used for making scrolls.")]
        Inscription,
        #endregion

        #region misc
        [Skill]
        [Category(StatCategory.Misc)]
        [Info("Perception Skill", "The degree to which one's physical senses are honed. Affects ease of finding details, hearing things, as well as accuracy in ranged attacks and the ability to zoom one's view. Can reveal physical actions of characters in dialogue.")]
        Perception,

        [Skill]
        [Category(StatCategory.Misc)]
        [Info("Intuition Skill", "The degree to which one's mind is honed. Affects the ability to get quest guidance, to 'see' into the future, and to some degree, natural learning ability. Can reveal characters' intentions or possible reactions in dialog.")]
        Intuition,

        [Skill]
        [Category(StatCategory.Misc)]
        [Info("Charisma Skill", "The degree to which one's social skills are honed. Affects your reputation and appearance to others, and your ability to lie or persuade in dialog. May reveal new dialog options.")]
        Charisma,

        [Skill]
        [Category(StatCategory.Misc)]
        [Info("Deftness Skill", "The degree to which one's skills with their hands are honed. Affects the ability to lockpick, pickpocket, and to get crits.")]
        Deftness,
        #endregion
        #endregion

        #region PROFICIENCIES
        #region melee
        // MELEE
        [Proficiency]
        [Category(StatCategory.Melee)]
        [Info("Swordsmanship Skill", "One's handiness with a sword. One or two hands.")]
        Sword,

        [Proficiency]
        [Category(StatCategory.Melee)]
        [Info("Axe-Handling Skill", "One's ability to effectively use axes. One or two hands.")]
        Axe,

        [Proficiency]
        [Category(StatCategory.Melee)]
        [Info("Bludgeoning Skill", "One's effectiveness with blunt weapons, such as clubs and maces. One or two hands.")]
        Blunt,

        [Proficiency]
        [Category(StatCategory.Melee)]
        [Info("Flail Weapon Skill", "One's ability to weild swinging weapons, such as flails, whips, ball-and-chains. Flailing attacks can often be charged. One or two hands.")]
        Flail,

        [Proficiency]
        [Category(StatCategory.Melee)]
        [Info("Small-Arm Skill", "One's handiness with small arms, such as daggers or knives. One hand only.")]
        SmallArm,

        [Proficiency]
        [Category(StatCategory.Melee)]
        [Info("Polearm Skill", "One's handiness with polearms, such as staves or spears. Two hands only.")]
        Polearm,
        #endregion

        #region ranged
        [Proficiency]
        [Category(StatCategory.Ranged)]
        [Info("Throwing Skill", "One's ability to throw projectiles, such as throwing knives or caltrops.")]
        Throwing,

        [Proficiency]
        [Category(StatCategory.Ranged)]
        [Info("Shortbow Shooting Skill", "One's ability to weild a short bow.")]
        ShortBow,

        [Proficiency]
        [Category(StatCategory.Ranged)]
        [Info("Longbow Shooting Skill", "One's ability to weild a long bow.")]
        LongBow,

        [Proficiency]
        [Category(StatCategory.Ranged)]
        [Info("Triggered Weapon Skill", "One's ability to use triggered weapons, such as crossbows or guns.")]
        Trigger,
        #endregion

        #region unarmed
        [Proficiency]
        [Category(StatCategory.Unarmed)]
        [Info("Punching Skill", "The ability to use one's hands in combat, including knuckle dusters.")]
        Punch,

        [Proficiency]
        [Category(StatCategory.Unarmed)]
        [Info("Kicking Skill", "The ability to use one's legs in combat.")]
        Kick,

        [Proficiency]
        [Category(StatCategory.Unarmed)]
        [Info("Grappling Skill", "The ability to grab onto people or things.")]
        Grab,

        [Proficiency]
        [Category(StatCategory.Unarmed)]
        [Info("Qi Skill", "The ability to channel one's life energy into attacks; a martial art created by Oru's monks.")]
        Qi,
        #endregion

        #region casting
        [Proficiency]
        [Category(StatCategory.Casting)]
        [Info("Self-Casting Skill", "One's ability to cast sorcery or wizardry spells on themself.")]
        Self,

        [Proficiency]
        [Category(StatCategory.Casting)]
        [Info("Touch-Casting Skill", "One's ability to cast sorcery or wizardry spells by touching things. Also used when placing static spells.")]
        Touch,

        [Proficiency]
        [Category(StatCategory.Casting)]
        [Info("Projectile-Casting Skill", "One's ability to cast sorcery or wizardry spells at range.")]
        Projectile,

        [Proficiency]
        [Category(StatCategory.Casting)]
        [Info("Area of Effect Skill", "One's ability to cast sorcery or wizardry spells over wide areas.")]
        AreaOfEffect,
        #endregion
        #endregion
    }
}
