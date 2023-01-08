using System;

namespace LorendisCore.Utilities
{
    internal static class MathUtil
    {
        public static int NonNeg(int value) => Math.Max(0, value);
        public static double NonNeg(double value) => Math.Max(0, value);

        public static int FloorDouble(double value) => (int)Math.Floor(value);
    }
}