namespace BrokenWheel.Math.Utility
{
    public static class MathUtil
    {
        public static int NonNeg(int value) => System.Math.Max(0, value);
        public static double NonNeg(double value) => System.Math.Max(0, value);

        public static int FloorDouble(double value) => (int)System.Math.Floor(value);
    }
}
