namespace BrokenWheel.Math.Utility
{
    public static class MathUtil
    {
        public static int NonNeg(int value) => System.Math.Max(0, value);
        public static double NonNeg(double value) => System.Math.Max(0, value);

        public static int LowerDoubleToInt(double value) => (int)System.Math.Floor(value);
        public static int RaiseDoubleToInt(double value) => (int)System.Math.Ceiling(value);
    }
}
