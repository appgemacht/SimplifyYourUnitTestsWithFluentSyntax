using FluentAssertions;
using Xunit;

namespace FluentHouse.Tests.Extensions
{
    public class HouseFinderTests
    {
        [Fact]
        public void FindLargestHouse_WhenComparingALuxuryAndSimpleHouse()
        {
            // Arrange
            House simpleHouse = TestHouse.CreateSimpleHouse();
            House luxuryHouse = TestHouse.CreateComplexHouse(numberOfFloors: 4, roomsPerFloor: 10);

            Neighbourhood neighbourhood = TestNeighbourhood.Create().WithHouses(luxuryHouse, simpleHouse);

            // Act
            var largestHouse = new HouseFinder(neighbourhood).FindLargestHouse();

            // Assert
            largestHouse.Should().Be(luxuryHouse);
        }
    }
}