using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// An object which controls the <see cref="IStatBarDisplay"/>s conveying a <see cref="IComplexStatistic"/>.
    /// </summary>
    internal interface IStatBar : IListener<ComplexStatUpdatedEvent>
    {
        /// <summary>
        /// The GUI representation of the stat bar.
        /// </summary>
        IStatBarDisplay Display { get; }

        /// <summary>
        /// The type of stat being tracked.
        /// </summary>
        StatInfo Info { get; }
        
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
        /// Sets the position of the bar then updates the display according to its statistic.
        /// The class is in charge of mirroring top or bottom.
        /// </summary>
        void SetPosition(int xPosition, int yPosition);
    }
}
