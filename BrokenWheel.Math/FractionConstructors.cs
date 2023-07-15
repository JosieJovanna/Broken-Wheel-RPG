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

        public Fraction(long wholeNumber, ZeroDenominatorOption option = default)
        {
            Initialize(wholeNumber, 1, option);
        }

        public Fraction(double floatingValue, ZeroDenominatorOption option = default)
        {
            var temp = FromDouble(floatingValue);
            Initialize(temp.Numerator, temp.Denominator, option);
            Reduce();
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
        }
    }
}
