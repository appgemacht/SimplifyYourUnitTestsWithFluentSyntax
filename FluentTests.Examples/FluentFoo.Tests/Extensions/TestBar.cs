namespace FluentFoo.Tests.Extensions
{
    public static class TestBar
    {
        public static Bar Create() => new();

        public static Bar With(this Bar bar, string property1 = null)
        {
            bar.Property1 = property1 ?? "ABC";
            return bar;
        }
    }
}
