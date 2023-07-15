using System;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        public Fraction Duplicate()
        {
            var frac = new Fraction
            {
                Numerator = Numerator,
                Denominator = Denominator
            };
            return frac;
        }

        public static Fraction Inverse(Fraction fraction)
        {
            var numerator = fraction.Denominator;
            var denominator = fraction.Numerator;
            return new Fraction(numerator, denominator);
        }

        /// <summary>
        /// The function reduces(simplifies) a Fraction object by dividing both its numerator 
        /// and denominator by their GCD
        /// </summary>
        public static void Reduce(Fraction fraction)
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
        /// The function returns Greatest Common Denominator of two numbers (used for reducing a Fraction)
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
    }
}
