using System;

namespace FluentHouse.Tests
{
    public class RoomFinder
    {
        private House house;

        public RoomFinder(House house)
        {
            this.house = house;
        }

        public (int level, string roomName) FindForFriendsMeetup()
        {
            throw new NotImplementedException();
        }
    }
}