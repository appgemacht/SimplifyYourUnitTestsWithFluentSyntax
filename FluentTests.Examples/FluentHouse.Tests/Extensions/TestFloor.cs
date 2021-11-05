namespace FluentHouse.Tests.Extensions
{
    public static class TestFloor
    {
        public static Floor Create(int level)
        {
            return new Floor { Level = level };
        }

        public static Floor WithRooms(this Floor floor, params Room[] rooms)
        {
            floor.Rooms.AddRange(rooms);
            return floor;
        }

        public static Floor WithRoom(this Floor floor, string name = null, int? size = null)
            => floor.WithRoom(TestRoom.Create(name, size));

        public static Floor WithRoom(this Floor floor, Room room)
        {
            floor.Rooms.Add(room);
            return floor;
        }
    }
}