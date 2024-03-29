﻿using System;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.UI.Settings.StatBar;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    /// <summary>
    /// Stores several properties from settings to avoid race conditions and simplify method signatures.
    /// </summary>
    internal class UpdateDisplayParameters<T> where T : Statistic
    {
        public T Stat { get; }
        public bool IsVertical { get; }
        public double PPP { get; }
        public int BorderSize { get; }
        public int Thickness { get; }
        public int FullLength { get; }
        public int BaseX { get; }
        public int BaseY { get; }

        public UpdateDisplayParameters(
            StatBarSettings settings,
            T stat,
            double ppp,
            int length,
            int x,
            int y)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            Stat = stat;
            IsVertical = settings.IsVertical;
            BorderSize = settings.BorderSize;
            Thickness = settings.Thickness;
            PPP = ppp;
            FullLength = length;
            BaseX = x;
            BaseY = y;
        }
    }
}
