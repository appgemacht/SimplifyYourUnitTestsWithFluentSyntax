using FluentAssertions;
using Xunit;

namespace FluentFoo.Tests.Extensions
{
    public class FooTest
    {
        [Fact]
        public void CreateFoo_WithAllPropertiesAndOneBar()
        {
            // Arrange
            var bar = TestBar.Create().With("Bar");

            // Act
            var testee = TestFoo2.Create()
                .WithPropertyGroup1("Property1")
                .WithPropertyGroup2(123, 345.67)
                .AddBar(bar);

            // Assert
            testee.Bars.Should().ContainSingle().Which.Property1.Should().Be("Bar");
            testee.Property1.Should().Be("Property1");
            testee.Property2.Should().Be(123);
            testee.Property3.Should().Be(345.67);
        }
    }
}
