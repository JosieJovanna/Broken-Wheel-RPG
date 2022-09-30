using System;

namespace LorendisCore.Utilities
{
    internal static class Validate
    {
        public static object ThrowIfNull(object value, string nameOfValue = "Value")
        {
            if (value == null)
                throw new ArgumentNullException($"{nameOfValue} cannot be null.");
            return value;
        }

        #region Strings
        public static string ThrowIfNullOrEmpty(string value, string nameOfValue = "String")
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameOfValue} cannot be null or empty.");
            return value;
        }
        
        public static string ThrowIfNullOrWhitespace(string value, string nameOfValue = "String")
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{nameOfValue} cannot be null or whitespace.");
            return value;
        }
        #endregion

        #region Non-Zero Values
        public static int ThrowIfZero(int value, string nameOfValue = "Integer") 
            => (int)ThrowIfZero((long)value, nameOfValue);
        public static long ThrowIfZero(long value, string nameOfValue = "Long")
        {
            if (value == 0)
                throw new ArgumentException($"{nameOfValue} cannot be zero.");
            return value;
        }
        
        public static float ThrowIfZero(float value, string nameOfValue = "Float") 
            => (float)ThrowIfZero((double)value, nameOfValue);
        public static double ThrowIfZero(double value, string nameOfValue = "Double")
        {
            if (double.IsNaN(value) || value == 0)
                throw new ArgumentException($"{nameOfValue} cannot be zero.");
            return value;
        }
        #endregion
    }
}