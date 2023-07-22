using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// An object which controls the <see cref="IStatBarUIElement"/>s conveying a <see cref="IComplexStatistic"/>.
    /// </summary>
    internal interface IStatBar
    {
        /// <summary>
        /// The type of stat being tracked.
        /// </summary>
        StatInfo Info { get; }
        
        /// <summary>
        /// Whether the individual stat bar is currently being rendered.
        /// </summary>
        bool IsHidden { get; }
        
        /// <summary>
        /// The order in which the stat bar is displayed. Results are unpredictable if two stat bars have the same order.
        /// </summary>
        int Order { get; set; }

        /// <summary>
        /// Shows the individual stat bar.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides the individual stat bar.
        /// </summary>
        void Hide();

        /// <summary>
        /// Sets the position of the bar then updates the display according to its statistic.
        /// The class is in charge of mirroring top or bottom.
        /// </summary>
        void SetPosition(int xPosition, int yPosition);
    }
}
