using System;
using BrokenWheel.Math.Options;

namespace BrokenWheel.Math
{
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
    public sealed partial class Fraction
    {
        private long _denominator;

        public ZeroDenominatorOption Option { get; set; }
        public long Numerator { get; set; }
        public long Denominator
        {
            get => _denominator;
            set => _denominator = HandleZeroDenominators(value);
        }
        
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
        
        private long HandleZeroDenominators(long denominator)
        {
            return denominator == 0 
                ? HandleZeroDenominator(denominator) 
                : denominator;
        }

        private long HandleZeroDenominator(long denominator)
        {
            switch (Option)
            {
                case ZeroDenominatorOption.ThrowOnSetting:
                    throw new FractionException("Denominator cannot be '0'; cannot divide by zero.");
                case ZeroDenominatorOption.ThrowOnGetting:
                    return 0;
                case ZeroDenominatorOption.SetValueToZero:
                    Numerator = 0;
                    return 1;
                case ZeroDenominatorOption.GetValueAsZero:
                case ZeroDenominatorOption.Ignore:
                default:
                    return denominator;
            }
        }

        private static ZeroDenominatorOption SameOrDefaultZeroOption(Fraction fraction1, Fraction fraction2)
        {
            return fraction1.Option == fraction2.Option 
                ? fraction1.Option 
                : default;
        }
    }
}
