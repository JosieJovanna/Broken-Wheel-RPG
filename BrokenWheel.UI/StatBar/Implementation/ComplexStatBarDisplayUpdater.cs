using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal static class ComplexStatBarDisplayUpdater
    {
        public static void UpdateDisplay(
            IComplexStatBarDisplay uiElement, UpdateDisplayParameters<ComplexStat> parameters)
        {
            uiElement.SetPosition(parameters.BaseX, parameters.BaseY);
            UpdateBorder(uiElement, parameters);
            UpdateBackground(uiElement, parameters);
            UpdatePrimary(uiElement, parameters, out var primaryLength);
            UpdateSecondary(uiElement, parameters, primaryLength);
            UpdateExhaustion(uiElement, parameters);
        }
        
        private static void UpdateBorder(
            IStatBarDisplay display, UpdateDisplayParameters<ComplexStat> parameters)
        {
            var borderSizeX2 = parameters.BorderSize * 2;
            var length = parameters.FullLength + borderSizeX2;
            var thickness = parameters.Thickness + borderSizeX2;
            var width = parameters.IsVertical ? thickness : length;
            var height = parameters.IsVertical ? length : thickness;
            display.SetBorderDimensions(0, 0, width, height);
        }

        private static void UpdateBackground(
            IComplexStatBarDisplay uiElement, UpdateDisplayParameters<ComplexStat> parameters)
        {
            var width = parameters.IsVertical ? parameters.Thickness : parameters.FullLength;
            var height = parameters.IsVertical ? parameters.FullLength : parameters.Thickness;
            uiElement.SetBackgroundDimensions(parameters.BorderSize, parameters.BorderSize, width, height);
        }

        private static void UpdatePrimary(
            IComplexStatBarDisplay uiElement, UpdateDisplayParameters<ComplexStat> parameters, out int length)
        {
            length = 10; // TODO: current value/destination value
            var width = parameters.IsVertical ? parameters.Thickness : length;
            var height = parameters.IsVertical ? length : parameters.Thickness;
            uiElement.SetPrimaryDimensions(parameters.BorderSize, parameters.BorderSize, width, height);
        }

        private static void UpdateSecondary(
            IComplexStatBarDisplay uiElement, UpdateDisplayParameters<ComplexStat> parameters, int primaryLength)
        {
            var length = 5; // TODO: destination value/current value
            var (x, y, width, height) = parameters.IsVertical 
                ? VerticalSecondaryDimensions(parameters, primaryLength, length) 
                : HorizontalSecondaryDimensions(parameters, primaryLength, length);
            uiElement.SetSecondaryDimensions(x, y, width, height);
        }

        private static (int, int, int, int) VerticalSecondaryDimensions(
            UpdateDisplayParameters<ComplexStat> parameters, int primaryLength, int length)
        {
            var x = parameters.BorderSize;
            var y = parameters.BorderSize + primaryLength + length;
            var width = parameters.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalSecondaryDimensions(
            UpdateDisplayParameters<ComplexStat> parameters, int primaryLength, int length)
        {
            var x = parameters.BorderSize + primaryLength + length;
            var y = parameters.BorderSize;
            var width = length;
            var height = parameters.Thickness;
            return (x, y, width, height);
        }

        private static void UpdateExhaustion(
            IComplexStatBarDisplay display, UpdateDisplayParameters<ComplexStat> parameters)
        {
            var length = MathUtil.RaiseDoubleToInt(parameters.PPP * parameters.Stat.Exhaustion);
            var (x, y, width, height) = parameters.IsVertical
                ? VerticalExhaustionDimensions(parameters, length)
                : HorizontalExhaustionDimensions(parameters, length);
            display.SetExhaustionDimensions(x, y, width, height);
        }
        

        private static (int, int, int, int) VerticalExhaustionDimensions(
            UpdateDisplayParameters<ComplexStat> parameters, int length)
        {
            var x = parameters.BorderSize;
            var y = parameters.BorderSize + parameters.FullLength - length;
            var width = parameters.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalExhaustionDimensions(
            UpdateDisplayParameters<ComplexStat> parameters, int length)
        {
            var x = parameters.BorderSize + parameters.FullLength - length;
            var y = parameters.BorderSize;
            var width = length;
            var height = parameters.Thickness;
            return (x, y, width, height);
        }
    }
}
