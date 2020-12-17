using System.Collections.Generic;

namespace FluentHouse
{
    public class Neighbourhood
    {
        private readonly List<House> _houses = new List<House>();

        public IEnumerable<House> Houses => _houses;

        public void AddHouse(House house) => _houses.Add(house);
    }
}