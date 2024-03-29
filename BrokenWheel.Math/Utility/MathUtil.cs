﻿namespace BrokenWheel.Math.Utility
{
    public static class MathUtil
    {
        /// <summary>
        /// Raises a negative value to zero.
        /// </summary>
        public static int NonNeg(int value) => System.Math.Max(0, value);

        /// <summary>
        /// Raises a negative value to zero.
        /// </summary>
        public static double NonNeg(double value) => System.Math.Max(0, value);


        /// <summary>
        /// Floors a double to the nearest int.
        /// </summary>
        public static int LowerDoubleToInt(double value) => (int)System.Math.Floor(value);

        /// <summary>
        /// Ceilings a double to the nearest int.
        /// </summary>
        public static int RaiseDoubleToInt(double value) => (int)System.Math.Ceiling(value);

        /// <summary>
        /// Returns a value between min and max, inclusive, rounded from value.
        /// </summary>
        public static int LimitValueToMinMax(int value, int min, int max)
        {
            var bottomTrimmed = System.Math.Max(value, min);
            var bothTrimmed = System.Math.Min(bottomTrimmed, max);
            return bothTrimmed;
        }
    }
}
