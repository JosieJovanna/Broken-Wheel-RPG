using System;
using System.Collections.Generic;
using BrokenWheel.Core.Stats.Enum;
using Xunit;

namespace BrokenWheel.Core.Utilities.EnumUtilsTests.Tests;

public class GetAllEnumValuesTests
{
    [Fact]
    public void GetAllEnumValues_ThrowsArgumentException_WhenTypeIsNotEnum()
    {
        Assert.Throws<ArgumentException>(() => EnumUtils.GetAllEnumValues<TestClass>());
    }

    private class TestClass
    {
        public int ValueOne = 1;
    }

    [Fact]
    public void GetAllEnumValues_ReturnsAllStats_ForEnum()
    {
        var values = EnumUtils.GetAllEnumValues<TestEnum>();
        Assert.Contains(TestEnum.ValueOne, values);
        Assert.Contains(TestEnum.ValueTwo, values);
    }

    private enum TestEnum
    {
        ValueOne,
        ValueTwo
    }

    [Fact]
    public void GetAllEnumValues_ReturnsAllStats_WhenTypeIsStat()
    {
        var values = EnumUtils.GetAllEnumValues<Stat>();
        var expected = AllStats();

        foreach (var stat in expected)
            Assert.Contains(stat, values);
    }

    private static IList<Stat> AllStats() => new Stat[]
    {
        Stat.Custom,
        Stat.Level,
        Stat.Experience,
        Stat.Luck,
        Stat.HP,
        Stat.SP,
        Stat.WP,
        Stat.Hydration,
        Stat.Satiation,
        Stat.Rest,
        Stat.Sprint,
        Stat.Leap,
        Stat.Climb,
        Stat.Swim,
        Stat.Fortitude,
        Stat.Evasion,
        Stat.Block,
        Stat.Parry,
        Stat.OneHanded,
        Stat.TwoHanded,
        Stat.Ranged,
        Stat.Unarmed,
        Stat.Evocation,
        Stat.WildMagic,
        Stat.Conjuration,
        Stat.Faith,
        Stat.Transmutation,
        Stat.Illusion,
        Stat.Necromancy,
        Stat.Psionics,
        Stat.Cooking,
        Stat.Alchemy,
        Stat.Enchantment,
        Stat.Perception,
        Stat.Intuition,
        Stat.Charisma,
        Stat.Deftness,
        Stat.Sword,
        Stat.Axe,
        Stat.Blunt,
        Stat.Flail,
        Stat.SmallArm,
        Stat.PoleArm,
        Stat.Throwing,
        Stat.Shortbow,
        Stat.Longbow,
        Stat.Trigger,
        Stat.Punch,
        Stat.Kick,
        Stat.Grab,
        Stat.Qi,
        Stat.Self,
        Stat.Touch,
        Stat.Projectile,
        Stat.AreaOfEffect,
    };
}
