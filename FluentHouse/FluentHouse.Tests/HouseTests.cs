using FluentAssertions;
using System;
using Xunit;

namespace FluentHouse.Tests
{
    public class HouseFinderTests
    {
        [Fact]
        public void FindLargestRoom_WhenOnlyOneLargestRoomExist()
        {
            // Arrange
            House house = TestHouse.Create().WithFloors(
                    TestFloor.Create(level: 1).WithRoom("Kitchen", size: 10).WithRoom("Living Room", size: 40),
                    TestFloor.Create(level: 2).WithRoom("Bathroom", size: 5).WithRoom("Bedroom", size: 10))
                .WithGarage().WithPool().WithGarden();

            // Act
            (int level, string roomName) = new RoomFinder(house).FindForFriendsMeetup();

            // Assert
            level.Should().Be(1);
            roomName.Should().Be("Living Room");
        } 
    }
}
