using System;
using System.Collections.Generic;
using BrokenWheel.Core.Settings;
using BrokenWheel.UI.Display;

namespace BrokenWheel.UI.StatBar.Implementation
{
    internal static class StatBarPositioner
    {
        private static StatBarSettings _settings;
        private static IList<IStatBar> _list;
        private static StatBarCorner _corner;
        private static bool _isVertical;
        private static int _spacing;
        private static int _alignment;
        private static int _distance;
        private static int _thickness;

        public static void PositionBars(
            StatBarSettings statBarSettings, 
            IList<IStatBar> statBarRelationships)
        {
            _settings = statBarSettings ?? throw new ArgumentNullException(nameof(statBarSettings));
            _list = statBarRelationships ?? throw new ArgumentException(nameof(statBarRelationships));
            InitiateToPreventRaceConditions();
            for (var i = 0; i < _list.Count; i++)
                PositionBar(i);
        }

        private static void InitiateToPreventRaceConditions()
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
                return DisplayInfo.UIWidth() - x;
            return x;
        }

        private static int CalculateY(int offset)
        {
            var y = _isVertical ? offset : _alignment;
            if (_corner == StatBarCorner.TopLeft || _corner == StatBarCorner.TopRight)
                return DisplayInfo.UIHeight() - y;
            return y;
        }
    }
}
