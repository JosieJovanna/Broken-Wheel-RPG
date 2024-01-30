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

        #region meta

        [StatInfo(StatType.Attribute, StatCategory.Meta,
            Code = "LVL",
            Name = "Level",
            Description = "Abstraction of one's overall strength and experience as an adventurer.")]
        Level,

        [StatInfo(StatType.Attribute, StatCategory.Meta,
            Code = "EXP",
            Name = "Experience",
            Description = "Progress towards reaching the next level." )]
        Experience,

        [StatInfo(StatType.Attribute, StatCategory.Meta,
            Code = "LUCK",
            Name = "Luck",
            Description = "The amount to which the world bends to one's will; used in many subtle calculations.")]
        Luck,

        #endregion // meta

        #region vitals

        [StatInfo(StatType.Attribute, StatCategory.Vitals,
            Code = "HP",
            Name = "Health",
            Description = "The abstract measure of one's vitals, including blood, organ functionality, et cetera. Reaching 0 results in death, unless fortunate.",
            IsComplex = true)]
        HP,

        [StatInfo(StatType.Attribute, StatCategory.Vitals,
            Code = "SP",
            Name = "Stamina",
            Description = "The abstract measure of one's physical capacity to perform actions. Reaching maximum exhaustion results in unconsciousness, often leading to death.",
            IsComplex = true)]
        SP,

        [StatInfo(StatType.Attribute, StatCategory.Vitals,
            Code = "WP",
            Name = "Willpower",
            Description = "The abstract measure of one's willpower, which affects not only their capacity for abilities, but also how much energy is available for magic.",
            IsComplex = true)]
        WP,

        #endregion // vitals

        #region meta

        [StatInfo(StatType.Attribute, StatCategory.Survival,
            Code = "DRNK",
            Name = "Hydration",
            Description = "The amount of water in one's system. Reaching 0 results in health exhaustion")]
        Hydration,
        
        [StatInfo(StatType.Attribute, StatCategory.Survival,
            Code = "FOOD",
            Name = "Satiation",
            Description = "The amount of food in one's stomach. Reaching 0 results in stamina exhaustion")]
        Satiation,

        [StatInfo(StatType.Attribute, StatCategory.Survival,
            Code = "REST",
            Name = "Rest",
            Description = "The amount of rest one has gotten. Reaching 0 results in willpower exhaustion")]
        Rest,

        #endregion // survival

        #region movement

        [StatInfo(StatType.Attribute, StatCategory.Movement,
            Code = "SPRN",
            Name = "Sprinting",
            Description = "The ability to run at high speeds and for sustained lengths of time. Also affects probability to trip and wall-running.")]
        Sprint,

        [StatInfo(StatType.Attribute, StatCategory.Movement,
            Code = "JUMP",
            Name = "Leaping",
            Description = "The ability to jump vertically and horizontally. Also affects wall-jumping.")]
        Leap,

        [StatInfo(StatType.Attribute, StatCategory.Movement,
            Code = "CLMB",
            Name = "Climbing",
            Description = "The ability to scale surfaces quickly and safely. Also affects wall-running and wall-jumping.")]
        Climb,

        [StatInfo(StatType.Attribute, StatCategory.Movement,
            Code = "SWIM",
            Name = "Swimming",
            Description = "The ability to move through liquids. Also affects how long breath is held.")]
        Swim,

        #endregion // movement

        #endregion // ATTRIBUTES

        #region SKILLS

        #region defense

        [StatInfo(StatType.Skill, StatCategory.Defense,
            Code = "FORT",
            Name = "Fortitude",
            Description = "The ability to take direct hits and survive. Required to effectively wear heavy armor. Reduces chance to be staggered.")]
        Fortitude,

        [StatInfo(StatType.Skill, StatCategory.Defense,
            Code = "EVAD",
            Name = "Evasion",
            Description = "The ability to avoid being hit. Affects probability of being hit, as well as being crit.")]
        Evasion,

        [StatInfo(StatType.Skill, StatCategory.Defense,
            Code = "BLCK",
            Name = "Block",
            Description = "The ability to actively block attacks, reducing damage and stagger. Blocking can be done with shields, and most weapons.")]
        Block,

        [StatInfo(StatType.Skill, StatCategory.Defense,
            Code = "PRRY",
            Name = "Parry",
            Description = "The ability to actively parry attacks by 'blocking' at the appropriate time. Parrying can be done whenever blocks are, though ripostes are exclusive to certain weapons, and are exclusively parries.")]
        Parry,

        #endregion // defense

        #region combat

        [StatInfo(StatType.Skill, StatCategory.Combat,
            Code = "1H",
            Name = "One-Handed",
            Description = "The ability to use melee weapons one-handed; melee proficiencies further increase damage. Required for using small arms.")]
        OneHanded,

        [StatInfo(StatType.Skill, StatCategory.Combat,
            Code = "2H",
            Name = "Two-Handed",
            Description = "The ability to use melee weapons two-handed; melee proficiencies further increase damage. Required for using polearms.")]
        TwoHanded,

        [StatInfo(StatType.Skill, StatCategory.Combat,
            Code = "RNGD",
            Name = "Ranged",
            Description = "The ability to aim ranged weapons; ranged proficiencies further increase damage.")]
        Ranged,

        [StatInfo(StatType.Skill, StatCategory.Combat,
            Code = "UNRM",
            Name = "Unarmed",
            Description = "The ability to use one's own body in combat; unarmed proficiencies further increase damage.")]
        Unarmed,

        #endregion // combat

        #region sorcery

        [StatInfo(StatType.Skill, StatCategory.Sorcery,
            Code = "EVOC",
            Name = "Evocation",
            Description = "The sorcery of the elements, used to harness the Primordials' power directly from the body. A craft as old as time itself.")]
        Evocation,

        [StatInfo(StatType.Skill, StatCategory.Sorcery,
            Code = "WILD",
            Name = "Wild Magic",
            Description = "The sorcery of life and growth, capable even of transforming the user. A sacred Druidic art.")]
        WildMagic,

        [StatInfo(StatType.Skill, StatCategory.Sorcery,
            Code = "CONJ",
            Name = "Conjuration",
            Description = "The sorcery of the universe, used to bend space to one's will, or even bind souls to matter. A ritualistic practice handed down from the Moon Prince.")]
        Conjuration,

        [StatInfo(StatType.Skill, StatCategory.Sorcery,
            Code = "FAI",
            Name = "Faith",
            Description = "The sorcery of belief, to which the universe innately responds. An inherent aspect of life which may be honed and directed by those with the talent.")]
        Faith,

        #endregion // sorcery

        #region wizardry

        [StatInfo(StatType.Skill, StatCategory.Wizardry,
            Code = "TRNS",
            Name = "Transmutation",
            Description = "The wizardry of the material, used to transform the physical conditions around us at will. A science gifted to the world by Iru.")]
        Transmutation,

        [StatInfo(StatType.Skill, StatCategory.Wizardry,
            Code = "ILLU",
            Name = "Illusion",
            Description = "The wizardry of the mind, used to deceive and dazzle, or even to temporarily alter reality by invoking with the illusion belief. An Elvish art.")]
        Illusion,

        [StatInfo(StatType.Skill, StatCategory.Wizardry,
            Code = "NCRO",
            Name = "Necromancy",
            Description = "The wizardry of anatomy, used to mend, reanimate, or destroy bodies. While primarily a medicinal science, it is better known for the practice of reanimating corpses. The forbidden teachings of the First Lich.")]
        Necromancy,

        [StatInfo(StatType.Skill, StatCategory.Wizardry,
            Code = "PSI",
            Name = "Psionics",
            Description = "The ultimate form of wizardry, used to alter reality on a fundamental level. This magic harnessing raw energy is only learned by the most accomplished scholars. The forefront of magic study.")]
        Psionics,

        #endregion // wizardry

        #region crafting

        [StatInfo(StatType.Skill, StatCategory.Crafting,
            Code = "COOK",
            Name = "Cooking",
            Description = "The ability to make food, often with many benefits. Practically essential for any survivalist. Closely related to alchemy.")]
        Cooking,

        [StatInfo(StatType.Skill, StatCategory.Crafting,
            Code = "ALCH",
            Name = "Alchemy",
            Description = "The ability to mix ingredients into effective potions.")]
        Alchemy,

        [StatInfo(StatType.Skill, StatCategory.Crafting,
            Code = "ENCH",
            Name = "Enchantment",
            Description = "The ability to harness raw ingredients' inherent magical traits, imbuing equipment with their effects. The physical craft of sorcerers, crucial to Dwarven magitech.")]
        Enchantment,

        [StatInfo(StatType.Skill, StatCategory.Crafting,
            Code = "INSC",
            Name = "Inscription",
            Description = "The ability to etch spells into equipment, actively casting themselves continuously, and thus capable of many effects. The physical craft of wizards, used for making scrolls.")]
        Inscription,

        #endregion // crafting

        #region misc

        [StatInfo(StatType.Skill, StatCategory.Miscellaneous,
            Code = "PER",
            Name = "Perception",
            Description = "The degree to which one's physical senses are honed. Affects ease of finding details, hearing things, as well as accuracy in ranged attacks and the ability to zoom one's view. Can reveal physical actions of characters in dialogue.")]
        Perception,

        [StatInfo(StatType.Skill, StatCategory.Miscellaneous,
            Code = "INT",
            Name = "Intuition",
            Description = "The degree to which one's mind is honed. Affects the ability to get quest guidance, to 'see' into the future, and to some degree, natural learning ability. Can reveal characters' intentions or possible reactions in dialog.")]
        Intuition,

        [StatInfo(StatType.Skill, StatCategory.Miscellaneous,
            Code = "CHA",
            Name = "Charisma",
            Description = "The degree to which one's social skills are honed. Affects your reputation and appearance to others, and your ability to lie or persuade in dialog. May reveal new dialog options.")]
        Charisma,

        [StatInfo(StatType.Skill, StatCategory.Miscellaneous,
            Code = "DEFT",
            Name = "Deft-Handedness",
            Description = "The degree to which one's skills with their hands are honed. Affects the ability to lockpick, pickpocket, and to get crits.")]
        Deftness,

        #endregion // misc

        #endregion // SKILLS

        #region PROFICIENCIES

        #region melee

        [StatInfo(StatType.Skill, StatCategory.Melee,
            Code = "SWRD",
            Name = "Swords",
            Description = "One's handiness with a sword. One or two hands.")]
        Sword,

        [StatInfo(StatType.Skill, StatCategory.Melee,
            Code = "AXE",
            Name = "Axes",
            Description = "One's ability to effectively use axes. One or two hands.")]
        Axe,

        [StatInfo(StatType.Skill, StatCategory.Melee,
            Code = "BASH",
            Name = "Blunt Weaponry",
            Description = "One's effectiveness with blunt weapons, such as clubs and maces. One or two hands.")]
        Blunt,

        [StatInfo(StatType.Skill, StatCategory.Melee,
            Code = "FLAI",
            Name = "Flails",
            Description = "One's ability to wield swinging weapons, such as flails, whips, ball-and-chains. Flailing attacks can often be charged. One or two hands.")]
        Flail,

        [StatInfo(StatType.Skill, StatCategory.Melee,
            Code = "SML",
            Name = "Small Arms",
            Description = "One's handiness with small arms, such as daggers or knives. One hand only.")]
        SmallArm,

        [StatInfo(StatType.Skill, StatCategory.Melee,
            Code = "POLE",
            Name = "Pole-Arms",
            Description = "One's handiness with pole-arms, such as staves or spears. Two hands only.")]
        PoleArm,

        #endregion // melee

        #region ranged

        [StatInfo(StatType.Skill, StatCategory.Ranged,
            Code = "THRO",
            Name = "Throwing",
            Description = "One's ability to throw projectiles, such as throwing knives or caltrops.")]
        Throwing,

        [StatInfo(StatType.Skill, StatCategory.Ranged,
            Code = "SBOW",
            Name = "Shortbows",
            Description = "One's ability to weild a short bow.")]
        Shortbow,

        [StatInfo(StatType.Skill, StatCategory.Ranged,
            Code = "LBOW",
            Name = "Longbows",
            Description = "One's ability to weild a long bow.")]
        Longbow,

        [StatInfo(StatType.Skill, StatCategory.Ranged,
            Code = "TRIG",
            Name = "Trigger Weapons",
            Description = "One's ability to use trigger weapons such as crossbows or guns.")]
        Trigger,

        #endregion // ranged

        #region unarmed

        [StatInfo(StatType.Skill, StatCategory.Unarmed,
            Code = "PNCH",
            Name = "Punching",
            Description = "The ability to use one's hands in combat, including knuckle dusters.")]
        Punch,

        [StatInfo(StatType.Skill, StatCategory.Unarmed,
            Code = "KICK",
            Name = "Kicking",
            Description = "The ability to use one's legs in combat.")]
        Kick,

        [StatInfo(StatType.Skill, StatCategory.Unarmed,
            Code = "GRAB",
            Name = "Grabbing",
            Description = "The ability to grab onto people or things.")]
        Grab,

        [StatInfo(StatType.Skill, StatCategory.Unarmed,
            Code = "QI",
            Name = "Qi",
            Description = "The ability to channel one's life energy into attacks; a martial art created by Oru's monks.")]
        Qi,

        #endregion // unarmed

        #region casting

        [StatInfo(StatType.Skill, StatCategory.Casting,
            Code = "SELF",
            Name = "Self-Casting",
            Description = "One's ability to cast sorcery or wizardry spells on themself.")]
        Self,

        [StatInfo(StatType.Skill, StatCategory.Casting,
            Code = "TCH",
            Name = "Touch-Casting",
            Description = "One's ability to cast sorcery or wizardry spells by touching things. Also used when placing static spells.")]
        Touch,

        [StatInfo(StatType.Skill, StatCategory.Casting,
            Code = "PROJ",
            Name = "Projectile-Casting",
            Description = "One's ability to cast sorcery or wizardry spells at range.")]
        Projectile,

        [StatInfo(StatType.Skill, StatCategory.Casting,
            Code = "AOE",
            Name = "AOE-Casting",
            Description = "One's ability to cast sorcery or wizardry spells over wide areas.")]
        AreaOfEffect,

        #endregion // casting

        #endregion // PROFICIENCIES
    }
}
