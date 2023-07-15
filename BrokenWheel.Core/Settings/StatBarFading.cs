namespace BrokenWheel.Core.Settings
{
    public enum StatBarFading
    {
        /// <summary>
        /// All stat bars show all of the time.
        /// </summary>
        AlwaysShowing = 99,
        
        /// <summary>
        /// Stat bars are never shown. (Why would you want this?)
        /// </summary>
        NeverShowing = 100,
        
        /// <summary>
        /// Individual stat bars will only show when their stat has recently changed.
        /// </summary>
        HideWhenNotChanged = default,
        
        /// <summary>
        /// Stat bars are shown when any of their stats has recently changed.
        /// </summary>
        HideAllWhenNotChanged = 2,
        
        /// <summary>
        /// Stat bars are never shown when not in combat, unless the value is below a certain percentage of the maximum.
        /// For example, if stamina is low while running outside of combat, its stat bar will display.
        /// </summary>
        HideWhenNotInCombat = 3,
        
        /// <summary>
        /// Stat bars are never shown outside of combat, unless any stat bar is within a certain percentage of the maximum.
        /// For example, if stamina is low while running outside of combat, all stat bars will display.
        /// </summary>
        HideAllWhenNotInCombat = 4,
        
        /// <summary>
        /// Stat bars are never shown when outside of combat, unless its stat is within a certain percentage of the maximum.
        /// </summary>
        HideWhenNotInCombatAndNotChanged = 5,
        
        /// <summary>
        /// Stat bars are never shown when outside of combat, unless any stat is within a certain percentage of the maximum.
        /// </summary>
        HideAllWhenNotInCombatAndNotChanged = 6,
    }
}
