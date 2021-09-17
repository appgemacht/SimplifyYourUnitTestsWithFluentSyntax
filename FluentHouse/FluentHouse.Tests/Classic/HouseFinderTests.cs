using FluentAssertions;
using FluentHouse.Tests.Extensions;
using System;
using System.Linq;
using Xunit;

namespace FluentHouse.Tests.Classic
{
    public class HouseFinderTests
    {
        [Fact]
        public void FindLargestHouse_WhenComparingALuxuryAndSimpleHouse()
        {
            // Arrange
            House simpleHouse = new House
            {
                Floors = new[]
                {
                    new Floor
                    {
                        Level = 1, Rooms = new[]
                        {
                            new Room("Living Room", 20, 1, 4, 0, "White", new DateTime(2020, 06, 01)),
                            new Room("Kitchen", 10, 2, 4, 2, "Black", new DateTime(2009, 06, 01)),
                            new Room("Bathroom", 5, 3, 2, 3, "Blue", new DateTime(2012, 06, 01))
                        }.ToList()
                    }
                }.ToList()
            };

            House luxuryHouse = new House
            {
                Floors = new[]
                {
                    new Floor
                    {
                        Level = -1, Rooms = new[]
                        {
                            new Room("Basement", 50, -1, 1, 0, "Grey", new DateTime(1998, 06, 01))
                        }.ToList()
                    },
                    new Floor
                    {
                        Level = 0, Rooms = new[]
                        {
                            new Room("Corridor", 5, null, 0, 0, "Brown", new DateTime(2004, 06, 01)),
                            new Room("Bathroom", 5, 2, 2, 3, "Blue", new DateTime(2012, 06, 01)),
                            new Room("Bedroom", 10, 1, 2, 0, "White", new DateTime(2014, 06, 01))
                        }.ToList()
                    },
                    new Floor
                    {
                        Level = 1, Rooms = new[]
                        {
                            new Room("Corridor", 5, null, 0, 0, "Brown", new DateTime(2004, 06, 01)),
                            new Room("Bathroom", 5, 13, 2, 3, "Blue", new DateTime(2012, 06, 01)),
                            new Room("Kitchen", 10, 11, 4, 2, "Black", new DateTime(2009, 06, 01)),
                            new Room("Living Room", 12, 1, 4, 0, "White", new DateTime(2020, 06, 01))
                        }.ToList()
                    },
                    new Floor
                    {
                        Level = 2, Rooms = new[]
                        {
                            new Room("Corridor", 5, null, 0, 0, "Brown", new DateTime(2004, 06, 01)),
                            new Room("Bathroom", 5, 21, 2, 3, "Blue", new DateTime(2012, 06, 01)),
                            new Room("Bathroom", 10, 22, 2, 3, "Blue", new DateTime(2012, 06, 01)),
                            new Room("Bedroom", 25, 23, 2, 0, "Green", new DateTime(2014, 06, 01)),
                            new Room("Bedroom", 20, 24, 2, 0, "Red", new DateTime(2014, 06, 01)),
                            new Room("Nursery", 10, 25, 2, 0, "Pink", new DateTime(2014, 06, 01)),
                            new Room("Nursery", 15, 26, 2, 0, "Light Blue", new DateTime(2014, 06, 01))
                        }.ToList()
                    },
                }.ToList(),
                Garage = new Garage(),
                Pool = new Pool(),
                Garden = new Garden()
            };

            Neighbourhood neighbourhood = TestNeighbourhood.Create().WithHouses(luxuryHouse, simpleHouse);

            // Act
            var largestHouse = new HouseFinder(neighbourhood).FindLargestHouse();

            // Assert
            largestHouse.Should().Be(luxuryHouse);
        }
    }
}