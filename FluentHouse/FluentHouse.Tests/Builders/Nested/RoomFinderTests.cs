using FluentAssertions;
using Xunit;

namespace FluentHouse.Tests.Builders.Nested
{
    public class RoomFinderTests
    {
        [Fact]
        public void FindRoomForFriendsMeetup_WhenOnlyOneLargestRoomExist_ThenReturnLargestRoom()
        {
            // Arrange
            House house = new TestHouseBuilder()
                .AddFloor(level: 1)
                    .AddRoom().With("Kitchen", size: 10).Add()
                    .AddRoom().With("Living Room", size: 40).Add()
                .Add()
                .AddFloor(level: 2)
                    .AddRoom().With("Bathroom", size: 5).Add()
                    .AddRoom().With("Bedroom", size: 10).Add()
                .Add()
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
            House house = new TestHouseBuilder()
                .AddFloor(level: -1)
                    .AddRoom("Basement", size: 60).Add()
                .AddFloor(level: 1)
                    .AddRoom("Kitchen", size: 10).AddRoom("Living Room", size: 40).Add()
                .Build();

            // Act
            (int level, string roomName) = new RoomFinder(house).FindRoomForFriendsMeetup();

            // Assert
            level.Should().Be(1);
            roomName.Should().Be("Living Room");
        }
    }
}
