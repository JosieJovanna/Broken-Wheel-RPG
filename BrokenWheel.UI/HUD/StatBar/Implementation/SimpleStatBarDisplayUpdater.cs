using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Math.Utility;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    internal static class SimpleStatBarDisplayUpdater
    {
        internal static void UpdateDisplay(
            IStatBarDisplay display, UpdateDisplayParameters<Stat> parameters)
        {
            display.SetPosition(parameters.BaseX, parameters.BaseY);
            UpdateBorder(display, parameters);
            UpdateBackground(display, parameters);
            UpdatePrimary(display, parameters);
        }
        
        internal static void UpdateBorder<TStat>(
            IStatBarDisplay display, UpdateDisplayParameters<TStat> parameters)
            where TStat : Stat
        {
            var borderSizeX2 = parameters.BorderSize * 2;
            var length = parameters.FullLength + borderSizeX2;
            var thickness = parameters.Thickness + borderSizeX2;
            var width = parameters.IsVertical ? thickness : length;
            var height = parameters.IsVertical ? length : thickness;
            display.SetBorderDimensions(0, 0, width, height);
        }

        internal static void UpdateBackground<TStat>(
            IStatBarDisplay display, UpdateDisplayParameters<TStat> parameters)
            where TStat : Stat
        {
            var width = parameters.IsVertical ? parameters.Thickness : parameters.FullLength;
            var height = parameters.IsVertical ? parameters.FullLength : parameters.Thickness;
            display.SetBackgroundDimensions(parameters.BorderSize, parameters.BorderSize, width, height);
        }

        private static void UpdatePrimary(
            IStatBarDisplay display, UpdateDisplayParameters<Stat> parameters)
        {
            var length = MathUtil.LowerDoubleToInt(parameters.PPP * parameters.Stat.EffectiveValue);
            var width = parameters.IsVertical ? parameters.Thickness : length;
            var height = parameters.IsVertical ? length : parameters.Thickness;
            display.SetPrimaryDimensions(parameters.BorderSize, parameters.BorderSize, width, height);
        }
    }
}
