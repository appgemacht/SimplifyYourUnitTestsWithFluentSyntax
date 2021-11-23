using System;

namespace FluentHouse.Tests.Extensions
{
    public static class TestRoom
    {
        private static readonly Random Random = new Random();

        public static Room Create(
            string name = null,
            int? size = null,
            int? roomNr = null,
            int? numberOfWallSockets = null,
            int? numberOfWaterSupplies = null,
            string color = null,
            DateTime? renovatedDate = null)
        {
            return new Room(
                name ?? GetRandomRoomName(),
                size ?? Random.Next(100),
                roomNr ?? Random.Next(0, 1000),
                numberOfWallSockets ?? Random.Next(0, 4),
                numberOfWaterSupplies ?? Random.Next(3),
                color ?? GetRandomColor(),
                renovatedDate ?? new DateTime(Random.Next(1990, 2021), 06, 01));
        }

        public static Room WithComment(this Room room, string comment, DateTime commentDate)
        {
            room.Comment = comment;
            room.CommentDate = commentDate;
            return room;
        }

        public static Room WithOccupant(this Room room, string occupant)
        {
            room.Occupant = occupant;
            return room;
        }

        private static string GetRandomRoomName()
        {
            string[] roomNames = { "Living Room", "Bathroom", "Kitchen", "Bedroom", "Nursery", "Dining Room", "Corridor", "Basement" };
            return roomNames[Random.Next(roomNames.Length)];
        }

        private static string GetRandomColor()
        {
            string[] roomNames = { "Blue", "Red", "White", "Black", "Pink", "Light Blue", "Green" };
            return roomNames[Random.Next(roomNames.Length)];
        }
    }
}