using BrokenWheel.Math.Options;
using Xunit;

namespace BrokenWheel.Math.Tests.FractionTests;

public class ConstructorTests
{
    private const ZeroDenominatorOption VALID_OPTION = ZeroDenominatorOption.Ignore;
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
        var fraction = new Fraction(VALID_NUMBER, VALID_NUMBER, VALID_OPTION);
        Assert.Equal(VALID_NUMBER, fraction.Numerator);
        Assert.Equal(VALID_NUMBER, fraction.Denominator);
        Assert.Equal(VALID_OPTION, fraction.Option);
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
        var fraction = new Fraction(VALID_NUMBER, VALID_OPTION);
        Assert.Equal(VALID_NUMBER, fraction.Numerator);
        Assert.Equal(1, fraction.Denominator);
        Assert.Equal(VALID_OPTION, fraction.Option);
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
        var fraction = new Fraction(toConvert, VALID_OPTION);
        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
        Assert.Equal(VALID_OPTION, fraction.Option);
    }

    [Fact]
    public void DoubleConstructor_HasDefaultZeroOption_WhenNoneGiven()
    {
        const ZeroDenominatorOption OPTION = default;
        var fraction = new Fraction((double)VALID_NUMBER);
        Assert.Equal(OPTION, fraction.Option);
    }

    [Theory]
    [InlineData("1/2", 1, 2)]
    public void StringConstructor_Passes(string toConvert, int expectedNumerator, int expectedDenominator)
    {
        var fraction = new Fraction(toConvert, VALID_OPTION);
        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
        Assert.Equal(VALID_OPTION, fraction.Option);
    }
}
