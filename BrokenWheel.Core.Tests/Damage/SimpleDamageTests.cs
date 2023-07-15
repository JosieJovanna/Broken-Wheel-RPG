using System;
using BrokenWheel.Core.Damage;
using BrokenWheel.Core.Damage.Dps;
using BrokenWheel.Math;
using Xunit;

namespace BrokenWheel.Core.Tests.Damage;

public class SimpleDamageTests
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(999, 4)]
    [InlineData(1000, 3)]
    [InlineData(100, 10)]
    [InlineData(1000, 1000)]
    public void Constructor_Passes(int amount, int duration)
    {
        // setup
        var type = DamageType.Generic;
        var damage = new SimpleDpsCalculator(type, amount, duration);
        var expectedDps = new Fraction(amount, duration);

        // test
        Assert.Equal(amount, damage.TotalDamage);
        Assert.Equal(amount, damage.RemainingDamage);
        Assert.Equal(0, damage.DamageDealt);

        Assert.Equal(duration, damage.Duration);
        Assert.Equal(duration, damage.TimeRemaining);
        Assert.Equal(0, damage.SecondsPassed);

        Assert.Equal(expectedDps.AsDouble(), damage.DPS);
        Assert.Equal(type, damage.Type);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_Throws_ArgumentException_OnInvalidAmounts(int amount)
    {
        Assert.Throws<ArgumentException>(() => new SimpleDpsCalculator(DamageType.Generic, amount, 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_Throws_ArgumentException_OnInvalidDurations(int duration)
    {
        Assert.Throws<ArgumentException>(() => new SimpleDpsCalculator(DamageType.Generic, 1, duration));
    }

    [Fact]
    public void Tick_NoDamageIsDealtAfterDone()
    {
        // setup
        var damage = new SimpleDpsCalculator(DamageType.Generic, 10, 1);

        // execute
        damage.Dps();
        Assert.True(damage.IsDone);
        var nextTick = damage.Dps();

        // test
        Assert.Equal(0, nextTick);
        Assert.Equal(10, damage.DamageDealt);
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(40, 3)]
    [InlineData(100, 10)]
    [InlineData(999, 120)]
    [InlineData(40, 120)]
    [InlineData(1, 99)]
    public void Tick_Properties_CorrectWhileRunning(int amount, int duration)
    {
        // setup
        var damage = new SimpleDpsCalculator(DamageType.Generic, amount, duration);

        var expectedDps = new Fraction(amount, duration);
        var expectedDealt = 0;
        var timePassed = 0;
        var timeRemaining = duration;

        // execute
        while (!damage.IsDone)
        {
            // execute
            timePassed++;
            timeRemaining--;
            var tick = damage.Dps();
            var newExpectedDealt = (int)System.Math.Floor((expectedDps * timePassed).AsDouble());
            var expectedTick = newExpectedDealt - expectedDealt;
            var expectedRemaining = amount - newExpectedDealt;
            expectedDealt = newExpectedDealt;

            // test
            Assert.Equal(expectedTick, tick);
            Assert.Equal(timePassed, damage.SecondsPassed);
            Assert.Equal(timeRemaining, damage.TimeRemaining);
            Assert.Equal(expectedDealt, damage.DamageDealt);
            Assert.Equal(expectedRemaining, damage.RemainingDamage);
        }
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(40, 3)]
    [InlineData(100, 10)]
    [InlineData(999, 120)]
    [InlineData(40, 120)]
    [InlineData(1, 99)]
    public void Tick_Properties_CorrectAfterCompletion(int amount, int duration)
    {
        // setup
        var damage = new SimpleDpsCalculator(DamageType.Generic, amount, duration);

        // execute
        while (!damage.IsDone)
            damage.Dps();
        damage.Dps();

        // test
        Assert.Equal(amount, damage.DamageDealt);
        Assert.Equal(0, damage.RemainingDamage);
        Assert.Equal(0, damage.TimeRemaining);
        Assert.Equal(duration + 1, damage.SecondsPassed);
    }

    [Theory]
    [InlineData(DamageType.Generic, 10, 5, 0)]
    [InlineData(DamageType.Stamina, 100, 3, 0)]
    [InlineData(DamageType.Willpower, 800, 4, 2)]
    [InlineData(DamageType.Bleed, 6894, 3, 5)]
    [InlineData(DamageType.Slash, 123, 3, 5)]
    [InlineData(DamageType.Shock, 8, 12, 5)]
    [InlineData(DamageType.Corrosion, 8, 12, 12)]
    [InlineData(DamageType.Necrotic, 8, 12, 13)]
    public void ToString_Contains_RelevantInfo(DamageType type, int amount, int duration, int time)
    {
        // setup
        var damage = new SimpleDpsCalculator(type, amount, duration);
        var expectedTime = System.Math.Min(duration, time);
        var expectedDps = new Fraction(amount, duration);
        var expectedDealt = (int)System.Math.Floor(expectedDps.AsDouble() * expectedTime);

        // execute
        for (var i = 0; i < time; i++)
            damage.Dps();
        var result = damage.ToString();

        // test
        Assert.Contains(type.GetName(), result);
        Assert.Contains(amount.ToString(), result);
        Assert.Contains(duration.ToString(), result);
        Assert.Contains(expectedDps.ToString(), result);
        Assert.Contains(expectedDealt.ToString(), result);
        Assert.Contains(expectedTime.ToString(), result);
    }

    [Theory]
    [InlineData(DamageType.Generic, 10, 5, 0)]
    [InlineData(DamageType.Stamina, 100, 3, 0)]
    [InlineData(DamageType.Willpower, 800, 4, 2)]
    [InlineData(DamageType.Bleed, 6894, 3, 5)]
    [InlineData(DamageType.Slash, 123, 3, 5)]
    [InlineData(DamageType.Shock, 8, 12, 5)]
    [InlineData(DamageType.Corrosion, 8, 12, 12)]
    [InlineData(DamageType.Necrotic, 8, 12, 13)]
    public void ToDataString_Contains_RelevantInfo(DamageType type, int amount, int duration, int time)
    {
        // setup
        var damage = new SimpleDpsCalculator(type, amount, duration);
        var expectedTime = System.Math.Min(duration, time);
        var expectedDps = new Fraction(amount, duration);
        var expectedDealt = (int)System.Math.Floor(expectedDps.AsDouble() * expectedTime);

        // execute
        for (var i = 0; i < time; i++)
            damage.Dps();
        var result = damage.ToDataString();

        // test
        Assert.Contains(type.GetName(), result);
        Assert.Contains(amount.ToString(), result);
        Assert.Contains(duration.ToString(), result);
        Assert.Contains(expectedDps.ToString(), result);
        Assert.Contains(expectedDealt.ToString(), result);
        Assert.Contains(System.Math.Min(duration, time).ToString(), result);
    }
}
