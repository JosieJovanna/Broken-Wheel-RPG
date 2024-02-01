using Xunit;

namespace BrokenWheel.Math.Fractions.Tests;

public class UtilityTests
{
    [Theory]
    [InlineData(2, 1)]
    [InlineData(-3, 4)]
    [InlineData(5, -6)]
    [InlineData(-7, -8)]
    public void Inverse_ReturnsNewFraction_WithCorrectlyInversedVariables(int numThenDen, int denThenNum)
    {
        var fraction = new Fraction(numThenDen, denThenNum);
        var inversed = Fraction.Inverse(fraction);

        Assert.Equal(numThenDen, inversed.Denominator);
        Assert.Equal(denThenNum, inversed.Numerator);
    }

    [Fact]
    public void Duplicate_Returns_DifferentInstanceOfMatchingFraction()
    {
        var originalFraction = new Fraction(2, 3);
        var newFraction = originalFraction.Duplicate();

        Assert.Equal(originalFraction.Numerator, newFraction.Numerator);
        Assert.Equal(originalFraction.Denominator, newFraction.Denominator);
        // different instance
        newFraction.Numerator++;
        Assert.NotEqual(originalFraction.Numerator, newFraction.Numerator);
    }

    [Theory]
    [InlineData(5, 3, 5, 3)]
    [InlineData(1, 2, 5, 10)]
    [InlineData(16, 5, 16 * 80, 5 * 80)]
    public void Reduce_CorrectlyReducesFractions_WithPositiveNumeratorAndPositiveDenominator(
        int expectedNumerator, int expectedDenominator, int numerator, int denominator)
    {
        var fraction = new Fraction(numerator, denominator);
        fraction.Reduce();

        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
    }

    [Theory]
    [InlineData(-5, 3, -5, 3)]
    [InlineData(-1, 2, -5, 10)]
    [InlineData(-16, 5, -16 * 80, 5 * 80)]
    public void Reduce_CorrectlyReducesFractions_WithNegativeNumeratorAndPositiveDenominator(
        int expectedNumerator, int expectedDenominator, int numerator, int denominator)
    {
        var fraction = new Fraction(numerator, denominator);
        fraction.Reduce();

        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
    }

    [Theory]
    [InlineData(5, 3, -5, -3)]
    [InlineData(1, 2, -5, -10)]
    [InlineData(16, 5, -16 * 80, -5 * 80)]
    public void Reduce_CorrectlyReducesFractions_WithNegativeNumeratorAndNegativeDenominator(
        int expectedNumerator, int expectedDenominator, int numerator, int denominator)
    {
        var fraction = new Fraction(numerator, denominator);
        fraction.Reduce();

        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
    }

    [Theory]
    [InlineData(-5, 3, 5, -3)]
    [InlineData(-1, 2, 5, -10)]
    [InlineData(-16, 5, 16 * 80, -5 * 80)]
    public void Reduce_CorrectlyReducesFractions_WithPositiveNumeratorAndNegativeDenominator(
        int expectedNumerator, int expectedDenominator, int numerator, int denominator)
    {
        var fraction = new Fraction(numerator, denominator);
        fraction.Reduce();

        Assert.Equal(expectedNumerator, fraction.Numerator);
        Assert.Equal(expectedDenominator, fraction.Denominator);
    }
}
