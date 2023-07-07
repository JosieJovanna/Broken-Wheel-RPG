using System;
using System.Globalization;

namespace BrokenWheel.Core
{
    /// <summary>
    /// Exception class for Fraction, derived from System.Exception
    /// </summary>
    public class FractionException : Exception
    {
        public FractionException()
        { }

        public FractionException(string message) 
            : base(message) { }

        public FractionException(string message, Exception innerException) 
            : base(message, innerException) { }
    }

    /*
     * Author: Syed Mehroz Alam
     * Email: smehrozalam@yahoo.com
     * URL: Programming Home "http://www.geocities.com/smehrozalam/" 
     * Date: 6/15/2004
     * Time: 10:54 AM
     * Version: 2.0
     * 
     * What's new in version 2.0:
     *     Changed Numerator and Denominator from Int32(integer) to Int64(long) for increased range
     *     renamed ConvertToString() to (overloaded) ToString()
     *     added the capability of detecting/raising overflow exceptions
     *     Fixed the bug that very small numbers e.g. 0.00000001 could not be converted to fraction
     *     Other minor bugs fixed
     * 
     * What's new in version 2.1
     *     overloaded user-defined conversions to/from Fractions
     * 	
     * 
     * Properties:
     * 	   Numerator: Set/Get value for Numerator
     * 	   Denominator:  Set/Get value for Numerator
     * 	   Value:  Set an integer value for the fraction
     * 
     * Constructors:
     *     no arguments: initializes fraction as 0/1
     *     (Numerator, Denominator): initializes fraction with the given numerator and denominator values
     *     (integer):	 initializes fraction with the given integer value
     *     (long):	     initializes fraction with the given long value
     *     (double):	 initializes fraction with the given double value
     *     (string):	 initializes fraction with the given string value
     *     			     the string can be an in the form of and integer, double or fraction.
     *     			     e.g it can be like "123" or "123.321" or "123/456"
     * 
     * Public Methods (Description is given with respective methods' definitions)
     * 	   (override) string ToString(Fraction)
     * 	   Fraction ToFraction(string)
     * 	   Fraction ToFraction(double)
     * 	   double ToDouble(Fraction)
     * 	   Fraction Duplicate()
     * 	   Fraction Inverse(integer)
     * 	   Fraction Inverse(Fraction)
     * 	   ReduceFraction(Fraction)
     * 	   Equals(object)
     * 	   GetHashCode()
     *    
     * Private Methods (Description is given with respective methods' definitions)
     * 	   Initialize(Numerator, Denominator)
     * 	   Fraction Negate(Fraction)
     * 	   Fraction Add(Fraction1, Fraction2)
     * 
     * Overloaded Operators (overloaded for Fractions, Integers and Doubles)
     * 	   Unary: -
     * 	   Binary: +, -, *, / 
     * 	   Relational and Logical Operators: ==, !=, <, >, <=, >=
     * 
     * Overloaded user-defined conversions
     * 	   Implicit: From double/long/string to Fraction
     * 	   Explicit: From Fraction to double/string
     */
    public sealed class Fraction
    {
        private long _denominator;
        
        public long Numerator { get; set; }
        
        public long Denominator
        {
            get => _denominator;
            set => _denominator = FilterZeroValues(value);
        }

        #region Constructors
        public Fraction() 
            => Initialize(0, 1);

        public Fraction(long numerator, long denominator) 
            => Initialize(numerator, denominator);

        public Fraction(long wholeNumber) 
            => Initialize(wholeNumber, 1);

        public Fraction(double decimalValue)
        {
            var temp = ToFraction(decimalValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        public Fraction(string valueAsString)
        {
            var temp = ToFraction(valueAsString);
            Initialize(temp.Numerator, temp.Denominator);
        }

        private void Initialize(long numerator, long enominator)
        {
            Numerator = numerator;
            Denominator = enominator;
            ReduceFraction(this);
        }
        #endregion

        /// <summary>
        /// Creates a new fraction with the exact same numerator and denominator.
        /// </summary>
        public Fraction Duplicate()
        {
            var frac = new Fraction
            {
                Numerator = Numerator,
                Denominator = Denominator
            };
            return frac;
        }

        /// <summary>
        /// Swaps the numerator and denominator of the fraction.
        /// </summary>
        public static Fraction Inverse(Fraction fraction)
        {
            var iNumerator = fraction.Denominator;
            var iDenominator = fraction.Numerator;
            return new Fraction(iNumerator, iDenominator);
        }

        /// <summary>
        /// The function reduces (simplifies) a Fraction object by dividing both its
        /// numerator and denominator by their greatest common denominator.
        /// Actual value remains the same.
        /// </summary>
        public static void ReduceFraction(Fraction fraction)
        {
            try
            {
                AttemptToReduceFraction();
            }
            catch (Exception ex)
            {
                throw new FractionException("Cannot reduce Fraction: " + ex.Message);
            }

            // LOCAL FX
            void AttemptToReduceFraction()
            {
                if (fraction.Numerator == 0)
                {
                    fraction.Denominator = 1;
                    return;
                }
                ReduceByGcd();
            }
            void ReduceByGcd()
            {
                var greatestCommonDenominator = GreatestCommonDenominator(fraction.Numerator, fraction.Denominator);
                fraction.Numerator /= greatestCommonDenominator;
                fraction.Denominator /= greatestCommonDenominator;
                MakeNumeratorNegativeInsteadOfTheDenominator();
            }
            void MakeNumeratorNegativeInsteadOfTheDenominator()
            {
                if (fraction.Denominator >= 0) 
                    return;
                fraction.Numerator *= -1;
                fraction.Denominator *= -1;
            }
        }

        /// <summary>
        /// The function returns Greatest Common Denominator of two numbers (used for reducing a Fraction).
        /// </summary>
        private static long GreatestCommonDenominator(long value1, long value2)
        {
            if (value1 < 0) 
                value1 = -value1;
            if (value2 < 0) 
                value2 = -value2;
            CalculateGcd();
            return value2;
            
            // LOCAL FX
            void CalculateGcd()
            {
                do
                {
                    if (value1 < value2)
                        (value1, value2) = (value2, value1);
                    value1 %= value2;
                } while (value1 != 0);
            }
        }

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

        private static Fraction ToFractionFromFloatingPoint(double floatingPoint)
        {
            var temporaryDouble = floatingPoint;
            var multiplier = 1L;
            var temporaryString = floatingPoint.ToString(CultureInfo.InvariantCulture);

            ProcessWhole();
            ProcessDecimal();
            return new Fraction((int) Math.Round(temporaryDouble), multiplier);
            
            // LOCAL FX
            void ProcessWhole()
            {
                while (temporaryString.IndexOf("E", StringComparison.Ordinal) > 0) // if in the form like 12E-9
                {
                    temporaryDouble *= 10;
                    multiplier *= 10;
                    temporaryString = temporaryDouble.ToString(CultureInfo.InvariantCulture);
                }
            }
            void ProcessDecimal()
            {
                var decimalsAsInt = temporaryString.Length - IndexOfDecimalPoint() - 1;
                while (decimalsAsInt > 0)
                {
                    temporaryDouble *= 10;
                    multiplier *= 10;
                    decimalsAsInt--;
                }
            }
            int IndexOfDecimalPoint()
            {
                var index = 0;
                while (temporaryString[index] != '.')
                    index++;
                return index;
            }
        }

        #region Static Math Methods
        private static Fraction Negate(Fraction frac1)
        {
            var iNumerator = -frac1.Numerator;
            var iDenominator = frac1.Denominator;
            return new Fraction(iNumerator, iDenominator);
        }

        private static Fraction TryAdd(Fraction frac1, Fraction frac2)
        {
            try
            {
                return Add(frac1, frac2);
            }
            catch (OverflowException ex)
            {
                throw new FractionException("Overflow occurred while performing arithmetic operation", ex);
            }
            catch (Exception ex)
            {
                throw new FractionException("An error occurred while performing arithmetic operation", ex);
            }
        }

        private static Fraction Add(Fraction frac1, Fraction frac2)
        {
            checked
            {
                var iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
                var iDenominator = frac1.Denominator * frac2.Denominator;
                return new Fraction(iNumerator, iDenominator);
            }
        }

        private static Fraction TryMultiply(Fraction frac1, Fraction frac2)
        {
            try
            {
                return Multiply(frac1, frac2);
            }
            catch (OverflowException ex)
            {
                throw new FractionException("Overflow occurred while performing arithmetic operation", ex);
            }
            catch (Exception ex)
            {
                throw new FractionException("An error occurred while performing arithmetic operation", ex);
            }
        }

        private static Fraction Multiply(Fraction frac1, Fraction frac2)
        {
            checked
            {
                var iNumerator = frac1.Numerator * frac2.Numerator;
                var iDenominator = frac1.Denominator * frac2.Denominator;
                return new Fraction(iNumerator, iDenominator);
            }
        }

        #endregion

        #region Object Overrides
        /// <summary>
        /// The function returns the current Fraction object as a string
        /// </summary>
        public override string ToString()
        {
            string str;
            if (Denominator == 1)
                str = Numerator.ToString();
            else
                str = Numerator + "/" + Denominator;
            return str;
        }

        /// <summary>
        /// Checks whether two fractions are equal
        /// </summary>
        public override bool Equals(object value)
        {
            var fraction = (Fraction) value;
            return fraction != null && Numerator == fraction.Numerator && Denominator == fraction.Denominator;
        }

        /// <summary>
        /// Returns a hash code for this fraction
        /// </summary>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF);
        }
        #endregion

        // Operators for the Fraction object
        // includes -(unary), and binary operators such as +, -, *, /
        // also includes relational and logical operators such as ==, !=, <, >, <=, >=
        #region Operators
        public static Fraction operator -(Fraction frac1) => Negate(frac1);
        public static Fraction operator +(Fraction frac1, Fraction frac2) => TryAdd(frac1, frac2);
        public static Fraction operator +(int iNo, Fraction frac1) => TryAdd(frac1, new Fraction(iNo));
        public static Fraction operator +(Fraction frac1, int iNo) => TryAdd(frac1, new Fraction(iNo));
        public static Fraction operator +(double dbl, Fraction frac1) => TryAdd(frac1, ToFraction(dbl));
        public static Fraction operator +(Fraction frac1, double dbl) => TryAdd(frac1, ToFraction(dbl));
        public static Fraction operator -(Fraction frac1, Fraction frac2) => TryAdd(frac1, -frac2);
        public static Fraction operator -(int iNo, Fraction frac1) => TryAdd(-frac1, new Fraction(iNo));
        public static Fraction operator -(Fraction frac1, int iNo) => TryAdd(frac1, -new Fraction(iNo));
        public static Fraction operator -(double dbl, Fraction frac1) => TryAdd(-frac1, ToFraction(dbl));
        public static Fraction operator -(Fraction frac1, double dbl) => TryAdd(frac1, -ToFraction(dbl));
        public static Fraction operator *(Fraction frac1, Fraction frac2) => TryMultiply(frac1, frac2);
        public static Fraction operator *(int iNo, Fraction frac1) => TryMultiply(frac1, new Fraction(iNo));
        public static Fraction operator *(Fraction frac1, int iNo) => TryMultiply(frac1, new Fraction(iNo));
        public static Fraction operator *(double dbl, Fraction frac1) => TryMultiply(frac1, ToFraction(dbl));
        public static Fraction operator *(Fraction frac1, double dbl) => TryMultiply(frac1, ToFraction(dbl));
        public static Fraction operator /(Fraction frac1, Fraction frac2) => TryMultiply(frac1, Inverse(frac2));
        public static Fraction operator /(int iNo, Fraction frac1) => TryMultiply(Inverse(frac1), new Fraction(iNo));
        public static Fraction operator /(Fraction frac1, int iNo) => TryMultiply(frac1, Inverse(new Fraction(iNo)));
        public static Fraction operator /(double dbl, Fraction frac1) => TryMultiply(Inverse(frac1), ToFraction(dbl));
        public static Fraction operator /(Fraction frac1, double dbl) => TryMultiply(frac1, Inverse(ToFraction(dbl)));
        
        public static bool operator ==(Fraction frac1, Fraction frac2) 
            => frac1 != null && frac1.Equals(frac2);
        public static bool operator ==(Fraction frac1, int iNo) 
            => frac1 != null && frac1.Equals(new Fraction(iNo));
        public static bool operator ==(Fraction frac1, double dbl) 
            => frac1 != null && frac1.Equals(new Fraction(dbl));
        public static bool operator !=(Fraction frac1, Fraction frac2) 
            => frac1 == null || !frac1.Equals(frac2);
        public static bool operator !=(Fraction frac1, int iNo) 
            => frac1 == null || !frac1.Equals(new Fraction(iNo));
        public static bool operator !=(Fraction frac1, double dbl) 
            => frac1 == null || !frac1.Equals(new Fraction(dbl));

        public static bool operator <(Fraction frac1, Fraction frac2) 
            => frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;
        public static bool operator >(Fraction frac1, Fraction frac2) 
            => frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;
        public static bool operator <=(Fraction frac1, Fraction frac2) 
            => frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;
        public static bool operator >=(Fraction frac1, Fraction frac2) 
            => frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;

        public static implicit operator Fraction(long lNo) => new Fraction(lNo);
        public static implicit operator Fraction(double dNo) => new Fraction(dNo);
        public static implicit operator Fraction(string strNo) => new Fraction(strNo);
        public static explicit operator double(Fraction frac) => frac.ToDouble();
        public static implicit operator string(Fraction frac) => frac.ToString();
        #endregion

        private static long FilterZeroValues(long value)
        {
            if (value == 0)
                throw new FractionException("Denominator cannot be assigned a ZERO Value");
            return value;
        }
    }
}
