using FluentAssertions;
using Xunit;

namespace FluentHouse.Tests.Builders
{
    public class RoomFinderTests
    {
        [Fact]
        public void FindRoomForFriendsMeetup_WhenOnlyOneLargestRoomExist_ThenReturnLargestRoom()
        {
            // Arrange
            House house = new TestHouseBuilder().SetFloors(
                    new TestFloorBuilder(level: 1).AddRoom("Kitchen", size: 10).AddRoom("Living Room", size: 40).Build(),
                    new TestFloorBuilder(level: 2).AddRoom("Bathroom", size: 5).AddRoom("Bedroom", size: 10).Build())
                .WithGarage().WithPool().WithGarden()
                .Build();

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
            House house = new TestHouseBuilder().SetFloors(
                    new TestFloorBuilder(level: -1).AddRoom("Basement", size: 60).Build(),
                    new TestFloorBuilder(level: 1).AddRoom("Kitchen", size: 10).AddRoom("Living Room", size: 40).Build())
                .Build();

            // Act
            (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

            // Assert
            level.Should().Be(1);
            roomName.Should().Be("Living Room");
        }
    }
}
