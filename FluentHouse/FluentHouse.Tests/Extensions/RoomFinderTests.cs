using FluentAssertions;
using Xunit;

namespace FluentHouse.Tests.Extensions
{
    public class RoomFinderTests
    {
        [Fact]
        public void FindRoomForFriendsMeetup_WhenOnlyOneLargestRoomExist_ThenReturnLargestRoom()
        {
            // Arrange
            House house = TestHouse.Create().WithFloors(
                    TestFloor.Create(level: 1).WithRoom("Kitchen", size: 10).WithRoom("Living Room", size: 40),
                    TestFloor.Create(level: 2).WithRoom("Bathroom", size: 5).WithRoom("Bedroom", size: 10))
                .WithGarage().WithPool().WithGarden();

            // Act
            (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

            // Assert
            level.Should().Be(1);
            roomName.Should().Be("Living Room");
        }


        [Fact]
        public void FindRoomForFriendsMeetup_WhenTheLargestRoomIsInBasement_ThenReturnSecondLargestRoom()
        {
            // Arrange
            House house = TestHouse.Create().WithFloors(
                    TestFloor.Create(level: -1).WithRoom("Basement", size: 60),
                    TestFloor.Create(level: 1).WithRoom("Kitchen", size: 10).WithRoom("Living Room", size: 40));

            // Act
            (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

            // Assert
            level.Should().Be(1);
            roomName.Should().Be("Living Room");
        }
    }
}
