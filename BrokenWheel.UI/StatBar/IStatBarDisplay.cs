﻿using BrokenWheel.Core.Settings;
using BrokenWheel.UI.Common;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// A simple interface for controlling the regions that constitute a stat bar.
    /// To be implemented in-engine for the GUI, where the methods affect display rectangles.
    /// Does not handle any logic regarding positioning, which is handled by <see cref="IStatBar"/>.
    /// Implementation is responsible for interpolating the distances, handling fading effects, et cetera.
    /// </summary>
    public interface IStatBarDisplay : IDisplay
    {
        /// <summary>
        /// Sets the colors of the bar to a new setting.
        /// </summary>
        void SetColorProfile(StatBarColorSettings colors);
        
        /// <summary>
        /// Sets the stat bar's position relative to the master position, and the size of the outline.
        /// The border is overlapped by all other display sections.
        /// </summary>
        void SetBorderDimensions(int xPosition, int yPosition, int width, int height);

        /// <summary>
        /// Sets the stat bar's background position and size relative to the bottom-left corner of the stat bar.
        /// Aside from the border, the background is overlapped by all other display sections.
        /// </summary>
        void SetBackgroundDimensions(int xPosition, int yPosition, int width, int height);

        /// <summary>
        /// Sets the stat bar's primary-colored section's position and size relative to the bottom-left of the stat bar.
        /// When health is decreasing, represents the target value; when increasing, represents the current value.
        /// Due to changing positioning, this and the secondary section should not overlap.
        /// When stat bar is full, this will be the only section showing aside from any border.
        /// </summary>
        void SetPrimaryDimensions(int xPosition, int yPosition, int width, int height);

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
