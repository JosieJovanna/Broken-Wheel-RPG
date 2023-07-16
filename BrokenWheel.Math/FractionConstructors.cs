using System;
using System.Linq;
using BrokenWheel.Math.Options;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        private const double DEFAULT_ACCURACY = 0.00000000001; // between zero and one

        /// <summary>
        /// Creates a Fraction with a numerator of zero and a denominator of one.
        /// </summary>
        public Fraction(ZeroDenominatorOption option = default)
        {
            Initialize(0, 1, option);
        }

        /// <summary>
        /// Creates a fraction with a set numerator and denominator.
        /// </summary>
        /// <exception cref="FractionException">
        /// When denominator is zero and the option does not ignore setting zeroes.
        /// </exception>
        public Fraction(long numerator, long denominator, ZeroDenominatorOption option = default)
        {
            Initialize(numerator, denominator, option);
        }

        /// <summary>
        /// Creates a fraction with a given whole value over one.
        /// </summary>
        public Fraction(long wholeNumber, ZeroDenominatorOption option = default)
        {
            Initialize(wholeNumber, 1, option);
        }

        /// <summary>
        /// Parses a double into the nearest fraction it can, though not with perfect accuracy.
        /// Common irrational numbers like ".333...", ".166..." will have the expected fractions.
        /// </summary>
        /// <exception cref="FractionException">
        /// When NaN or infinity, or denominator is zero and the option does not ignore setting zeroes.
        /// </exception>
        public Fraction(double floatingValue, ZeroDenominatorOption option = default)
        {
            var temp = FromDouble(floatingValue);
            Initialize(temp.Numerator, temp.Denominator, option);
            Reduce();
        }

        /// <summary>
        /// Formats a string into a Fraction, with several possible formats.
        /// All numbers can have commas or underscore inserted; they will be ignored.
        /// <para> Fraction: "#/#" - with at least one number on each side, and no letters. </para>
        /// <para> Whole number: "###" - just a number as a string, over one.  </para>
        /// <para> Decimal number: "#.#" - any floating point number, with a '.' as the decimal point (not sorry, british 'people'). </para>
        /// </summary>
        /// <exception cref="FractionException">
        /// When string format is incorrect, or denominator is zero unless set to ignore setting zeroes.
        /// </exception>
        public Fraction(string valueAsString, ZeroDenominatorOption option = default)
        {
            var temp = FromString(valueAsString);
            Initialize(temp.Numerator, temp.Denominator, option);
        }
        
        private void Initialize(long numerator, long denominator, ZeroDenominatorOption zeroDenominatorOption = default)
        {
            Option = zeroDenominatorOption; // set first so that setting denominator behaves correctly
            Numerator = numerator;
            Denominator = denominator;
        }
        
        /// <summary>
        /// Attempts to find a sufficiently similar fraction to the given double.
        /// <remarks> Algorithm edited from: https://stackoverflow.com/a/32903747. </remarks>
        /// </summary>
        /// <param name="value"> The double being converted. </param>
        /// <param name="accuracy"> How close a fraction must be to the value to be accepted as equivalent. </param>
        /// <returns> A fraction representing the double, preserving sign. </returns>
        /// <exception cref="FractionException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Fraction FromDouble(double value, double accuracy = DEFAULT_ACCURACY)
        {
            ValidateParameters();
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
            void ValidateParameters()
            {
                if (accuracy <= 0.0 || accuracy >= 1.0)
                    throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be > 0 and < 1.");
                if (double.IsNaN(value))
                    throw new ArgumentException($"{nameof(value)} must be a number.");
                if (double.IsInfinity(value))
                    throw new ArgumentException($"{nameof(value)} must be a finite number.");
            }
            Fraction FromDoubleChecked()
            {
                checked
                {
                    return value % 1 == 0 // if whole number
                        ? new Fraction((long) value) 
                        : ToFractionFromFloatingPoint();
                }
            }
            Fraction ToFractionFromFloatingPoint()
            {
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

                var (lowerNumerator, lowerDenominator) = (0, 1);
                var (upperNumerator, upperDenominator) = (1, 1);

                while (true)
                {
                    var middleNumerator = lowerNumerator + upperNumerator;
                    var middleDenominator = lowerDenominator + upperDenominator;

                    if (middleDenominator * (value + maxError) < middleNumerator)
                        (upperNumerator, upperDenominator) = (middleNumerator, middleDenominator);
                    else if (middleNumerator < (value - maxError) * middleDenominator)
                        (lowerNumerator, lowerDenominator) = (middleNumerator, middleDenominator);
                    else // middle is best fraction
                        return new Fraction((n * middleDenominator + middleNumerator) * sign, middleDenominator);
                }
            }
        }
  
        /// <summary>
        /// Formats a string into a Fraction, with several possible formats.
        /// All numbers can have commas or underscore inserted; they will be ignored.
        /// <para> Fraction: "#/#" - with at least one number on each side, and no letters. </para>
        /// <para> Whole number: "###" - just a number as a string, over one.  </para>
        /// <para> Decimal number: "#.#" - any floating point number, with a '.' as the decimal point (not sorry, british 'people'). </para>
        /// </summary>
        /// <exception cref="FractionException">
        /// When string format is incorrect, or denominator is zero unless set to ignore setting zeroes.
        /// </exception>
        public static Fraction FromString(string value)
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
