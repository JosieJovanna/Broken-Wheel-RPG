using System.Collections.Generic;
using System.Drawing;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.UI.Settings.StatBar.Enum;

namespace BrokenWheel.UI.Settings.StatBar
{
    public class StatBarSettings : ISettings
    {
        /// <summary>
        /// Which corner the stat bars are displayed in.
        /// </summary>
        public StatBarCorner DisplayCorner { get; set; } = default;

        /// <summary>
        /// The way the stat bars display, relative to each other.
        /// </summary>
        public StatBarDisplayMode DisplayMode { get; set; } = default;

        /// <summary>
        /// The conditions under which stat bars are hidden or shown.
        /// </summary>
        public StatBarFading FadingMode { get; set; } = default;

        /// <summary>
        /// The order of the main health, stamina, and willpower stats.
        /// </summary>
        public IList<StatBarByStatSettings> MainStatsInOrder { get; set; } = new StatBarByStatSettings[]
        {
            new StatBarByStatSettings
            {
                Stat = Stat.HP,
                ColorSettings = new StatBarColorSettings
                {
                    BorderColor = Color.White,
                    BackgroundColor = Color.Black,
                    PrimaryColor = Color.Red,
                    SecondaryColor = Color.Coral,
                    ExhaustionColor = Color.DarkRed
                }
            },
            new StatBarByStatSettings
            {
                Stat = Stat.SP,
                ColorSettings = new StatBarColorSettings
                {
                    BorderColor = Color.White,
                    BackgroundColor = Color.Black,
                    PrimaryColor = Color.LimeGreen,
                    SecondaryColor = Color.GreenYellow,
                    ExhaustionColor = Color.DarkGreen
                }
            },
            new StatBarByStatSettings
            {
                Stat = Stat.WP,
                ColorSettings = new StatBarColorSettings
                {
                    BorderColor = Color.White,
                    BackgroundColor = Color.Black,
                    PrimaryColor = Color.RoyalBlue,
                    SecondaryColor = Color.Cyan,
                    ExhaustionColor = Color.DarkBlue
                }
            },
        };

        /// <summary>
        /// Color profiles for any non-standard stats being tracked.
        /// </summary>
        public IDictionary<string, StatBarColorSettings> ColorsByCode { get; set; } =
            new Dictionary<string, StatBarColorSettings>();

        /// <summary>
        /// The default color profile for any other stat added not included in <see cref="ColorsByCode"/>.
        /// </summary>
        public StatBarColorSettings DefaultColors { get; set; } = new StatBarColorSettings
        {
            BorderColor = Color.White,
            BackgroundColor = Color.Black,
            PrimaryColor = Color.DarkGoldenrod,
            SecondaryColor = Color.Gold,
            ExhaustionColor = Color.SaddleBrown
        };

        /// <summary>
        /// Whether the stat bars should be displayed vertically, rather than horizontally.
        /// </summary>
        public bool IsVertical { get; set; } = true;

        /// <summary>
        /// The horizontal distance from the side of the screen, left or right.
        /// Between zero and the width of the display; if outside this range, rounds to be within it.
        /// </summary>
        public int PixelsFromVerticalEdge { get; set; } = 2;

        /// <summary>
        /// The vertical distance from the side of the screen, bottom or top.
        /// Between zero and the height of the display; if outside this range, rounds to be within it.
        /// </summary>
        public int PixelsFromHorizontalEdge { get; set; } = 2;

        /// <summary>
        /// The spacing between stat bars, in pixels.
        /// </summary>
        public int Spacing { get; set; } = 2;

        /// <summary>
        /// The size of the border around the stat bars, in pixels. Should not be set too large.
        /// </summary>
        public int BorderSize { get; set; } = 1;

        /// <summary>
        /// The thickness, in pixels, of all of the stat bars.
        /// </summary>
        public int Thickness { get; set; } = 4;

        /// <summary>
        /// The maximum length of the stat bars, in pixels. Typically, the stat bars will start at a <see cref="DefaultLength"/>;
        /// if they scale up, then this is the maximum they will reach.
        /// </summary>
        public int MaxLength { get; set; } = 320;

        /// <summary>
        /// When a simple stat is being tracked, most have some hard limit.
        /// When they do not, and even when they do, a stat bar is unlikely to be necessary.
        /// However, rather than checking if a stat has a maximum, it will assume one, in hopes of making the display
        /// at least somewhat readable.
        /// </summary>
        public int MaximumWhenSimpleStatHasNone { get; set; } = 100;

        /// <summary>
        /// The default length of stat bars, in pixels. When the bars are not set to a <see cref="DisplayMode"/> which scales,
        /// will stay at this length. If stat bars are set to have a set ratio of points per pixel, this setting will not be used.
        /// </summary>
        public int DefaultLength { get; set; } = 100;

        /// <summary>
        /// The number of stat points it takes to increase a stat bar's length by one pixel.
        /// This setting will only be used when set to a <see cref="DisplayMode"/> that scales this way.
        /// </summary>
        public double DefaultPointPerPixelRatio { get; set; } = 10;

        /// <summary>
        /// The number of seconds it takes for an inactive stat bar to hide. Greater than zero.
        /// </summary>
        public double SecondsToHide { get; set; } = 6;

        /// <summary>
        /// The percentage at which a stat bar will show, if the <see cref="FadingMode"/> will otherwise not show it outside of combat.
        /// </summary>
        public double AlertPercentage { get; set; } = .1;
    }
}
