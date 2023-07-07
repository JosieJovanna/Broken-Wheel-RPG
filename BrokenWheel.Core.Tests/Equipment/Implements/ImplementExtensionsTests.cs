using System;
using Xunit;

namespace LorendisCore.Equipment.Implements.Tests;

public class ImplementExtensionsTests
{
    [Fact]
    public void Validate_That_IsTwoHanded_Functions()
    {
        Interface impl1 = new Implementation();
        Interface impl2 = new Impl2();
        WriteType(impl1);
        WriteType(impl2);

        Assert.IsType<Interface>(impl1);

        Console.WriteLine("instance of Interface: " + impl2.GetType().IsSubclassOf(typeof(Interface)).ToString());
        Console.WriteLine("instance of Interface 2: " + impl2.GetType().IsInstanceOfType(typeof(Int2)));
        Console.WriteLine("instance of Implementation: " + impl2.GetType().IsInstanceOfType(typeof(Implementation)));
        Console.WriteLine("instance of Implementation 2: " + impl2.GetType().IsInstanceOfType(typeof(Impl2)));
        return;

        void WriteType(Interface obj)
        {
            Console.WriteLine(obj.GetType());
        }
    }

    interface Interface
    {
    }

    interface Int2
    {
    }

    class Implementation : Interface
    {
    }

    class Impl2 : Implementation, Int2
    {
    }        
}