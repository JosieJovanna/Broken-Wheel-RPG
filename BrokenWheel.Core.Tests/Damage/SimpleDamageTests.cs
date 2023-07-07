using System;
using BrokenWheel.Core.Damage;
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
        var damage = new SimpleDamage(type, amount, duration);
        var expectedDps = new Fraction(amount, duration);

        // test
        Assert.Equal(amount, damage.Total);
        Assert.Equal(amount, damage.Remaining);
        Assert.Equal(0, damage.Dealt);

        Assert.Equal(duration, damage.Duration);
        Assert.Equal(duration, damage.TimeRemaining);
        Assert.Equal(0, damage.TimePassed);

        Assert.Equal(expectedDps.ToDouble(), damage.DPS);
        Assert.Equal(type, damage.Type);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_Throws_ArgumentException_OnInvalidAmounts(int amount)
    {
        Assert.Throws<ArgumentException>(() => new SimpleDamage(DamageType.Generic, amount, 1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Constructor_Throws_ArgumentException_OnInvalidDurations(int duration)
    {
        Assert.Throws<ArgumentException>(() => new SimpleDamage(DamageType.Generic, 1, duration));
    }

    [Fact]
    public void Tick_NoDamageIsDealtAfterDone()
    {
        // setup
        var damage = new SimpleDamage(DamageType.Generic, 10, 1);

        // execute
        damage.Tick();
        Assert.True(damage.IsDone);
        var nextTick = damage.Tick();

        // test
        Assert.Equal(0, nextTick);
        Assert.Equal(10, damage.Dealt);
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
        var damage = new SimpleDamage(DamageType.Generic, amount, duration);

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
            var tick = damage.Tick();
            var newExpectedDealt = (int)Math.Floor((expectedDps * timePassed).ToDouble());
            var expectedTick = newExpectedDealt - expectedDealt;
            var expectedRemaining = amount - newExpectedDealt;
            expectedDealt = newExpectedDealt;

            // test
            Assert.Equal(expectedTick, tick);
            Assert.Equal(timePassed, damage.TimePassed);
            Assert.Equal(timeRemaining, damage.TimeRemaining);
            Assert.Equal(expectedDealt, damage.Dealt);
            Assert.Equal(expectedRemaining, damage.Remaining);
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
        var damage = new SimpleDamage(DamageType.Generic, amount, duration);

        // execute
        while (!damage.IsDone)
            damage.Tick();
        damage.Tick();

        // test
        Assert.Equal(amount, damage.Dealt);
        Assert.Equal(0, damage.Remaining);
        Assert.Equal(0, damage.TimeRemaining);
        Assert.Equal(duration + 1, damage.TimePassed);
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
        var damage = new SimpleDamage(type, amount, duration);
        var expectedTime = Math.Min(duration, time);
        var expectedDps = new Fraction(amount, duration);
        var expectedDealt = (int)Math.Floor(expectedDps.ToDouble() * expectedTime);

        // execute
        for (var i = 0; i < time; i++)
            damage.Tick();
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
        var damage = new SimpleDamage(type, amount, duration);
        var expectedTime = Math.Min(duration, time);
        var expectedDps = new Fraction(amount, duration);
        var expectedDealt = (int)Math.Floor(expectedDps.ToDouble() * expectedTime);

        // execute
        for (var i = 0; i < time; i++)
            damage.Tick();
        var result = damage.ToDataString();

        // test
        Assert.Contains(type.GetName(), result);
        Assert.Contains(amount.ToString(), result);
        Assert.Contains(duration.ToString(), result);
        Assert.Contains(expectedDps.ToString(), result);
        Assert.Contains(expectedDealt.ToString(), result);
        Assert.Contains(Math.Min(duration, time).ToString(), result);
    }
}
