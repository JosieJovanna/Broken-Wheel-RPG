using System;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        private static Fraction Negate(Fraction fraction)
        {
            var numerator = -fraction.Numerator;
            var denominator = fraction.Denominator;
            return new Fraction(numerator, denominator);
        }

        private static Fraction TryAdd(Fraction fraction1, Fraction fraction2)
        {
            try
            {
                return Add(fraction1, fraction2);
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

        private static Fraction Add(Fraction fraction1, Fraction fraction2)
        {
            checked
            {
                var numerator = fraction1.Numerator * fraction2.Denominator + fraction2.Numerator * fraction1.Denominator;
                var denominator = fraction1.Denominator * fraction2.Denominator;
                return new Fraction(numerator, denominator);
            }
        }

        private static Fraction TryMultiply(Fraction fraction1, Fraction fraction2)
        {
            try
            {
                return Multiply(fraction1, fraction2);
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

        private static Fraction Multiply(Fraction fraction1, Fraction fraction2)
        {
            checked
            {
                var numerator = fraction1.Numerator * fraction2.Numerator;
                var denominator = fraction1.Denominator * fraction2.Denominator;
                return new Fraction(numerator, denominator);
            }
        }
    }
}
