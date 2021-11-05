using System.Collections.Generic;
using System.Linq;

namespace FluentHouse.Tests.Extensions
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

        public static House CreateSimpleHouse()
        {
            return Create().WithFloors(TestFloor.Create(1).WithRoom(null, 20).WithRoom(null, 10).WithRoom(null, 5));
        }

        public static House CreateComplexHouse(int numberOfFloors, int roomsPerFloor)
        {
            var floors = new List<Floor>();

            for (int i = 0; i < numberOfFloors; i++)
                floors.Add(TestFloor.Create(i).WithRooms(TestRooms.Create(roomsPerFloor).ToArray()));

            return Create().WithFloors(floors.ToArray())
                .WithGarage().WithPool().WithGarden();
        }
    }
}
