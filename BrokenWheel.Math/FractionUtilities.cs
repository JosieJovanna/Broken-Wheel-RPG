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
        public void Reduce()
        {
            var (tempNumerator, tempDenominator) = (Numerator, Denominator);
            try
            {
                AttemptToReduceFraction();
                (Numerator, Denominator) = (tempNumerator, tempDenominator);
            }
            catch (Exception ex)
            {
                throw new FractionException("Cannot reduce Fraction: " + ex.Message);
            }

            // LOCAL FX
            void AttemptToReduceFraction()
            {
                if (tempNumerator == 0)
                    tempDenominator = 1;
                else
                    ReduceByGcd();
            }
            void ReduceByGcd()
            {
                var greatestCommonDenominator = GreatestCommonDenominator(tempNumerator, tempDenominator);
                tempNumerator /= greatestCommonDenominator;
                tempDenominator /= greatestCommonDenominator;
                MakeNumeratorNegativeInsteadOfTheDenominator();
            }
            void MakeNumeratorNegativeInsteadOfTheDenominator()
            {
                if (tempDenominator >= 0) 
                    return;
                tempNumerator *= -1;
                tempDenominator *= -1;
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
