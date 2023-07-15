using System;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
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
    }
}

/*
using System;
using BrokenWheel.Math.Options;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        private Fraction Negate()
        {
            Numerator = -Numerator;
            return this;
        }
        
        private static Fraction Negate(Fraction fraction)
        {
            var iNumerator = -fraction.Numerator;
            var iDenominator = fraction.Denominator;
            return new Fraction(iNumerator, iDenominator, fraction.Option);
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
                var crossProduct1 = fraction1.Numerator * fraction2.Denominator;
                var crossProduct2 = fraction2.Numerator * fraction1.Denominator;
                var numerator = crossProduct1 + crossProduct2;
                var denominator = fraction1.Denominator * fraction2.Denominator;
                return new Fraction(numerator, denominator, SameOrDefaultZeroOption(fraction1, fraction2));
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
                var iNumerator = fraction1.Numerator * fraction2.Numerator;
                var iDenominator = fraction1.Denominator * fraction2.Denominator;
                return new Fraction(iNumerator, iDenominator, SameOrDefaultZeroOption(fraction1, fraction2));
            }
        }
    }
}
*/
