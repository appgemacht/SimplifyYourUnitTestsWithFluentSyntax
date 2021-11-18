using FluentAssertions;
using Xunit;

namespace FluentFoo.Tests.FactoryMethods
{
    public class FooTest
    {
        [Fact]
        public void CreateFoo_WithAllProperties()
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
    }
}
