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

        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456"
        /// </summary>
        public static Fraction ToFraction(string strValue)
        {
            var i = IndexOfSlash();
            if (i == strValue.Length) // if string is not in the form of a fraction
                return Convert.ToDouble(strValue);
            
            var iNumerator = Convert.ToInt64(strValue.Substring(0, i));
            var iDenominator = Convert.ToInt64(strValue.Substring(i + 1));
            return new Fraction(iNumerator, iDenominator);
            
            // LOCAL FX
            int IndexOfSlash()
            {
                int i1;
                for (i1 = 0; i1 < strValue.Length; i1++)
                    if (strValue[i1] == '/')
                        break;
                return i1;
            }
        }


        /// <summary>
        /// The function takes a floating point number as an argument 
        /// and returns its corresponding reduced fraction
        /// </summary>
        public static Fraction ToFraction(double dValue)
        {
            try
            {
                return ToFractionChecked(dValue);
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

        private static Fraction ToFractionChecked(double dValue)
        {
            checked
            {
                return dValue % 1 == 0 // if whole number
                    ? new Fraction((long) dValue) 
                    : ToFractionFromFloatingPoint(dValue);
            }
        }

        private static Fraction ToFractionFromFloatingPoint(double dValue)
        {
            var dTemp = dValue;
            var iMultiple = 1L;
            var strTemp = dValue.ToString(CultureInfo.InvariantCulture);

            ProcessWhole();
            ProcessDecimal();
            return new Fraction((int) System.Math.Round(dTemp), iMultiple);
            
            // LOCAL FX
            void ProcessWhole()
            {
                while (strTemp.IndexOf("E", StringComparison.Ordinal) > 0) // if in the form like 12E-9
                {
                    dTemp *= 10;
                    iMultiple *= 10;
                    strTemp = dTemp.ToString(CultureInfo.InvariantCulture);
                }
            }
            void ProcessDecimal()
            {
                var iDigitsAfterDecimal = strTemp.Length - IndexOfDecimalPoint() - 1;
                while (iDigitsAfterDecimal > 0)
                {
                    dTemp *= 10;
                    iMultiple *= 10;
                    iDigitsAfterDecimal--;
                }
            }
            int IndexOfDecimalPoint()
            {
                var idx = 0;
                while (strTemp[idx] != '.')
                    idx++;
                return idx;
            }
        }
    }
}

/*
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
        public double ToDouble()
        {
            if (Denominator != 0 || Option == ZeroDenominatorOption.Ignore)
                return (double)Numerator / Denominator;
            if (Option == ZeroDenominatorOption.GetValueAsZero)
                return 0;
            throw new FractionException("Cannot evaluate a fraction with zero denominator.");
        }

        public static bool TryFromString(string value, out Fraction fraction, ZeroDenominatorOption option = default)
        {
            try
            {
                fraction = FromString(value, option);
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
        public static Fraction FromString(string value, ZeroDenominatorOption option = default)
        {
            var barIndex = IndexOfFractionBar(value);
            if (barIndex == value.Length) // if string is not in the form of a fraction
                return Convert.ToDouble(value);

            var iNumerator = Convert.ToInt64(value.Substring(0, barIndex));
            var iDenominator = Convert.ToInt64(value.Substring(barIndex + 1));
            return new Fraction(iNumerator, iDenominator, option);
        }

        private static int IndexOfFractionBar(string value)
        {
            int index;
            for (index = 0; index < value.Length; index++)
                if (value[index] == '/')
                    break;
            return index;
        }

        public static bool TryFromDouble(double value, out Fraction fraction, ZeroDenominatorOption option = default)
        {
            try
            {
                fraction = FromDouble(value, option);
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
        public static Fraction FromDouble(double value, ZeroDenominatorOption option = default)
        {
            try
            {
                return FromDoubleChecked(value, option);
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

        public static Fraction FromDoubleChecked(double value, ZeroDenominatorOption option = default)
        {
            checked
            {
                return value % 1 == 0 // if whole number
                    ? new Fraction((long)value)
                    : FromFloatingPoint(value, option);
            }
        }

        public static Fraction FromFloatingPoint(double floatingPoint, ZeroDenominatorOption option = default)
        {
            var temporaryNumerator = floatingPoint;
            var temporaryDenominator = 1L;
            var temporaryString = floatingPoint.ToString(CultureInfo.InvariantCulture);

            ProcessWhole(ref temporaryString, ref temporaryNumerator, ref temporaryDenominator);
            ProcessDecimal(ref temporaryString, ref temporaryNumerator, ref temporaryDenominator);
            return new Fraction((int)System.Math.Round(temporaryNumerator), temporaryDenominator, option);
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
*/
