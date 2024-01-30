using System;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        /// <summary>
        /// Creates a new fraction with the exact same numerator and denominator.
        /// </summary>
        public Fraction Duplicate() => Duplicate(this);

        /// <summary>
        /// Creates a copy of a fraction, with the exact same numerator and denominator.
        /// </summary>
        public static Fraction Duplicate(Fraction fraction)
        {
            return new Fraction
            {
                Numerator = fraction.Numerator,
                Denominator = fraction.Denominator,
                Option = fraction.Option
            };
        }

        /// <summary>
        /// Creates another fraction with the numerator and denominator swapped.
        /// </summary>
        public Fraction Inverse() => Inverse(this);

        /// <summary>
        /// Creates a copy of the fraction with numerator and denominator reversed.
        /// </summary>
        public static Fraction Inverse(Fraction fraction)
        {
            var numerator = fraction.Denominator;
            var denominator = fraction.Numerator;
            return new Fraction(numerator, denominator, fraction.Option);
        }

        /// <summary>
        /// Tries to reduce the fraction. If it fails, keeps the fraction unchanged.
        /// </summary>
        /// <returns>The resulting fraction, reduced or not.</returns>
        public Fraction TryReduce(out bool succeeded)
        {
            var backup = Duplicate();
            try
            {
                Reduce();
                succeeded = true;
            }
            catch (Exception)
            {
                Numerator = backup.Numerator;
                Denominator = backup.Denominator;
                succeeded = false;
            }
            return this;
        }

        public Fraction Reduce() => Reduce(this);
        
        /// <summary>
        /// The function reduces (simplifies) a Fraction object by dividing both its
        /// numerator and denominator by their greatest common denominator.
        /// Effective value remains the same.
        /// </summary>
        public static Fraction Reduce(Fraction fraction)
        {
            try
            {
                return AttemptToReduceFraction(fraction);
            }
            catch (Exception ex)
            {
                throw new FractionException("Cannot reduce Fraction: " + ex.Message);
            }
        }
        
        private static Fraction AttemptToReduceFraction(Fraction fraction)
        {
            if (SetDenominatorToOneIfNumeratorIsZero(fraction))
                return fraction;
            ReduceByGcd(fraction);
            MakeNumeratorNegativeInsteadOfTheDenominator(fraction);
            return fraction;
        }

        private static bool SetDenominatorToOneIfNumeratorIsZero(Fraction fraction)
        {
            if (fraction.Numerator != 0)
                return false;
            fraction.Denominator = 1;
            return true;
        }

        private static void ReduceByGcd(Fraction fraction)
        {
            var greatestCommonDenominator = GreatestCommonDenominator(fraction.Numerator, fraction.Denominator);
            fraction.Numerator /= greatestCommonDenominator;
            fraction.Denominator /= greatestCommonDenominator;
        }
        
        private static void MakeNumeratorNegativeInsteadOfTheDenominator(Fraction fraction)
        {
            if (fraction.Denominator >= 0) 
                return;
            fraction.Numerator *= -1;
            fraction.Denominator *= -1;
        }

        /// <summary>
        /// Calculates the greatest common denominator of the fraction's numerator and denominator.
        /// </summary>
        public long GreatestCommonDenominator() => GreatestCommonDenominator(Numerator, Denominator);

        /// <summary>
        /// Calculates the Greatest Common Denominator of two numbers (used for reducing a Fraction).
        /// </summary>
        public static long GreatestCommonDenominator(long value1, long value2)
        {
            if (value1 < 0) 
                value1 = -value1;
            if (value2 < 0) 
                value2 = -value2;
            return CalculateGcd(value1, value2);
        }

        private static long CalculateGcd(long value1, long value2)
        {
            do
            {
                if (value1 < value2)
                    (value1, value2) = (value2, value1);
                value1 %= value2;
            } while (value1 != 0);
            return value2;
        }
    }
}
