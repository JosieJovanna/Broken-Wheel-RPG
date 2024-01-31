using BrokenWheel.Math.Options;
using Xunit;

namespace BrokenWheel.Math.Fractions.Tests;

public class ConstructorTests
{
    private const ZeroDenominatorOption IGNORE_OPTION = ZeroDenominatorOption.Ignore;
    private const int VALID_NUMBER = 3;

    [Fact]
    public void PlainConstructor_HasDefaultZeroOption_WhenNoneGiven()
    {
        const ZeroDenominatorOption OPTION = default;
        var fraction = new Fraction();
        Assert.Equal(OPTION, fraction.Option);
    }

    [Fact]
    public void PlainConstructor_Passes()
    {
        var fraction = new Fraction(VALID_NUMBER, VALID_NUMBER, IGNORE_OPTION);
        Assert.Equal(VALID_NUMBER, fraction.Numerator);
        Assert.Equal(VALID_NUMBER, fraction.Denominator);
        Assert.Equal(IGNORE_OPTION, fraction.Option);
    }

    [Fact]
    public void NumeratorDenominatorConstructor_HasDefaultZeroOption_WhenNoneGiven()
    {
        const ZeroDenominatorOption OPTION = default;
        var fraction = new Fraction(VALID_NUMBER, VALID_NUMBER);
        Assert.Equal(OPTION, fraction.Option);
    }

    [Fact]
    public void WholeNumberConstructor_Passes()
    {
        var fraction = new Fraction(VALID_NUMBER, IGNORE_OPTION);
        Assert.Equal(VALID_NUMBER, fraction.Numerator);
        Assert.Equal(1, fraction.Denominator);
        Assert.Equal(IGNORE_OPTION, fraction.Option);
    }

    [Fact]
    public void WholeNumberConstructor_Passes_WhenValueIsZero()
    {
        var fraction = new Fraction(0, IGNORE_OPTION);
        Assert.Equal(0, fraction.Numerator);
        Assert.Equal(1, fraction.Denominator);
        Assert.Equal(IGNORE_OPTION, fraction.Option);
    }

    [Fact]
    public void WholeNumberConstructor_HasDefaultZeroOption_WhenNoneGiven()
    {
        const ZeroDenominatorOption OPTION = default;
        var fraction = new Fraction(VALID_NUMBER);
        Assert.Equal(OPTION, fraction.Option);
    }

    [Theory]
    [InlineData(.5, 1, 2)]
    public void DoubleConstructor_Passes(double toConvert, int expectedNumerator, int expectedDenominator)
    {
        var fraction = new Fraction(toConvert, IGNORE_OPTION);
        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
        Assert.Equal(IGNORE_OPTION, fraction.Option);
    }

    [Fact]
    public void DoubleConstructor_HasDefaultZeroOption_WhenNoneGiven()
    {
        const ZeroDenominatorOption OPTION = default;
        var fraction = new Fraction((double)VALID_NUMBER);
        Assert.Equal(OPTION, fraction.Option);
    }

    [Theory]
    [InlineData("7/0", 7, 0)]
    [InlineData("0/4", 0, 4)]
    [InlineData("1/2", 1, 2)]
    [InlineData("-3/4", -3, 4)]
    [InlineData("6/-3", 6, -3)]
    [InlineData("-7/-7", -7, -7)]
    [InlineData("019238019243/2", 019238019243, 2)]
    [InlineData("7/2034982394", 7, 2034982394)]
    [InlineData("7_099_821/9_000_013_232", 7_099_821, 9_000_013_232)]
    [InlineData("68_980_001/5,473", 68_980_001, 5473)]
    [InlineData("5,946,322,681,710/5,429,251,144,170", 5946322681710, 5429251144170)]
    [InlineData("8___1_/,94,3455", 81, 943455)]
    public void StringConstructor_Passes_WhenStringIsValidFractionFormat(
        string toConvert, long expectedNumerator, long expectedDenominator)
    {
        var fraction = new Fraction(toConvert, IGNORE_OPTION);
        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
        Assert.Equal(IGNORE_OPTION, fraction.Option);
    }

    [Theory]
    [InlineData("0.5", 1, 2)]
    [InlineData("1.5", 3, 2)]
    [InlineData(".3333333333333333333333333", 1, 3)]
    [InlineData(".6666666666666666666666", 2, 3)]
    [InlineData(".16666666666666666666666", 1, 6)]
    [InlineData(".218614", 109307, 500000)]
    [InlineData("809123", 809123, 1)]
    [InlineData("1_000_000", 1_000_000, 1)]
    [InlineData("34,000", 34000, 1)]
    [InlineData(",,,2,,,", 2, 1)]
    [InlineData(".0,625", 1, 16)]
    [InlineData("._02", 1, 50)]
    public void StringConstructor_Passes_WhenStringIsValidDecimalFormat(
        string toConvert, long expectedNumerator, long expectedDenominator)
    {
        var fraction = new Fraction(toConvert, IGNORE_OPTION);
        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
        Assert.Equal(IGNORE_OPTION, fraction.Option);
    }

    [Theory]
    [InlineData("not a fraction")]
    [InlineData("0123456789abcdef")]
    [InlineData("1/2e")]
    [InlineData("e1/2")]
    [InlineData("/213")]
    [InlineData("67/")]
    [InlineData("21/2134/")]
    [InlineData("123 321")]
    [InlineData(".21301312.")]
    public void StringConstructor_ThrowsArgumentException_WhenIncorrectlyFormatted(string toConvert)
    {
        Assert.Throws<FractionException>(() => new Fraction(toConvert, IGNORE_OPTION));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(999)]
    [InlineData(-999)]
    public void FromDouble_ThrowsArgumentOutOfRangeException_WhenAccuracyNotBetweenZeroAndOne(double accuracy)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Fraction.FromDouble(0.5, accuracy));
    }

    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    public void FromDouble_ThrowsArgumentException_WhenNaNOrInfinity(double toConvert)
    {
        Assert.Throws<ArgumentException>(() => Fraction.FromDouble(toConvert));
    }
}
