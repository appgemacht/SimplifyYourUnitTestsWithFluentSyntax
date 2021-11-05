using System.Linq;

namespace FluentHouse
{
    public class HouseFinder
    {
        private readonly Neighbourhood _neighbourhood;

        public HouseFinder(Neighbourhood neighbourhood)
        {
            _neighbourhood = neighbourhood;
        }

        public House FindLargestHouse()
        {
            return _neighbourhood.Houses.OrderByDescending(h => h.Floors.SelectMany(f => f.Rooms).Sum(r => r.Size)).FirstOrDefault();
        }
    }
}