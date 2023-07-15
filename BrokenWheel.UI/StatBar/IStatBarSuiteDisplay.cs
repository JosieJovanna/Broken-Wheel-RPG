using BrokenWheel.Core.Settings;

namespace BrokenWheel.UI.StatBar
{
    public interface IStatBarSuiteDisplay
    {
        void Show();
        void Hide();

        void AddBar(IStatBarDisplay display);
        void RemoveDisplay(IStatBarDisplay display);

        IStatBarDisplay CreateStatBarDisplay(StatBarColorSettings colorSettings);
        void DestroyStatBarDisplay(IStatBarDisplay display);
    }
}
