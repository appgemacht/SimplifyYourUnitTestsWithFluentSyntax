using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace FluentHouse.Tests.Classic
{
    public class RoomFinderTests
    {
        [Fact]
        public void FindRoomForFriendsMeetup_WhenOnlyOneLargestRoomExist_ThenReturnLargestRoom2()
        {
            // Arrange
            House house = new House
            {
                Floors = new[]
                {
                    new Floor { Level = 1, Rooms = new[]
                    {
                        new Room("Kitchen", 10, 2, 4, 2, "Black", new DateTime(2009, 06, 01)),
                        new Room("Living Room", 40, 1, 4, 0, "White", new DateTime(2020, 06, 01))
                    }.ToList()},
                    new Floor { Level = 2, Rooms = new[]
                    {
                        new Room("Bathroom", 5, 2, 2, 3, "Blue", new DateTime(2012, 06, 01)),
                        new Room("Bedroom", 10, 23, 2, 0, "Green", new DateTime(2014, 06, 01))
                    }.ToList()},
                }.ToList(),
                Garage = new Garage(),
                Pool = new Pool(),
                Garden = new Garden()
            };

            // Act
            (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

            // Assert
            level.Should().Be(1);
            roomName.Should().Be("Living Room");
        }
    }
}
