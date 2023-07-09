using System.Collections.Generic;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// A controller for displaying multiple <see cref="IStatBar"/>s on the UI, and coordinating their positions.
    /// </summary>
    public interface IStatBarSuite
    {
        /// <summary>
        /// Event listener for stat changes.
        /// </summary>
        void StatUpdateHandler(object sender, StatUpdateEventArgs args);
        
        /// <summary>
        /// Updates all of the stat bars, to reflect stat values and settings.
        /// </summary>
        void Update();
        
        /// <summary>
        /// Gets a list of all current stats with bars assigned to them. There can only be one stat per <see cref="StatType"/>.
        /// </summary>
        IList<IComplexStatistic> GetStats();
        
        /// <summary>
        /// Adds a bar for the specified stat, if there is not already a stat of that type being displayed.
        /// </summary>
        void AddStat(IComplexStatistic statistic);
        
        /// <summary>
        /// Removes the <see cref="IStatBar"/> for the specified stat type.
        /// </summary>
        void RemoveStat(StatType statType);
    }
}
