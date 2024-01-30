using BrokenWheel.Math.Utility;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal static class StatBarDisplayUpdater
    {
        public static void UpdateDisplay(IStatBarUIElement uiElement, UpdateDisplayParameters parameters)
        {
            uiElement.SetPosition(parameters.BaseX, parameters.BaseY);
            UpdateBorder(uiElement, parameters);
            UpdateBackground(uiElement, parameters);
            UpdatePrimary(uiElement, parameters, out var primaryLength);
            UpdateSecondary(uiElement, parameters, primaryLength);
            UpdateExhaustion(uiElement, parameters);
        }
        
        private static void UpdateBorder(IStatBarUIElement uiElement, UpdateDisplayParameters p)
        {
            var borderSizeX2 = p.BorderSize * 2;
            var length = p.FullLength + borderSizeX2;
            var thickness = p.Thickness + borderSizeX2;
            var width = p.IsVertical ? thickness : length;
            var height = p.IsVertical ? length : thickness;
            uiElement.SetBorderDimensions(0, 0, width, height);
        }

        private static void UpdateBackground(IStatBarUIElement uiElement, UpdateDisplayParameters p)
        {
            var width = p.IsVertical ? p.Thickness : p.FullLength;
            var height = p.IsVertical ? p.FullLength : p.Thickness;
            uiElement.SetBackgroundDimensions(p.BorderSize, p.BorderSize, width, height);
        }

        private static void UpdatePrimary(IStatBarUIElement uiElement, UpdateDisplayParameters p, out int length)
        {
            length = 10; // TODO: current value/destination value
            var width = p.IsVertical ? p.Thickness : length;
            var height = p.IsVertical ? length : p.Thickness;
            uiElement.SetPrimaryDimensions(p.BorderSize, p.BorderSize, width, height);
        }

        private static void UpdateSecondary(IStatBarUIElement uiElement, UpdateDisplayParameters p, int primaryLength)
        {
            var length = 5; // TODO: destination value/current value
            var (x, y, width, height) = p.IsVertical 
                ? VerticalSecondaryDimensions(p, primaryLength, length) 
                : HorizontalSecondaryDimensions(p, primaryLength, length);
            uiElement.SetSecondaryDimensions(x, y, width, height);
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

        private static void UpdateExhaustion(IStatBarUIElement uiElement, UpdateDisplayParameters p)
        {
            var length = MathUtil.RaiseDoubleToInt(p.PPP * p.Stat.Exhaustion);
            var (x, y, width, height) = p.IsVertical
                ? VerticalExhaustionDimensions(p, length)
                : HorizontalExhaustionDimensions(p, length);
            uiElement.SetExhaustionDimensions(x, y, width, height);
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
