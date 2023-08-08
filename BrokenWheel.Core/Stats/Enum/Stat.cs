using BrokenWheel.Core.Stats.Attributes;

namespace BrokenWheel.Core.Stats.Enum
{
    public enum Stat
    {
        /// <summary>
        /// An unspecified stat, which must be checked using string matching on the dynamic stat table.
        /// </summary>
        Custom = default,

        #region ATTRIBUTES
        [StatCategories(StatType.Attribute, StatCategory.Survival)]
        Level,
        
        [StatCategories(StatType.Attribute, StatCategory.Survival)]
        Hunger,
        
        [StatCategories(StatType.Attribute, StatCategory.Survival)]
        Thirst,
        
        [StatCategories(StatType.Attribute, StatCategory.Survival)]
        Exhaustion,

        #region vitals
        [ComplexStat]
        [StatCategories(StatType.Attribute, StatCategory.Vitals)]
        [Info("Health Points", "Represents hitpoints: one's vitality and physical condition, such as blood remaining or injuries received.")]
        HP,

        [ComplexStat]
        [StatCategories(StatType.Attribute, StatCategory.Vitals)]
        [Info("Stamina Points", "Represents one's energy available to perform physical actions, from movement to combat, and even some casting.")]
        SP,

        [ComplexStat]
        [StatCategories(StatType.Attribute, StatCategory.Vitals)]
        [Info("Willpower Points", "Represents one's mental capacity, whether that be for magic-casting, or simply more taxing physical combat.")]
        WP,

        [StatCategories(StatType.Attribute, StatCategory.Vitals)]
        [Info("Luck", "One's luck, a stat which is typically obscured.")]
        Luck,
        #endregion

        #region movement
        [StatCategories(StatType.Attribute, StatCategory.Movement)]
        [Info("Sprinting Attribute", "The ability to run at high speeds and for sustained lengths of time. Also affects probability to trip and wall-running.")]
        Sprint,

        [StatCategories(StatType.Attribute, StatCategory.Movement)]
        [Info("Leaping Attribute", "The ability to jump vertically and horizontally. Also affects wall-jumping.")]
        Leap,

        [StatCategories(StatType.Attribute, StatCategory.Movement)]
        [Info("Climbing Attribute", "The ability to scale surfaces quickly and safely. Also affects wall-running and wall-jumping.")]
        Climb,

        [StatCategories(StatType.Attribute, StatCategory.Movement)]
        [Info("Swimming Attribute", "The ability to move through liquids. Also affects how long breath is held.")]
        Swim,
        #endregion
        #endregion

        #region SKILLS
        #region defense
        [StatCategories(StatType.Skill, StatCategory.Defense)]
        [Info("Fortitude Skill", "The ability to take direct hits and survive. Required to effectively wear heavy armor. Reduces chance to be staggered.")]
        Fortitude,

        [StatCategories(StatType.Skill, StatCategory.Defense)]
        [Info("Evasion Skill", "The ability to avoid being hit. Affects probability of being hit, as well as being crit.")]
        Evasion,

        [StatCategories(StatType.Skill, StatCategory.Defense)]
        [Info("Blocking Skill", "The ability to actively block attacks, reducing damage and stagger. Blocking can be done with shields, and most weapons.")]
        Block,

        [StatCategories(StatType.Skill, StatCategory.Defense)]
        [Info("Parrying Skill", "The ability to actively parry attacks by 'blocking' at the appropriate time. Parrying can be done whenever blocks are, though ripostes are exclusive to certain weapons, and are exclusively parries.")]
        Parry,
        #endregion

        #region combat
        [StatCategories(StatType.Skill, StatCategory.Combat)]
        [Info("One-Handed Skill", "The ability to use melee weapons one-handed; melee proficiencies further increase damage. Required for using small arms.")]
        OneHanded,

        [StatCategories(StatType.Skill, StatCategory.Combat)]
        [Info("Two-Handed Skill", "The ability to use melee weapons two-handed; melee proficiencies further increase damage. Required for using polearms.")]
        TwoHanded,

        [StatCategories(StatType.Skill, StatCategory.Combat)]
        [Info("Ranged Weapon Skill", "The ability to aim ranged weapons; ranged proficiencies further increase damage.")]
        Ranged,

        [StatCategories(StatType.Skill, StatCategory.Combat)]
        [Info("Unarmed Combat Skill", "The ability to use one's own body in combat; unarmed proficiencies further increase damage.")]
        Unarmed,
        #endregion

        #region sorcery
        [StatCategories(StatType.Skill, StatCategory.Sorcery)]
        [Info("Evocation Skill", "The sorcery of the elements, used to harness the Primordials' power directly from the body. A craft as old as time itself.")]
        Evocation,

        [StatCategories(StatType.Skill, StatCategory.Sorcery)]
        [Info("Wild Magic Skill", "The sorcery of life and growth, capable even of transforming the user. A sacred Druidic art.")]
        WildMagic,

        [StatCategories(StatType.Skill, StatCategory.Sorcery)]
        [Info("Conjuration Skill", "The sorcery of the universe, used to bend space to one's will, or even bind souls to matter. A ritualistic practice handed down from the Moon Prince.")]
        Conjuration,

        [StatCategories(StatType.Skill, StatCategory.Sorcery)]
        [Info("Faith Skill", "The sorcery of belief, to which the universe innately responds. An inherent aspect of life which may be honed and directed by those with the talent.")]
        Faith,
        #endregion

        #region wizardry
        [StatCategories(StatType.Skill, StatCategory.Wizardry)]
        [Info("Transmutation Skill", "The wizardry of the material, used to transform the physical conditions around us at will. A science gifted to the world by Iru.")]
        Transmutation,

        [StatCategories(StatType.Skill, StatCategory.Wizardry)]
        [Info("Illusion Skill", "The wizardry of the mind, used to deceive and dazzle, or even to temporarily alter reality by invoking with the illusion belief. An Elvish art.")]
        Illusion,

        [StatCategories(StatType.Skill, StatCategory.Wizardry)]
        [Info("Necromancy Skill", "The wizardry of anatomy, used to mend, reanimate, or destroy bodies. While primarily a medicinal science, it is better known for the practice of reanimating corpses. The forbidden teachings of the First Lich.")]
        Necromancy,

        [StatCategories(StatType.Skill, StatCategory.Wizardry)]
        [Info("Psionic Skill", "The ultimate form of wizardry, used to alter reality on a fundamental level. This magic harnessing raw energy is only learned by the most accomplished scholars. The forefront of magic study.")]
        Psionics,
        #endregion

        #region crafting
        [StatCategories(StatType.Skill, StatCategory.Crafting)]
        [Info("Cooking Skill", "The ability to make food, often with many benefits. Practically essential for any survivalist. Closely related to alchemy.")]
        Cooking,

        [StatCategories(StatType.Skill, StatCategory.Crafting)]
        [Info("Alchemic Skill", "The ability to mix ingredients into effective potions.")]
        Alchemy,

        [StatCategories(StatType.Skill, StatCategory.Crafting)]
        [Info("Enchantment Skill", "The ability to harness raw ingredients' inherent magical traits, imbuing equipment with their effects. The physical craft of sorcerers, crucial to Dwarven magitech.")]
        Enchantment,

        [StatCategories(StatType.Skill, StatCategory.Crafting)]
        [Info("Inscription Skill", "The ability to etch spells into equipment, actively casting themselves continuously, and thus capable of many effects. The physical craft of wizards, used for making scrolls.")]
        Inscription,
        #endregion

        #region misc
        [StatCategories(StatType.Skill, StatCategory.Misc)]
        [Info("Perception Skill", "The degree to which one's physical senses are honed. Affects ease of finding details, hearing things, as well as accuracy in ranged attacks and the ability to zoom one's view. Can reveal physical actions of characters in dialogue.")]
        Perception,

        [StatCategories(StatType.Skill, StatCategory.Misc)]
        [Info("Intuition Skill", "The degree to which one's mind is honed. Affects the ability to get quest guidance, to 'see' into the future, and to some degree, natural learning ability. Can reveal characters' intentions or possible reactions in dialog.")]
        Intuition,

        [StatCategories(StatType.Skill, StatCategory.Misc)]
        [Info("Charisma Skill", "The degree to which one's social skills are honed. Affects your reputation and appearance to others, and your ability to lie or persuade in dialog. May reveal new dialog options.")]
        Charisma,

        [StatCategories(StatType.Skill, StatCategory.Misc)]
        [Info("Deftness Skill", "The degree to which one's skills with their hands are honed. Affects the ability to lockpick, pickpocket, and to get crits.")]
        Deftness,
        #endregion
        #endregion

        #region PROFICIENCIES
        #region melee
        // MELEE
        [StatCategories(StatType.Proficiency, StatCategory.Melee)]
        [Info("Swordsmanship Skill", "One's handiness with a sword. One or two hands.")]
        Sword,

        [StatCategories(StatType.Proficiency, StatCategory.Melee)]
        [Info("Axe-Handling Skill", "One's ability to effectively use axes. One or two hands.")]
        Axe,

        [StatCategories(StatType.Proficiency, StatCategory.Melee)]
        [Info("Bludgeoning Skill", "One's effectiveness with blunt weapons, such as clubs and maces. One or two hands.")]
        Blunt,

        [StatCategories(StatType.Proficiency, StatCategory.Melee)]
        [Info("Flail Weapon Skill", "One's ability to weild swinging weapons, such as flails, whips, ball-and-chains. Flailing attacks can often be charged. One or two hands.")]
        Flail,

        [StatCategories(StatType.Proficiency, StatCategory.Melee)]
        [Info("Small-Arm Skill", "One's handiness with small arms, such as daggers or knives. One hand only.")]
        SmallArm,

        [StatCategories(StatType.Proficiency, StatCategory.Melee)]
        [Info("Polearm Skill", "One's handiness with polearms, such as staves or spears. Two hands only.")]
        PoleArm,
        #endregion

        #region ranged
        [StatCategories(StatType.Proficiency, StatCategory.Ranged)]
        [Info("Throwing Skill", "One's ability to throw projectiles, such as throwing knives or caltrops.")]
        Throwing,

        [StatCategories(StatType.Proficiency, StatCategory.Ranged)]
        [Info("Shortbow Shooting Skill", "One's ability to weild a short bow.")]
        ShortBow,

        [StatCategories(StatType.Proficiency, StatCategory.Ranged)]
        [Info("Longbow Shooting Skill", "One's ability to weild a long bow.")]
        LongBow,

        [StatCategories(StatType.Proficiency, StatCategory.Ranged)]
        [Info("Triggered Weapon Skill", "One's ability to use triggered weapons, such as crossbows or guns.")]
        Trigger,
        #endregion

        #region unarmed
        [StatCategories(StatType.Proficiency, StatCategory.Unarmed)]
        [Info("Punching Skill", "The ability to use one's hands in combat, including knuckle dusters.")]
        Punch,

        [StatCategories(StatType.Proficiency, StatCategory.Unarmed)]
        [Info("Kicking Skill", "The ability to use one's legs in combat.")]
        Kick,

        [StatCategories(StatType.Proficiency, StatCategory.Unarmed)]
        [Info("Grappling Skill", "The ability to grab onto people or things.")]
        Grab,

        [StatCategories(StatType.Proficiency, StatCategory.Unarmed)]
        [Info("Qi Skill", "The ability to channel one's life energy into attacks; a martial art created by Oru's monks.")]
        Qi,
        #endregion

        #region casting
        [StatCategories(StatType.Proficiency, StatCategory.Casting)]
        [Info("Self-Casting Skill", "One's ability to cast sorcery or wizardry spells on themself.")]
        Self,

        [StatCategories(StatType.Proficiency, StatCategory.Casting)]
        [Info("Touch-Casting Skill", "One's ability to cast sorcery or wizardry spells by touching things. Also used when placing static spells.")]
        Touch,

        [StatCategories(StatType.Proficiency, StatCategory.Casting)]
        [Info("Projectile-Casting Skill", "One's ability to cast sorcery or wizardry spells at range.")]
        Projectile,

        [StatCategories(StatType.Proficiency, StatCategory.Casting)]
        [Info("Area of Effect Skill", "One's ability to cast sorcery or wizardry spells over wide areas.")]
        AreaOfEffect,
        #endregion
        #endregion
    }
}
