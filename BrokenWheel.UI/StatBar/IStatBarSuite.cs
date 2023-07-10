using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.StatBar
{
    /// <summary>
    /// A controller for displaying multiple <see cref="IStatBar"/>s on the UI, and coordinating their positions.
    /// </summary>
    public interface IStatBarSuite
    {
        /// <summary>
        /// Shows all <see cref="IStatBar"/>s.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides all <see cref="IStatBar"/>s.
        /// </summary>
        void Hide();
        
        /// <summary>
        /// Updates all of the stat bars, to reflect stat values and settings.
        /// </summary>
        void UpdateDisplays();
        
        /// <summary>
        /// Adds a bar for the specified stat, if there is not already a stat of that type being displayed.
        /// </summary>
        void AddStat(StatType statType, IStatBarDisplay display);
        
        /// <summary>
        /// Adds a bar for the specified custom stat, if there is not already a stat of that type being displayed.
        /// </summary>
        void AddCustomStat(string customStatName, IStatBarDisplay display);
        
        /// <summary>
        /// Removes the <see cref="IStatBar"/> for the specified stat type. If the stat type 
        /// </summary>
        void RemoveStat(StatType statType);

        /// <summary>
        /// Removes the <see cref="IStatBar"/> for the specified stat type. If the stat type 
        /// </summary>
        void RemoveCustomStat(StatType customStatName);
    }
}
