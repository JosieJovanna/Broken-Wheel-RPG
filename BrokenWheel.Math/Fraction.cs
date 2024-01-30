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
 */

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
