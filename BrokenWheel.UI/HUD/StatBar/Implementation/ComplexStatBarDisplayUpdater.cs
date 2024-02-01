using BrokenWheel.Core.Stats;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    internal static class ComplexStatBarDisplayUpdater
    {
        public static void UpdateDisplay(
            IComplexStatBarDisplay display,
            UpdateDisplayParameters<ComplexStatistic> parameters)
        {
            display.SetPosition(parameters.BaseX, parameters.BaseY);
            SimpleStatBarDisplayUpdater.UpdateBorder(display, parameters);
            SimpleStatBarDisplayUpdater.UpdateBackground(display, parameters);
            UpdatePrimary(display, parameters, out var primaryLength);
            UpdateSecondary(display, parameters, primaryLength);
            UpdateExhaustion(display, parameters);
        }

        private static void UpdatePrimary(
            IComplexStatBarDisplay display,
            UpdateDisplayParameters<ComplexStatistic> parameters, out int length)
        {
            var primaryStat = parameters.Stat.Value >= parameters.Stat.DestinationValue // decreasing?
                ? parameters.Stat.DestinationValue // colored up to destination
                : parameters.Stat.Value; // colored up to current value
            length = MathUtil.LowerDoubleToInt(parameters.PPP * primaryStat);
            var width = parameters.IsVertical ? parameters.Thickness : length;
            var height = parameters.IsVertical ? length : parameters.Thickness;
            display.SetPrimaryDimensions(parameters.BorderSize, parameters.BorderSize, width, height);
        }

        private static void UpdateSecondary(
            IComplexStatBarDisplay display, UpdateDisplayParameters<ComplexStatistic> parameters, int primaryLength)
        {
            var secondaryStat = parameters.Stat.Value >= parameters.Stat.DestinationValue // decreasing?
                ? parameters.Stat.Value - parameters.Stat.DestinationValue // color value above destination
                : parameters.Stat.DestinationValue - parameters.Stat.Value; // color value yet to be gained
            var length = 5; // TODO: destination value/current value
            var (x, y, width, height) = parameters.IsVertical
                ? VerticalSecondaryDimensions(parameters, primaryLength, length)
                : HorizontalSecondaryDimensions(parameters, primaryLength, length);
            display.SetSecondaryDimensions(x, y, width, height);
        }

        private static (int, int, int, int) VerticalSecondaryDimensions(
            UpdateDisplayParameters<ComplexStatistic> parameters, int primaryLength, int length)
        {
            var x = parameters.BorderSize;
            var y = parameters.BorderSize + primaryLength;
            var width = parameters.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalSecondaryDimensions(
            UpdateDisplayParameters<ComplexStatistic> parameters, int primaryLength, int length)
        {
            var x = parameters.BorderSize + primaryLength;
            var y = parameters.BorderSize;
            var width = length;
            var height = parameters.Thickness;
            return (x, y, width, height);
        }

        private static void UpdateExhaustion(
            IComplexStatBarDisplay display, UpdateDisplayParameters<ComplexStatistic> parameters)
        {
            var length = MathUtil.RaiseDoubleToInt(parameters.PPP * parameters.Stat.Exhaustion);
            var (x, y, width, height) = parameters.IsVertical
                ? VerticalExhaustionDimensions(parameters, length)
                : HorizontalExhaustionDimensions(parameters, length);
            display.SetExhaustionDimensions(x, y, width, height);
        }


        private static (int, int, int, int) VerticalExhaustionDimensions(
            UpdateDisplayParameters<ComplexStatistic> parameters, int length)
        {
            var x = parameters.BorderSize;
            var y = parameters.BorderSize + parameters.FullLength - length;
            var width = parameters.Thickness;
            var height = length;
            return (x, y, width, height);
        }

        private static (int, int, int, int) HorizontalExhaustionDimensions(
            UpdateDisplayParameters<ComplexStatistic> parameters, int length)
        {
            var x = parameters.BorderSize + parameters.FullLength - length;
            var y = parameters.BorderSize;
            var width = length;
            var height = parameters.Thickness;
            return (x, y, width, height);
        }
    }
}
