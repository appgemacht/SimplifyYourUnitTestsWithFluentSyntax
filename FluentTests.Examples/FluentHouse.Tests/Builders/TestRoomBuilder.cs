using System;

namespace FluentHouse.Tests.Builders
{
    public class TestRoomBuilder
    {
        private readonly TestFloorBuilder _parent;
        private readonly Random _random = new Random();

        public TestRoomBuilder()
        {
        }

        public TestRoomBuilder(TestFloorBuilder parent)
        {
            _parent = parent;
        }

        private string Name { get; set; }
        private int? Size { get; set; }
        private int? RoomNr { get; set; }
        private int? NumberOfWallSockets { get; set; }
        private int? NumberOfWaterSupplies { get; set; }
        private string Color { get; set; }
        private DateTime? RenovatedDate { get; set; }
        private string Occupant { get; set; }
        private string Comment { get; set; }
        private DateTime? CommentDate { get; set; }

        public TestRoomBuilder With(string name = null, int? size = null, int? roomNr = null, int? numberOfWallSockets = null, int? numberOfWaterSupplies = null, string color = null, DateTime? renovatedDate = null)
        {
            Name = name ?? Name;
            Size = size ?? Size;
            RoomNr = roomNr ?? RoomNr;
            NumberOfWallSockets = numberOfWallSockets ?? NumberOfWallSockets;
            NumberOfWaterSupplies ??= numberOfWaterSupplies ?? NumberOfWaterSupplies;
            Color = color ?? Color;
            RenovatedDate = renovatedDate ?? RenovatedDate;
            return this;
        }

        public TestRoomBuilder WithName(string name)
        {
            Name = name;
            return this;
        }

        public TestRoomBuilder WithSize(int size)
        {
            Size = size;
            return this;
        }

        public TestRoomBuilder WithComment(string comment, DateTime commentDate)
        {
            Comment = comment;
            CommentDate = commentDate;
            return this;
        }

        public TestRoomBuilder WithOccupant(string occupant)
        {
            Occupant = occupant;
            return this;
        }

        public TestFloorBuilder Add()
        {
            if (_parent == null) throw new InvalidOperationException("Call Build()");
            _parent.AddRoom(CreateRoom());
            return _parent;
        }

        public Room Build()
        {
            if (_parent != null) throw new InvalidOperationException("Call Add()");
            return CreateRoom();
        }

        private Room CreateRoom()
        {
            return new Room(
                Name ?? GetRandomRoomName(),
                Size ?? _random.Next(100),
                RoomNr ?? _random.Next(0, 1000),
                NumberOfWallSockets ?? _random.Next(0, 4),
                NumberOfWaterSupplies ?? _random.Next(3),
                Color ?? GetRandomColor(),
                RenovatedDate ?? new DateTime(_random.Next(1990, 2021), 06, 01))
            {
                Comment = Comment,
                CommentDate = CommentDate,
                Occupant = Occupant
            };
        }

        private string GetRandomRoomName()
        {
            string[] roomNames = { "Living Room", "Bathroom", "Kitchen", "Bedroom", "Nursery", "Dining Room", "Corridor", "Basement" };
            return roomNames[_random.Next(roomNames.Length)];
        }

        private string GetRandomColor()
        {
            string[] roomNames = { "Blue", "Red", "White", "Black", "Pink", "Light Blue", "Green" };
            return roomNames[_random.Next(roomNames.Length)];
        }
    }
}