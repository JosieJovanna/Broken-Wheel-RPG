namespace BrokenWheel.UI.StatBar
{
    public interface IStatBarSuiteDisplay
    {
        void Show();
        void Hide();

        void AddBar(IStatBarDisplay display);
        void RemoveBar(IStatBarDisplay display);
    }
}
