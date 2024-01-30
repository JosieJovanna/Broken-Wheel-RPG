using System;
using Xunit;

namespace LorendisCore.Equipment.Implements.Tests
{
    public class ImplementExtensionsTests
    {
        [Fact]
        public void Validate_That_IsTwoHanded_Functions()
        {
            IInterface impl1 = new Implementation();
            IInterface impl2 = new Impl2();
            WriteType(impl1);
            WriteType(impl2);

            Assert.True(impl1.GetType().IsAssignableTo(typeof(IInterface)));

            Console.WriteLine("instance of Interface: " + impl2.GetType().IsSubclassOf(typeof(IInterface)).ToString());
            Console.WriteLine("instance of Interface 2: " + impl2.GetType().IsInstanceOfType(typeof(IInt2)));
            Console.WriteLine("instance of Implementation: " + impl2.GetType().IsInstanceOfType(typeof(Implementation)));
            Console.WriteLine("instance of Implementation 2: " + impl2.GetType().IsInstanceOfType(typeof(Impl2)));
            return;

            void WriteType(IInterface obj)
            {
                Console.WriteLine(obj.GetType());
            }
        }

        interface IInterface
        {
        }

        interface IInt2
        {
        }

        class Implementation : IInterface
        {
        }

        class Impl2 : Implementation, IInt2
        {
        }
    }
}
