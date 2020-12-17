namespace FluentHouse.Tests.Extensions
{
    public static class TestNeighbourhood
    {
        public static Neighbourhood Create()
        {
            return new Neighbourhood();
        }

        public static Neighbourhood WithHouses(this Neighbourhood neighbourhood, params House[] houses)
        {
            foreach (var house in houses)
                neighbourhood.AddHouse(house);

            return neighbourhood;
        }
    }
}