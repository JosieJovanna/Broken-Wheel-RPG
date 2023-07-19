using BrokenWheel.Math.Utility;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal static class StatBarDisplayUpdater
    {
        public static void UpdateDisplay(IStatBarDisplay display, UpdateDisplayParameters parameters)
        {
            display.SetPosition(parameters.BaseX, parameters.BaseY);
            UpdateBorder(display, parameters);
            UpdateBackground(display, parameters);
            UpdatePrimary(display, parameters, out var primaryLength);
            UpdateSecondary(display, parameters, primaryLength);
            UpdateExhaustion(display, parameters);
        }
        
        private static void UpdateBorder(IStatBarDisplay display, UpdateDisplayParameters p)
        {
            var borderSizeX2 = p.BorderSize * 2;
            var length = p.FullLength + borderSizeX2;
            var thickness = p.Thickness + borderSizeX2;
            var width = p.IsVertical ? thickness : length;
            var height = p.IsVertical ? length : thickness;
            display.SetBorderDimensions(0, 0, width, height);
        }

        private static void UpdateBackground(IStatBarDisplay display, UpdateDisplayParameters p)
        {
            var width = p.IsVertical ? p.Thickness : p.FullLength;
            var height = p.IsVertical ? p.FullLength : p.Thickness;
            display.SetBackgroundDimensions(p.BorderSize, p.BorderSize, width, height);
        }

        private static void UpdatePrimary(IStatBarDisplay display, UpdateDisplayParameters p, out int length)
        {
            length = 10; // TODO: current value/destination value
            var width = p.IsVertical ? p.Thickness : length;
            var height = p.IsVertical ? length : p.Thickness;
            display.SetPrimaryDimensions(p.BorderSize, p.BorderSize, width, height);
        }

        private static void UpdateSecondary(IStatBarDisplay display, UpdateDisplayParameters p, int primaryLength)
        {
            var length = 5; // TODO: destination value/current value
            var (x, y, width, height) = p.IsVertical 
                ? VerticalSecondaryDimensions(p, primaryLength, length) 
                : HorizontalSecondaryDimensions(p, primaryLength, length);
            display.SetSecondaryDimensions(x, y, width, height);
        }

        private static (int, int, int, int) VerticalSecondaryDimensions(UpdateDisplayParameters p, int primaryLength, int length)
        {
            var x = p.BorderSize;
            var y = p.BorderSize + primaryLength + length;
            var width = p.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalSecondaryDimensions(UpdateDisplayParameters p, int primaryLength, int length)
        {
            var x = p.BorderSize + primaryLength + length;
            var y = p.BorderSize;
            var width = length;
            var height = p.Thickness;
            return (x, y, width, height);
        }

        private static void UpdateExhaustion(IStatBarDisplay display, UpdateDisplayParameters p)
        {
            var length = MathUtil.RaiseDoubleToInt(p.PPP * p.Stat.Exhaustion);
            var (x, y, width, height) = p.IsVertical
                ? VerticalExhaustionDimensions(p, length)
                : HorizontalExhaustionDimensions(p, length);
            display.SetExhaustionDimensions(x, y, width, height);
        }
        

        private static (int, int, int, int) VerticalExhaustionDimensions(UpdateDisplayParameters p, int length)
        {
            var x = p.BorderSize;
            var y = p.BorderSize + p.FullLength - length;
            var width = p.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalExhaustionDimensions(UpdateDisplayParameters p, int length)
        {
            var x = p.BorderSize + p.FullLength - length;
            var y = p.BorderSize;
            var width = length;
            var height = p.Thickness;
            return (x, y, width, height);
        }
    }
}
