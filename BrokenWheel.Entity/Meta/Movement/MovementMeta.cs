namespace BrokenWheel.Entity.Meta.Movement
{
    public class MovementMeta
    {
        /// <summary>
        /// The amount of time, in seconds, that must pass before a jump can be made again.
        /// </summary>
        public double JumpCoolDown { get; set; } = 1.2;

        /// <summary>
        /// The maximum number of jumps that an entity can make before touching the ground again.
        /// </summary>
        public int NumberOfJumps { get; set; } = 2;

        /// <summary>
        /// The amount of time after leaving a ledge during which the entity can still jump.
        /// </summary>
        public double HangTime { get; set; } = .5f;

        /// <summary>
        /// The total amount of time the entity has available for any extra jumps after the first.
        /// </summary>
        public double TimeToJump { get; set; } = 5;

        public float JumpBaseSpeed { get; set; } = 7.0f;
        public float JumpPowerMultiplier { get; set; } = 1.5f;
        public float JumpMaxSpeed { get; set; } = 20.0f;
        public float DirectionChangeAcceleration { get; set; } = 50.0f;

        public SpeedMeta BaseStandingSpeeds { get; set; } = new SpeedMeta { Fast = 4.0f, Normal = 3.0f, Slow = 2.0f };
        public SpeedMeta BaseCrouchingSpeeds { get; set; } = new SpeedMeta { Fast = 3.0f, Normal = 1.8f, Slow = 1.0f };
        public SpeedMeta BaseCrawlingSpeeds { get; set; } = new SpeedMeta { Fast = 1.0f, Normal = 0.8f, Slow = 0.5f };
    }
}
