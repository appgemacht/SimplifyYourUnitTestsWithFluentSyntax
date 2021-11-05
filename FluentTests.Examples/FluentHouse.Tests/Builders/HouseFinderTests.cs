using FluentAssertions;
using FluentHouse.Tests.Extensions;
using Xunit;

namespace FluentHouse.Tests.Builders
{
    public class HouseFinderTests
    {
        [Fact]
        public void FindLargestHouse_WhenComparingALuxuryAndSimpleHouse()
        {
            // Arrange
            House simpleHouse = new TestHouseBuilder().CreateSimpleHouse().Build();
            House luxuryHouse = new TestHouseBuilder().CreateComplexHouse(numberOfFloors: 4, roomsPerFloor: 10).Build();

            Neighbourhood neighbourhood = TestNeighbourhood.Create().WithHouses(luxuryHouse, simpleHouse);

            // Act
            var largestHouse = new HouseFinder(neighbourhood).FindLargestHouse();

            // Assert
            largestHouse.Should().Be(luxuryHouse);
        }
    }
}