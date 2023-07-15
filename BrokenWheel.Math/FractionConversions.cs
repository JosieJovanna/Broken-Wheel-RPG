using System;
using System.Globalization;
using BrokenWheel.Math.Options;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        /// <summary>
        /// The function returns the current Fraction object as double
        /// </summary>
        public double AsDouble()
        {
            if (Denominator != 0 || Option == ZeroDenominatorOption.Ignore)
                return (double)Numerator / Denominator;
            if (Option == ZeroDenominatorOption.GetValueAsZero)
                return 0;
            throw new FractionException("Cannot evaluate a fraction with zero denominator.");
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

        private static Fraction FromDoubleChecked(double value)
        {
            checked
            {
                return value % 1 == 0 // if whole number
                    ? new Fraction((long) value) 
                    : ToFractionFromFloatingPoint(value);
            }
        }

        private static Fraction ToFractionFromFloatingPoint(double value)
        {
            var tempValue = value;
            var multiple = 1L;
            var tempString = value.ToString(CultureInfo.InvariantCulture);

            ProcessWhole();
            ProcessDecimal();
            return new Fraction((int) System.Math.Round(tempValue), multiple);
            
            // LOCAL FX
            void ProcessWhole()
            {
                while (tempString.IndexOf("E", StringComparison.Ordinal) > 0) // if in the form like 12E-9
                {
                    tempValue *= 10;
                    multiple *= 10;
                    tempString = tempValue.ToString(CultureInfo.InvariantCulture);
                }
            }
            void ProcessDecimal()
            {
                var digitsAfterDecimal = tempString.Length - IndexOfDecimalPoint() - 1;
                while (digitsAfterDecimal > 0)
                {
                    tempValue *= 10;
                    multiple *= 10;
                    digitsAfterDecimal--;
                }
            }
            int IndexOfDecimalPoint()
            {
                var index = 0;
                while (tempString[index] != '.')
                    index++;
                return index;
            }
        }

        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456"
        /// </summary>
        public static Fraction FromString(string value)
        {
            var i = IndexOfSlash();
            if (i == value.Length) // if string is not in the form of a fraction
                return Convert.ToDouble(value);
            
            var numerator = Convert.ToInt64(value.Substring(0, i));
            var denominator = Convert.ToInt64(value.Substring(i + 1));
            return new Fraction(numerator, denominator);
            
            // LOCAL FX
            int IndexOfSlash()
            {
                int j;
                for (j = 0; j < value.Length; j++)
                    if (value[j] == '/')
                        break;
                return j;
            }
        }
    }
}
