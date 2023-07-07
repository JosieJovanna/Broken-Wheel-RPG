using System;
using System.Globalization;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        /// <summary>
        /// The function returns the current Fraction object as double
        /// </summary>
        public double ToDouble()
        {
            return (double) Numerator / Denominator;
        }

        public static bool TryFromString(string value, out Fraction fraction)
        {
            try
            {
                fraction = FromString(value);
                return true;
            }
            catch (Exception)
            {
                fraction = null;
                return false;
            }
        }
        
        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456".
        /// </summary>
        public static Fraction FromString(string value)
        {
            var barIndex = IndexOfFractionBar(value);
            if (barIndex == value.Length) // if string is not in the form of a fraction
                return Convert.ToDouble(value);
            
            var iNumerator = Convert.ToInt64(value.Substring(0, barIndex));
            var iDenominator = Convert.ToInt64(value.Substring(barIndex + 1));
            return new Fraction(iNumerator, iDenominator);
        }
        
        private static int IndexOfFractionBar(string value)
        {
            int index;
            for (index = 0; index < value.Length; index++)
                if (value[index] == '/')
                    break;
            return index;
        }

        public static bool TryFromDouble(double value, out Fraction fraction)
        {
            try
            {
                fraction = FromDouble(value);
                return true;
            }
            catch (Exception)
            {
                fraction = null;
                return false;
            }
        }
        
        /// <summary>
        /// The function takes a floating point number as an argument 
        /// and returns its corresponding reduced fraction
        /// </summary>
        public static Fraction FromDouble(double value)
        {
            try
            {
                return FromDoubleChecked(value);
            }
            catch (OverflowException ex)
            {
                throw new FractionException("Conversion not possible due to overflow", ex);
            }
            catch (Exception ex)
            {
                throw new FractionException("Conversion not possible", ex);
            }
        }

        public static Fraction FromDoubleChecked(double value)
        {
            checked
            {
                return value % 1 == 0 // if whole number
                    ? new Fraction((long) value) 
                    : FromFloatingPoint(value);
            }
        }

        public static Fraction FromFloatingPoint(double floatingPoint)
        {
            var temporaryNumerator = floatingPoint;
            var temporaryDenominator = 1L;
            var temporaryString = floatingPoint.ToString(CultureInfo.InvariantCulture);

            ProcessWhole(ref temporaryString, ref temporaryNumerator, ref temporaryDenominator);
            ProcessDecimal(ref temporaryString, ref temporaryNumerator, ref temporaryDenominator);
            return new Fraction((int) System.Math.Round(temporaryNumerator), temporaryDenominator);
        }
        
        private static void ProcessWhole(ref string tempString, ref double tempNumerator, ref long tempDenominator)
        {
            while (tempString.IndexOf("E", StringComparison.Ordinal) > 0) // if in the form of ###E-###
            {
                tempNumerator *= 10;
                tempDenominator *= 10;
                tempString = tempNumerator.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        private static void ProcessDecimal(ref string tempString, ref double tempNumerator, ref long tempDenominator)
        {
            var numberOfDecimals = AmountOfNumbersAfterDecimalPoint(tempString);
            var multiplier = System.Math.Pow(10, numberOfDecimals);
            tempNumerator *= multiplier;
            tempDenominator *= (long)multiplier;
        }
        
        private static int AmountOfNumbersAfterDecimalPoint(string value)
        {
            var index = 0;
            while (value[index] != '.')
                index++;
            return value.Length - index - 1;
        }
    }
}
