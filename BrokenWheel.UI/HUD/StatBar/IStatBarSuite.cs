﻿using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.HUD.StatBar
{
    /// <summary>
    /// A controller for displaying multiple stat bars on the UI, and coordinating their positions.
    /// </summary>
    public interface IStatBarSuite
    {
        /// <summary>
        /// Shows all stat bars.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides all stat bars.
        /// </summary>
        void Hide();
        
        /// <summary>
        /// Updates all of the stat bars, to reflect stat values and settings.
        /// </summary>
        void RepositionDisplays();

        /// <summary>
        /// Adds a bar for the specified stat, if there is not already a stat of that type being displayed.
        /// </summary>
        void AddStat(StatInfo info);
        
        /// <summary>
        /// Adds a bar for the specified stat, if there is not already a stat of that type being displayed.
        /// </summary>
        void AddStat(StatType type);
        
        /// <summary>
        /// Adds a bar for the specified custom stat, if there is not already a stat of that type being displayed.
        /// </summary>
        void AddCustomStat(string code);
        
        /// <summary>
        /// Removes the stat bar for the specified stat type, if it exists.
        /// May not remove health, stamina, or willpower bars.
        /// </summary>
        void RemoveStat(StatInfo info);
        
        /// <summary>
        /// Removes the stat bar for the specified stat type, if it exists.
        /// May not remove health, stamina, or willpower bars.
        /// </summary>
        void RemoveStat(StatType type);

        /// <summary>
        /// Removes the stat bar for the specified stat type, if it exists.
        /// May not remove health, stamina, or willpower bars.
        /// </summary>
        void RemoveCustomStat(string code);
    }
}
