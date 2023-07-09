using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// An object which controls the <see cref="IStatBarDisplay"/>s conveying a <see cref="IComplexStatistic"/>.
    /// Several will exist, and be managed by a (TODO) which controls the stat bars in relation to each other,
    /// and handles updating stats and displays -- event handling, etc.
    /// </summary>
    public interface IStatBar
    {
        /// <summary>
        /// The type of stat being tracked.
        /// </summary>
        StatTypeInfo Type { get; }

        /// <summary>
        /// The overall position of the bar, relative to the lower-left corner of the screen.
        /// </summary>
        void SetPosition(int xPosition, int yPosition);

        /// <summary>
        /// Updates the display according to the stat values and settings.
        /// </summary>
        void UpdateDisplay();
    }
}
