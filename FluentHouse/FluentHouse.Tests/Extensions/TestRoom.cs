using System;

namespace FluentHouse.Tests.Extensions
{
    public static class TestRoom
    {
        public static Room Create(string name = null, int? size = null)
        {
            return new Room
            {
                Name = name ?? CreateRoomName(), 
                Size = size ?? new Random().Next(100)
            };
        }

        public static string CreateRoomName()
        {
            string[] roomNames = {"Living Room", "Bathroom", "Kitchen", "Bedroom", "Nursery", "Dining Room", "Corridor", "Basement"};
            return roomNames[new Random().Next(roomNames.Length)];
        }
    }
}