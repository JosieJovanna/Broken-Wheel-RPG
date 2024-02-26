namespace BrokenWheel.Entity.Meta
{
    public class PhysicsMeta
    {
        /// <summary>
        /// This individual entity's acceleration in meters per second per second.
        /// </summary>
        public float Gravity { get; set; } = -9.8f; // TODO: const

        /// <summary>
        /// "Do you think that if you were falling in space, you would slow down after a while or go faster and faster?"
        /// 
        /// ...
        /// 
        /// "Faster and faster. 
        /// For a long time, you wouldn't feel anything. 
        /// Then 
        /// you'd burst into 
        /// fire. 
        /// 
        /// Forever. 
        /// 
        /// And the angels wouldn't help you... 
        /// because they've all gone away."
        /// </summary>
        public float TerminalVelocity { get; set; } = float.MaxValue;

        /// <summary>
        /// The modifier applied to realtime for processing.
        /// </summary>
        public float TimeScale { get; set; } = 1;
    }
}
