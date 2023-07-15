﻿using System.Collections.Generic;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;

namespace BrokenWheel.Core.Settings
{
    public sealed class StatBarSettings : ISettings
    {
        /// <summary>
        /// Which corner the stat bars are displayed in.
        /// </summary>
        public StatBarCorner Corner { get; internal set; } = default;

        /// <summary>
        /// The way the stat bars display, relative to each other.
        /// </summary>
        public StatBarDisplayMode DisplayMode { get; internal set; } = default;

        /// <summary>
        /// The conditions under which stat bars are hidden or shown.
        /// </summary>
        public StatBarFading FadingMode { get; internal set; } = default;

        /// <summary>
        /// The order of the main health, stamina, and willpower stats.
        /// </summary>
        public StatType[] MainStatOrder { get; internal set; } = { StatType.HP, StatType.SP, StatType.WP };

        /// <summary>
        /// The color profile of the health bar.
        /// </summary>
        public StatBarColorSettings HpColors { get; internal set; }

        /// <summary>
        /// The color profile of the stamina bar.
        /// </summary>
        public StatBarColorSettings SpColors { get; internal set; }

        /// <summary>
        /// The color profile of the willpower bar.
        /// </summary>
        public StatBarColorSettings WpColors { get; internal set; }

        /// <summary>
        /// The default color profile for any other stat added not included in <see cref="ColorsByCode"/>.
        /// </summary>
        public StatBarColorSettings DefaultColors { get; internal set; }

        /// <summary>
        /// Color profiles for any non-standard stats being tracked.
        /// </summary>
        public IDictionary<string, StatBarColorSettings> ColorsByCode { get; internal set; } =
            new Dictionary<string, StatBarColorSettings>();

        /// <summary>
        /// Whether the stat bars should be displayed vertically, rather than horizontally.
        /// </summary>
        public bool IsVertical { get; internal set; } = true;

        /// <summary>
        /// The horizontal distance from the side of the screen, left or right.
        /// Between zero and the width of the display; if outside this range, rounds to be within it.
        /// </summary>
        public int PixelsFromEdgeX { get; internal set; } = 2;

        /// <summary>
        /// The vertical distance from the side of the screen, bottom or top.
        /// Between zero and the height of the display; if outside this range, rounds to be within it.
        /// </summary>
        public int PixelsFromEdgeY { get; internal set; } = 2;

        /// <summary>
        /// The spacing between stat bars, in pixels.
        /// </summary>
        public int Spacing { get; internal set; } = 2;

        /// <summary>
        /// The size of the border around the stat bars, in pixels. Should not be set too large.
        /// </summary>
        public int BorderSize { get; internal set; } = 1;

        /// <summary>
        /// The thickness, in pixels, of all of the stat bars.
        /// </summary>
        public int Thickness { get; internal set; } = 4;

        /// <summary>
        /// The maximum length of the stat bars, in pixels. Typically, the stat bars will start at a <see cref="DefaultLength"/>;
        /// if they scale up, then this is the maximum they will reach.
        /// </summary>
        public int MaxLength { get; internal set; } = 320;

        /// <summary>
        /// The default length of stat bars, in pixels. When the bars are not set to a <see cref="DisplayMode"/> which scales,
        /// will stay at this length. If stat bars are set to have a set ratio of points per pixel, this setting will not be used.
        /// </summary>
        public int DefaultLength { get; internal set; } = 100;

        /// <summary>
        /// The number of stat points it takes to increase a stat bar's length by one pixel.
        /// This setting will only be used when set to a <see cref="DisplayMode"/> that scales this way.
        /// </summary>
        public double DefaultPointPerPixelRatio { get; internal set; } = 10;

        /// <summary>
        /// The number of seconds it takes for an inactive stat bar to hide. Greater than zero.
        /// </summary>
        public double SecondsToHide { get; internal set; } = 6;

        /// <summary>
        /// The percentage at which a stat bar will show, if the <see cref="FadingMode"/> will otherwise not show it outside of combat.
        /// </summary>
        public double AlertPercentage { get; internal set; } = .1;
    }
}
