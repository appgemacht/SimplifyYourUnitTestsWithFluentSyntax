using System.Linq;
using FluentAssertions;
using Xunit;

namespace FluentFoo.Tests.FactoryMethods
{
    public class FooTest
    {
        [Fact]
        // using test helper
        public void CreateFoo_WithAllPropertiesAndOneBar()
        {
            // Arrange
            var bar = TestBar.Create("Bar");

            // Act
            var testee = TestFoo.Create("Property1", 123, new[] { bar });
            testee.Property3 = 345.67; // let's say this is the only test where Property3 is relevant. don't introduce any test helper param for this!

            // Assert
            testee.Bars.Should().ContainSingle().Which.Property1.Should().Be("Bar");
            testee.Property1.Should().Be("Property1");
            testee.Property2.Should().Be(123);
            testee.Property3.Should().Be(345.67);
        }
        
        [Fact]
        // using helper method in test class
        public void CreateFoo_WithOneProperty()
        {
            // Arrange
            // Act
            var testee = CreateFoo("ABC");
            
            // Assert
            testee.Property1.Should().Be("ABC");
        }

        private static Foo CreateFoo(string property1 = "Foo", int property2 = 1234, double property3 = 12.34) 
            => new Foo(property1, property2, property3) { Bars = new[] { CreateBar() }.ToList() };

        private static Bar CreateBar(string property1 = null) => new Bar(property1 ?? "Bar");
    }
}
