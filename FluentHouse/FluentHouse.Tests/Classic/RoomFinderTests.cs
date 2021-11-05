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
                    new Floor
                    {
                        Level = 1,
                        Rooms = new[]
                        {
                            new Room("Kitchen",
                                size: 10,
                                roomNr: 2,
                                numberOfWallSockets: 4,
                                numberOfWaterSupplies: 2,
                                color: "Black",
                                renovatedDate: new DateTime(2009, 06, 01)),
                            new Room("Living Room",
                                size: 40,
                                roomNr: 1,
                                numberOfWallSockets: 4,
                                numberOfWaterSupplies: 0,
                                color: "White",
                                renovatedDate: new DateTime(2020, 06, 01))
                        }.ToList()
                    },
                    new Floor
                    {
                        Level = 2,
                        Rooms = new[]
                        {
                            new Room("Bathroom",
                                size: 5,
                                roomNr: 2,
                                numberOfWallSockets: 2,
                                numberOfWaterSupplies: 3,
                                color: "Blue",
                                renovatedDate: new DateTime(2012, 06, 01)),
                            new Room("Bedroom",
                                size: 10,
                                roomNr: 23,
                                numberOfWallSockets: 2,
                                numberOfWaterSupplies: 0,
                                color: "Green",
                                renovatedDate: new DateTime(2014, 06, 01))
                        }.ToList()
                    },
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
