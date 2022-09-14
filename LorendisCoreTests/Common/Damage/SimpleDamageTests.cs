using Xunit;
using LorendisCore.Common.Damage;

namespace LorendisCore.Common.Damage.Tests
{
    public class SimpleDamageTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(100, 10)]
        [InlineData(999, 4)]
        [InlineData(1000, 1000)]
        public void Constructor_Passes(int amount, int duration)
        {
            var damage = new SimpleDamage(DamageType.Generic, amount, duration);

            Assert.Equal(amount, damage.Total);
            Assert.Equal(amount, damage.Remaining);
            Assert.Equal(0, damage.Dealt);

            Assert.Equal(duration, damage.Duration);
            Assert.Equal(duration, damage.TimeRemaining);
            Assert.Equal(0, damage.TimePassed);

            int expectedDps = (int)Math.Floor((float)amount / duration);
            Assert.Equal(expectedDps, damage.DPS);
        }

        [Fact]
        public void Constructor_OneTimeDamage_IsZeroOnRationalRatios()
        {
            var damage = new SimpleDamage(DamageType.Generic, 4, 4);
            Assert.Equal(0, damage.OneTimeDamage);
        }

        [Fact]
        public void Constructor_OneTimeDamage_IsSetOnIrrationalRatios()
        {
            var damage = new SimpleDamage(DamageType.Generic, 4, 3);
            Assert.Equal(1, damage.OneTimeDamage);
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
            var damage = new SimpleDamage(DamageType.Generic, 10, 1);

            damage.Tick();
            Assert.True(damage.IsDone);

            var nextTick = damage.Tick();
            Assert.Equal(0, nextTick);
            Assert.Equal(10, damage.Dealt);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(100, 10)]
        [InlineData(40, 3)]
        [InlineData(40, 120)]
        [InlineData(999, 120)]
        public void Tick_Passes(int amount, int duration)
        {
            var damage = new SimpleDamage(DamageType.Generic, amount, duration);

            int expectedDps = (int)Math.Floor((float)amount / duration);
            int expectedOneTimeDamage = amount - expectedDps * duration;
            int expectedDealt = 0;
            int expectedRemaining = amount;
            int timePassed = 0;
            int timeRemaining = duration;

            while (!damage.IsDone)
            {
                // Queries Before Tick
                Assert.Equal(expectedDealt, damage.Dealt);
                Assert.Equal(timePassed, damage.TimePassed);
                Assert.Equal(timeRemaining, damage.TimeRemaining);

                // Tick!
                timePassed++;
                timeRemaining--;
                var tick = damage.Tick();
                int expectedTick = timePassed == 1
                ? expectedDps + expectedOneTimeDamage
                : expectedDps;
                expectedDealt += expectedTick;
                expectedRemaining -= expectedTick;

                // Queries After Tick
                Assert.Equal(expectedTick, tick);
                Assert.Equal(timePassed, damage.TimePassed);
                Assert.Equal(timeRemaining, damage.TimeRemaining);
                Assert.Equal(expectedDealt, damage.Dealt);
                Assert.Equal(expectedRemaining, damage.Remaining);
            }

            Assert.Equal(amount, expectedDealt);
            Assert.Equal(amount, damage.Dealt);

            Assert.Equal(0, expectedRemaining);
            Assert.Equal(0, damage.Remaining);

            Assert.Equal(0, timeRemaining);
            Assert.Equal(0, damage.TimeRemaining);
            Assert.Equal(duration, timePassed);
            Assert.Equal(duration, damage.TimePassed);
        }
    }
}