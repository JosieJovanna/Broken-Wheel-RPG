namespace BrokenWheel.UI.StatBar
{
    public interface IStatBarSuiteDisplay
    {
        void Show();
        void Hide();

        IStatBarDisplay AddDisplay(string name);
        void RemoveDisplay(IStatBarDisplay display);
    }
}
