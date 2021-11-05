using System.Collections.Generic;
using System.Linq;
using FluentHouse.Tests.Extensions;

namespace FluentHouse.Tests.Builders
{
    public class TestHouseBuilder
    {
        public TestHouseBuilder SetFloors(params Floor[] floors)
        {
            Floors = floors.ToList();
            return this;
        }

        public TestFloorBuilder AddFloor(int level)
        {
            return new TestFloorBuilder(this, level);
        }

        public TestHouseBuilder AddFloor(Floor floor)
        {
            Floors.Add(floor);
            return this;
        }

        public TestHouseBuilder WithGarden()
        {
            Garden = new Garden();
            return this;
        }

        public TestHouseBuilder WithPool()
        {
            Pool = new Pool();
            return this;
        }

        public TestHouseBuilder WithGarage()
        {
            Garage = new Garage();
            return this;
        }

        public TestHouseBuilder CreateSimpleHouse()
        {
            return SetFloors(TestFloor.Create(1).WithRoom(null, 20).WithRoom(null, 10).WithRoom(null, 5));
        }

        public TestHouseBuilder CreateComplexHouse(int numberOfFloors, int roomsPerFloor)
        {
            var floors = new List<Floor>();

            for (int i = 0; i < numberOfFloors; i++)
                floors.Add(TestFloor.Create(i).WithRooms(TestRooms.Create(roomsPerFloor).ToArray()));

            return SetFloors(floors.ToArray())
                .WithGarage().WithPool().WithGarden();
        }

        public House Build()
        {
            return new House { Floors = Floors, Garage = Garage, Garden = Garden, Pool = Pool };
        }

        private List<Floor> Floors { get; set; }
        private Garage Garage { get; set; }
        private Garden Garden { get; set; }
        private Pool Pool { get; set; }
    }
}