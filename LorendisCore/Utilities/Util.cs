using System;

namespace LorendisCore.Utilities
{
    internal static class Util
    {
        public static int NonNeg(int value) => Math.Max(0, value);
        public static double NonNeg(double value) => Math.Max(0, value);
    }
}