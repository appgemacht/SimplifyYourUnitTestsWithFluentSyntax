using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentHouse.Tests.Builders
{
    public class TestFloorBuilder
    {
        private readonly TestHouseBuilder _parent;

        public TestFloorBuilder(TestHouseBuilder parent, int level)
        {
            _parent = parent;
            Level = level;
        }

        public TestFloorBuilder(int level)
        {
            Level = level;
        }

        public TestFloorBuilder SetRooms(params Room[] rooms)
        {
            Rooms = rooms.ToList();
            return this;
        }

        public TestRoomBuilder AddRoom() => new TestRoomBuilder(this);

        public TestFloorBuilder AddRoom(Room room)
        {
            Rooms.Add(room);
            return this;
        }

        public TestFloorBuilder AddRoom(string name = null, int? size = null)
        {
            AddRoom(new TestRoomBuilder().With(name, size).Build());
            return this;
        }

        public TestHouseBuilder Add()
        {
            if (_parent == null) throw new InvalidOperationException("Call Build()");
            _parent.AddFloor(CreateFloor());
            return _parent;
        }

        public Floor Build()
        {
            if (_parent != null) throw new InvalidOperationException("Call Add()");
            return CreateFloor();
        }

        private Floor CreateFloor() => new Floor {Level = Level, Rooms = Rooms};

        private int Level { get; }
        private List<Room> Rooms { get; set; } = new List<Room>();
    }
}