using BrokenWheel.Core.Events.Stats;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// An object which controls the <see cref="IStatBarDisplay"/>s conveying a <see cref="IComplexStatistic"/>.
    /// </summary>
    internal interface IStatBar
    {
        /// <summary>
        /// The GUI representation of the stat bar.
        /// </summary>
        IStatBarDisplay Display { get; }

        /// <summary>
        /// The type of stat being tracked.
        /// </summary>
        StatTypeInfo Info { get; }
        
        /// <summary>
        /// Whether the individual stat bar is currently being rendered.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Shows the individual stat bar.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides the individual stat bar.
        /// </summary>
        void Hide();

        /// <summary>
        /// Event listener for stat changes. Passes <see cref="StatUpdateEventArgs"/> in case some implementation
        /// applies special effects based on damage, such as, for example, flames emitting on taking fire damage.
        /// </summary>
        void StatUpdateHandler(object sender, StatUpdateEventArgs args);

        /// <summary>
        /// Sets the position of the bar then updates the display according to its statistic.
        /// </summary>
        /// <param name="xPosition"> The X position of the bar, before accounting for length, if on the right side. </param>
        /// <param name="yPosition"> The Y position of the bar, before accounting for length, if on the top. </param>
        void Update(int xPosition, int yPosition);
    }
}
