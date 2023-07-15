using System;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats;

namespace BrokenWheel.UI.StatBar.Implementation
{
    /// <summary>
    /// Stores several properties from settings to avoid race conditions and simplify method signatures.
    /// </summary>
    internal class UpdateDisplayParameters
    {
        public IComplexStatistic Stat { get; }
        public bool IsVertical { get; }
        public double PPP { get; }
        public int BorderSize { get; }
        public int Thickness { get; }
        public int FullLength { get; }
        public int BaseX { get; }
        public int BaseY { get; }
        public int BackgroundX { get; }
        public int BackgroundY { get; }
        
        public UpdateDisplayParameters(
            StatBarSettings settings, 
            IComplexStatistic stat, 
            double ppp, 
            int length, 
            int x, 
            int y)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
            
            Stat = stat ?? throw new ArgumentNullException(nameof(stat));
            IsVertical = settings.IsVertical;
            BorderSize = settings.BorderSize;
            Thickness = settings.Thickness;
            PPP = ppp;
            FullLength = length;
            BaseX = x;
            BaseY = y;
            BackgroundX = x + BorderSize;
            BackgroundY = y + BorderSize;
        }
    }
}
