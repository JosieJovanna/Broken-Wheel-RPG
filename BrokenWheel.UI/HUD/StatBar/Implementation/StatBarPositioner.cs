using System;
using System.Collections.Generic;
using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.UI.Display;
using BrokenWheel.UI.Settings.StatBar;
using BrokenWheel.UI.Settings.StatBar.Enum;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    internal static class StatBarPositioner
    {
        private static readonly IDisplayTool _displayTool;
        private static readonly StatBarSettings _settings;

        private static IList<StatBar> _list;
        private static StatBarCorner _corner;
        private static bool _isVertical;
        private static int _spacing;
        private static int _alignment;
        private static int _distance;
        private static int _thickness;

        static StatBarPositioner()
        {
            var module = Injection.GetModule();
            _displayTool = module.GetService<IDisplayTool>();
            _settings = module.GetSettings<StatBarSettings>();
        }

        public static void PositionBars(IList<StatBar> statBarRelationships)
        {
            _list = statBarRelationships ?? throw new ArgumentException(nameof(statBarRelationships));
            CacheCurrentSettings();
            for (var i = 0; i < _list.Count; i++)
                PositionBar(i);
        }

        private static void CacheCurrentSettings()
        {
            _corner = _settings.DisplayCorner;
            _isVertical = _settings.IsVertical;
            _spacing = _settings.Spacing;
            _alignment = _isVertical ? _settings.PixelsFromHorizontalEdge : _settings.PixelsFromVerticalEdge;
            _distance = _isVertical ? _settings.PixelsFromVerticalEdge : _settings.PixelsFromHorizontalEdge;
            _thickness = _settings.BorderSize * 2 + _settings.Thickness;
        }

        private static void PositionBar(int i)
        {
            var offset = (_thickness + _spacing) * i + _distance;
            var x = CalculateX(offset);
            var y = CalculateY(offset);
            _list[i].SetPosition(x, y);
        }

        private static int CalculateX(int offset)
        {
            var x = _isVertical ? _alignment : offset;
            if (_corner == StatBarCorner.BottomRight || _corner == StatBarCorner.TopRight)
                return _displayTool.ScaledResolution.Width - x;
            return x;
        }

        private static int CalculateY(int offset)
        {
            var y = _isVertical ? offset : _alignment;
            if (_corner == StatBarCorner.TopLeft || _corner == StatBarCorner.TopRight)
                return _displayTool.ScaledResolution.Height - y;
            return y;
        }
    }
}
