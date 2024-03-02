namespace BrokenWheel.Entity.Meta
{
    public class EntityMeta
    {
        public PhysicsMeta Physics { get; } = new PhysicsMeta();

        /// <summary>
        /// The amount of time, in seconds, that must pass before a jump can be made again.
        /// </summary>
        public double JumpCoolDown { get; set; } = 1;

        /// <summary>
        /// The maximum number of jumps that an entity can make before touching the ground again.
        /// </summary>
        public int NumberOfJumps { get; set; } = 2; // TODO: movement meta

        /// <summary>
        /// The amount of time after leaving a ledge during which the entity can still jump.
        /// </summary>
        public double HangTime { get; set; } = .5f;

        /// <summary>
        /// The total amount of time the entity has available for any extra jumps after the first.
        /// </summary>
        public double TimeToJump { get; set; } = 5;
    }
}
