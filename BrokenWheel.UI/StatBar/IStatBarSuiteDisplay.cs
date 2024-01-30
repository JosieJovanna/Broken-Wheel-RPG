namespace BrokenWheel.UI.StatBar
{
    public interface IStatBarSuiteDisplay
    {
        void Show();
        void Hide();

        IStatBarUIElement AddDisplay(string name);
        void RemoveDisplay(IStatBarUIElement uiElement);
    }
}
