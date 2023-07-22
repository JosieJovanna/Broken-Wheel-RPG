using BrokenWheel.Core.Settings;
using BrokenWheel.UI.Common;

namespace BrokenWheel.UI.HUD.StatBar
{
    public interface IStatBarSuiteDisplay : IUIElement
    {
        /// <summary>
        /// Creates a new Stat Bar GUI element.
        /// </summary>
        /// <param name="name"> The name of the stat. This could be displayed, and should thus be readable. </param>
        /// <param name="colors"> The color that the stat bar will take on. If left null, must be set separately. </param>
        /// <typeparam name="T"> The type of element to create.</typeparam>
        T CreateStatBarElement<T>(string name, StatBarColorSettings colors = null) where T : IStatBarDisplay;
        
        /// <summary>
        /// Removes a stat bar's GUI element from the group display.
        /// </summary>
        /// <param name="display"> The specific element to remove. </param>
        void RemoveStatBarElement(IStatBarDisplay display);
    }
}
