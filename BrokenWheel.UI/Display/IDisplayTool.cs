using BrokenWheel.Core.Events;
using BrokenWheel.Core.GameModes;
using BrokenWheel.Core.Settings;
using BrokenWheel.UI.Settings;

namespace BrokenWheel.UI.Display
{
    /// <summary>
    /// An interface used by <see cref="DisplayInfo"/> to get information on the implementation's display.
    /// </summary>
    public interface IDisplayTool
        : IEventHandler<GameModeUpdateEvent>
        , IEventHandler<SettingsUpdateEvent<DisplaySettings>>
    {
        bool IsUIMode { get; }
        int UIScale { get; }

        (int Width, int Height) TrueResolution { get; }
        (int X, int Y) TrueCentre { get; }
        (int Width, int Height) ScaledResolution { get; }
        (int X, int Y) ScaledCentre { get; }

        void LockCursor();
        void FreeCursor();
    }
}
