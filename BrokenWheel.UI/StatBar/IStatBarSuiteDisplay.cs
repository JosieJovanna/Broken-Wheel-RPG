using BrokenWheel.UI.Common;

namespace BrokenWheel.UI.StatBar
{
    public interface IStatBarSuiteDisplay : IUIElement
    {
        T CreateStatBarElement<T>(string name) where T : IStatBarUIElement;
        void RemoveStatBarElement(IStatBarUIElement uiElement);
    }
}
