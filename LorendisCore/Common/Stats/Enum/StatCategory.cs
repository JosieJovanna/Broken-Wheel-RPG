using LorendisCore.Common.Stats.Attributes;

namespace LorendisCore.Common.Stats
{
    public enum StatCategory
    {
        // ATTRIBUTES
        [Attribute]
        Vitals,
        [Attribute]
        Movement,

        // SKILLS
        [Skill]
        Defense,
        [Skill]
        Combat,
        [Skill]
        Sorcery,
        [Skill]
        Wizardry,
        [Skill]
        Crafting,
        [Skill]
        Misc,

        // PROFICIENCIES
        [Proficiency]
        Melee,
        [Proficiency]
        Ranged,
        [Proficiency]
        Unarmed,
        [Proficiency]
        Casting,
    }
}
