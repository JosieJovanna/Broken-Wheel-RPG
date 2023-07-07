using BrokenWheel.Core.Damage;
using Xunit;

namespace BrokenWheel.Core.Tests.Damage;

public class ComplexDamageTests
{
    [Theory]
    [InlineData(5, new double[] { 1.0, 2.5, 3, 4, 5.5 })]
    [InlineData(10, new double[] { -1.0, 2.5, -3, 4, -5.5 })]
    [InlineData(1, new double[] { 5, 2 })]
    [InlineData(1, new double[] { 5, -2 })]
    public void Constructor_Passes(int duration, double[] coefficients)
    {
        var damage = new PolynomialDamage(DamageType.Generic, coefficients, duration);
        Assert.True(true);
    }
}
