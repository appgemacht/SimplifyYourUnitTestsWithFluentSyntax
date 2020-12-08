using System.Linq;

namespace FluentHouse.Tests
{
    public static class TestHouse
    {
        public static House Create()
        {
            return new House();
        }

        public static House WithFloors(this House house, params Floor[] floors)
        {
            house.Floors = floors.ToList();
            return house;
        }

        public static House WithGarden(this House house)
        {
            house.Garden = new Garden();
            return house;
        }

        public static House WithPool(this House house)
        {
            house.Pool = new Pool();
            return house;
        }

        public static House WithGarage(this House house)
        {
            house.Garage = new Garage();
            return house;
        }
    }

    public static class TestFloor
    {
        public static Floor Create(int level)
        {
            return new Floor { Level = level };
        }

        public static Floor WithRoom(this Floor floor, string name, int size)
        {
            floor.Rooms.Add(new Room() { Name = name, Size = size });
            return floor;
        }
    }
}
