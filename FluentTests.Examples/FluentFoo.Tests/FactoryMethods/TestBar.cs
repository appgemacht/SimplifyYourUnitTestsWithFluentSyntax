namespace FluentFoo.Tests.FactoryMethods
{
    public static class TestBar
    {
        public static Bar Create(string property1 = null) => new Bar(property1 ?? "ABC");
    }
}
