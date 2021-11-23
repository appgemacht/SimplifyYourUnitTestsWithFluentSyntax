using System.Linq;

namespace FluentHouse
{
    public class RoomFinder
    {
        private readonly House _house;

        public RoomFinder(House house)
        {
            _house = house;
        }

        public (int level, string roomName) FindRoomForFriendsMeetup()
        {
            var largestRoom = _house.Floors.Where(f => f.Level >= 0)
                .SelectMany(f => f.Rooms)
                .OrderByDescending(r => r.Size)
                .First();
            
            var level = _house.Floors
                .Single(f => f.Rooms.Contains(largestRoom))
                .Level;
            
            return (level, largestRoom.Name);
        }
    }
}