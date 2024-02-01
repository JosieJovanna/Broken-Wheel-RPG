using System;
using System.Collections.Generic;
using System.Linq;

namespace BrokenWheel.Core.Utilities
{
    internal static class Validate
    {
        public static object ThrowIfNull(object value, string nameOfValue = "Value")
        {
            return value ?? throw new ArgumentNullException($"{nameOfValue} cannot be null.");
        }

        public static IEnumerable<object> ThrowIfEmpty(IEnumerable<object> value, string nameOfValue = "Value")
        {
            if (value == null || !value.Any())
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

        #region Primitive Values
        // ZEROES
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

        // NEGATIVES
        public static int ThrowIfNegative(int value, string nameOfValue = "Integer")
            => (int)ThrowIfNegative((long)value);
        public static long ThrowIfNegative(long value, string nameOfValue = "Long")
        {
            if (value < 0)
                throw new ArgumentException($"{nameOfValue} cannot be less than zero.");
            return value;
        }

        public static float ThrowIfNegative(float value, string nameOfValue = "Float")
            => (float)ThrowIfNegative((double)value);
        public static double ThrowIfNegative(double value, string nameOfValue = "Double")
        {
            if (value < 0)
                throw new ArgumentException($"{nameOfValue} cannot be less than zero.");
            return value;
        }

        // NON-POSITIVES
        public static int ThrowIfNotPositive(int value, string nameOfValue = "Integer")
            => (int)ThrowIfNotPositive((long)value);
        public static long ThrowIfNotPositive(long value, string nameOfValue = "Long")
        {
            if (value <= 0)
                throw new ArgumentException($"{nameOfValue} cannot be zero or less than zero.");
            return value;
        }

        public static float ThrowIfNotPositive(float value, string nameOfValue = "Float")
            => (float)ThrowIfNotPositive((double)value);
        public static double ThrowIfNotPositive(double value, string nameOfValue = "Double")
        {
            if (value <= 0)
                throw new ArgumentException($"{nameOfValue} cannot be zero or less than zero.");
            return value;
        }
        #endregion
    }
}
