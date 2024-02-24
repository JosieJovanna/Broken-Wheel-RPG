namespace BrokenWheel.Math.Utility
{
    public static class AngleUtil
    {
        private const double PI = System.Math.PI;

        public static float DegreesToRadians(float degrees)
        {
            var radians = degrees * PI / 180;
            return (float)radians;
        }
    }
}
