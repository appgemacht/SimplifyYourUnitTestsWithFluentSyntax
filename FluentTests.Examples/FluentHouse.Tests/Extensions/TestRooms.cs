using System.Collections.Generic;
using System.Linq;

namespace FluentHouse.Tests.Extensions
{
    public static class TestRooms
    {
        public static IEnumerable<Room> Create(int numberOfRooms)
            => Enumerable.Range(0, numberOfRooms).Select(i => TestRoom.Create());
    }
}