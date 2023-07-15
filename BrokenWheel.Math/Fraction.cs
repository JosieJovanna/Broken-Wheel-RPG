using System;
using System.Globalization;

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
        public long Numerator { get; set; }
        public long Denominator
        {
            get => _denominator;
            set => _denominator = FilterZeroValues(value);
        }
        public long Value
        {
            set
            {
                Numerator = value;
                _denominator = 1;
            }
        }

        private long _denominator;

        public Fraction Duplicate()
        {
            var frac = new Fraction
            {
                Numerator = Numerator,
                Denominator = Denominator
            };
            return frac;
        }

        public static Fraction Inverse(Fraction frac)
        {
            var iNumerator = frac.Denominator;
            var iDenominator = frac.Numerator;
            return new Fraction(iNumerator, iDenominator);
        }

        /// <summary>
        /// The function reduces(simplifies) a Fraction object by dividing both its numerator 
        /// and denominator by their GCD
        /// </summary>
        public static void ReduceFraction(Fraction frac)
        {
            try
            {
                AttemptToReduceFraction();
            }
            catch (Exception exp)
            {
                throw new FractionException("Cannot reduce Fraction: " + exp.Message);
            }

            // LOCAL FX
            void AttemptToReduceFraction()
            {
                if (frac.Numerator == 0)
                {
                    frac.Denominator = 1;
                    return;
                }
                ReduceByGcd();
            }
            void ReduceByGcd()
            {
                var greatestCommonDenominator = GreatestCommonDenominator(frac.Numerator, frac.Denominator);
                frac.Numerator /= greatestCommonDenominator;
                frac.Denominator /= greatestCommonDenominator;
                MakeNumeratorNegativeInsteadOfTheDenominator();
            }
            void MakeNumeratorNegativeInsteadOfTheDenominator()
            {
                if (frac.Denominator >= 0) 
                    return;
                frac.Numerator *= -1;
                frac.Denominator *= -1;
            }
        }

        /// <summary>
        /// The function returns Greatest Common Denominator of two numbers (used for reducing a Fraction)
        /// </summary>
        private static long GreatestCommonDenominator(long iNo1, long iNo2)
        {
            if (iNo1 < 0) 
                iNo1 = -iNo1;
            if (iNo2 < 0) 
                iNo2 = -iNo2;
            CalculateGcd();
            return iNo2;
            
            // LOCAL FX
            void CalculateGcd()
            {
                do
                {
                    if (iNo1 < iNo2)
                        (iNo1, iNo2) = (iNo2, iNo1);
                    iNo1 %= iNo2;
                } while (iNo1 != 0);
            }
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
        public override bool Equals(object obj)
        {
            var frac = (Fraction) obj;
            return frac != null && Numerator == frac.Numerator && Denominator == frac.Denominator;
        }

        /// <summary>
        /// Returns a hash code for this fraction
        /// </summary>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF);
        }

        private static long FilterZeroValues(long value)
        {
            if (value == 0)
                throw new FractionException("Denominator cannot be assigned a ZERO Value");
            return value;
        }
    }
}

/*
using System;
using BrokenWheel.Math.Options;

/*
 * ORIGINAL COMMENT
 * 
 * Author: Syed Mehroz Alam
 * Email: smehrozalam@yahoo.com
 * URL: Programming Home 'http://www.geocities.com/smehrozalam/'
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
 * Overloaded Operators (overloaded for Fractions, Integers and Doubles)
 * 	   Unary: -
 * 	   Binary: +, -, *, / 
 * 	   Relational and Logical Operators: ==, !=, <, >, <=, >=
 * 
 * Overloaded user-defined conversions
 * 	   Implicit: From double/long/string to Fraction
 * 	   Explicit: From Fraction to double/string
 #1#

namespace BrokenWheel.Math
{
    /// <summary>
    /// An open-source fraction class, originally written by Syed Mehroz Alam, and edited for this project.
    /// </summary>
    public sealed partial class Fraction
    {
        private long _denominator;
        
        public long Numerator { get; set; }
        
        public long Denominator
        {
            get => _denominator;
            set => _denominator = HandleZeroDenominators(value);
        }
        
        public ZeroDenominatorOption Option { get; set; }
        
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
            var fraction1 = Duplicate().TryReduce(out var wasReduced);
            if (!TryCastToReducedFraction(value, out var fraction2) )
                return false;
            if (fraction1 == null && fraction2 == null)
                return true;
            return fraction1.Numerator == fraction2.Numerator && fraction1.Denominator == fraction2.Denominator;
        }

        private static bool TryCastToReducedFraction(object value, out Fraction result)
        {
            try
            {
                result = (Fraction)value;
                result.TryReduce(out var wasReduced);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Returns a hash code for this fraction
        /// </summary>
        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return Convert.ToInt32((Numerator ^ Denominator) & 0xFFFFFFFF);
        }
    }
}
*/
