﻿namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        public Fraction() 
            => Initialize(0, 1);

        public Fraction(long iNumerator, long iDenominator) 
            => Initialize(iNumerator, iDenominator);

        public Fraction(long iWholeNumber) 
            => Initialize(iWholeNumber, 1);

        public Fraction(double dDecimalValue)
        {
            var temp = ToFraction(dDecimalValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        public Fraction(string strValue)
        {
            var temp = ToFraction(strValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        private void Initialize(long iNumerator, long iDenominator)
        {
            Numerator = iNumerator;
            Denominator = iDenominator;
            ReduceFraction(this);
        }
    }
}

/*
using BrokenWheel.Math.Options;

namespace BrokenWheel.Math
{
    public sealed partial class Fraction
    {
        public Fraction(ZeroDenominatorOption option = default)
        {
            Initialize(0, 1, option);
        }

        public Fraction(long numerator, long denominator, ZeroDenominatorOption option = default)
        {
            Initialize(numerator, denominator, option);
        }

        public Fraction(long wholeNumber, ZeroDenominatorOption option)
        {
            Initialize(wholeNumber, 1, option);
        }

        public Fraction(double decimalValue, ZeroDenominatorOption option = default)
        {
            var temp = FromDouble(decimalValue);
            Initialize(temp.Numerator, temp.Denominator, option);
        }

        public Fraction(string valueAsString, ZeroDenominatorOption option = default)
        {
            var temp = FromString(valueAsString);
            Initialize(temp.Numerator, temp.Denominator, option);
        }
        
        private void Initialize(long numerator, long denominator, ZeroDenominatorOption zeroDenominatorOption = default)
        {
            Option = zeroDenominatorOption; // set first so that setting denominator behaves correctly
            Numerator = numerator;
            Denominator = denominator;
            Reduce();
        }
    }
}
*/
