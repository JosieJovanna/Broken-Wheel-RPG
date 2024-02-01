namespace BrokenWheel.Core.Stats.Enum
{
    public enum StatType
    {
        Custom = default,

        /// <summary>
        /// Stats which increase every level, independent of choice.
        /// Includes HP, WP, and SP -- the only complex stat types in the base engine -- and movement stats.
        /// </summary>
        Attribute,

        /// <summary>
        /// Stats that increase by using skill points.
        /// The main way in which a character is designed.
        /// </summary>
        Skill,

        /// <summary>
        /// Stats that increase with use.
        /// Includes specific weapon type proficiencies.
        /// </summary>
        Proficiency,

        /// <summary>
        /// A stat which tracks some abstract value, such as progress in a quest.
        /// </summary>
        Tracker,
    }
}
