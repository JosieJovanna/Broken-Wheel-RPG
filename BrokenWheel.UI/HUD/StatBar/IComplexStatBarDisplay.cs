namespace BrokenWheel.UI.HUD.StatBar
{
    public interface IComplexStatBarDisplay : IStatBarDisplay
    {
        /// <summary>
        /// Sets the stat bar's secondary-colored section's position and size relative to the bottom-left of the stat bar.
        /// When health is decreasing, represents the current value; when increasing, represents the target value.
        /// Due to changing positioning, this and the primary section should not overlap.
        /// </summary>
        void SetSecondaryDimensions(int xPosition, int yPosition, int width, int height);

        /// <summary>
        /// Sets the stat bar's exhaustion position and size relative to the bottom-left corner of the stat bar.
        /// The exhaustion region overlaps all other display sections.
        /// </summary>
        void SetExhaustionDimensions(int xPosition, int yPosition, int width, int height);
    }
}
