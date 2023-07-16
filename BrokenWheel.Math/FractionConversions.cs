using System;
using System.Linq;
using BrokenWheel.Math.Options;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        private const double DEFAULT_ACCURACY = 0.00000000001;
        
        /// <summary>
        /// The function takes a floating point number as an argument 
        /// and returns its corresponding reduced fraction
        /// </summary>
        internal static Fraction FromDouble(double value, double accuracy = DEFAULT_ACCURACY)
        {
            try
            {
                return FromDoubleChecked();
            }
            catch (OverflowException ex)
            {
                throw new FractionException("Conversion not possible due to overflow", ex);
            }
            catch (Exception ex)
            {
                throw new FractionException("Conversion not possible", ex);
            }
            
            // LOCAL FX
            Fraction FromDoubleChecked()
            {
                if (double.IsNaN(value))
                    throw new ArgumentException($"{nameof(value)} must be a number.");
                if (double.IsInfinity(value))
                    throw new ArgumentException($"{nameof(value)} must be a finite number.");
                checked
                {
                    return value % 1 == 0 // if whole number
                        ? new Fraction((long) value) 
                        : ToFractionFromFloatingPoint();
                }
            }
            Fraction ToFractionFromFloatingPoint()
            {
                /* Algorithm from https://stackoverflow.com/a/32903747 */
                if (accuracy <= 0.0 || accuracy >= 1.0)
                    throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be > 0 and < 1.");

                var sign = System.Math.Sign(value);
                if (sign == -1)
                    value = System.Math.Abs(value);

                // Accuracy is the maximum relative error; convert to absolute maxError
                var maxError = sign == 0 ? accuracy : value * accuracy;

                var n = (int) System.Math.Floor(value);
                value -= n;

                if (value < maxError)
                    return new Fraction(sign * n, 1);
                if (1 - maxError < value)
                    return new Fraction(sign * (n + 1), 1);

                var lowerNumerator = 0;
                var lowerDenominator = 1;

                var upperNumerator = 1;
                var uppderDenominator = 1;

                while (true)
                {
                    var MiddleNumerator = lowerNumerator + upperNumerator;
                    var middleDenominator = lowerDenominator + uppderDenominator;

                    if (middleDenominator * (value + maxError) < MiddleNumerator)
                    {
                        upperNumerator = MiddleNumerator;
                        uppderDenominator = middleDenominator;
                    }
                    else if (MiddleNumerator < (value - maxError) * middleDenominator)
                    { 
                        lowerNumerator = MiddleNumerator;
                        lowerDenominator = middleDenominator;
                    }
                    else // middle is best fraction
                    {
                        return new Fraction((n * middleDenominator + MiddleNumerator) * sign, middleDenominator);
                    }
                }
            }
        }

        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456"
        /// </summary>
        internal static Fraction FromString(string value)
        {
            var i = IndexOfSlash();
            try
            {
                CheckStringFormat();
                return ConvertFromString();
            }
            catch (FractionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FractionException($"Failed to convert from string '{value}'.", ex);
            }

            // LOCAL FX
            int IndexOfSlash()
            {
                int j;
                for (j = 0; j < value.Length; j++)
                    if (value[j] == '/')
                        break;
                return j;
            }
            void CheckStringFormat()
            {
                if (i == 0 || i == value.Length - 1)
                    throw new FractionException(
                        $"Could not convert string '{value}' - slash cannot be first or last character.");
                if (value.Count(_ => _ == '/') > 1)
                    throw new FractionException(
                        $"Could not convert string '{value}' - cannot have more than one slash.");

            }
            Fraction ConvertFromString()
            {
                if (i == value.Length) // if string is not in the form of a fraction
                    return FromDouble(Convert.ToDouble(SanitizeString(value)));

                var numerator = ConvertSubStringToLong(0, i);
                var denominator = ConvertSubStringToLong(i + 1);
                return new Fraction(numerator, denominator, ZeroDenominatorOption.Ignore);
            }
            long ConvertSubStringToLong(int startIndex, int? length = null)
            {
                var substring = length == null 
                    ? value.Substring(startIndex) 
                    : value.Substring(startIndex, length.Value);
                return Convert.ToInt64(SanitizeString(substring));
            }
            string SanitizeString(string toSanitize)
            {
                return toSanitize
                    .Replace("_", "")
                    .Replace(",", "");
            }
        }
    }
}
