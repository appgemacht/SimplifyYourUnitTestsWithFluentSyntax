using FluentFoo.Tests.FactoryMethods;

namespace FluentFoo.Tests.Extensions
{
    // using Create..., WithParamGroup... syntax
    public static class TestFoo2
    {
        private static int _property1Counter = 1;

        // some properties need to be unique or random for each test
        public static string CreateParam1() => $"Property1-{_property1Counter++}";

        public static Foo Create() => new Foo().WithPropertyGroup1().WithPropertyGroup2(123);

        // split into independent groups when properties are often configured/omitted together
        public static Foo WithPropertyGroup1(this Foo foo, string property1 = null)
        {
            foo.Property1 = property1 ?? CreateParam1();
            return foo;
        }

        public static Foo WithPropertyGroup2(this Foo foo, int property2, double? optionalProperty3 = null)
        {
            foo.Property2 = property2;
            foo.Property3 = optionalProperty3 ?? TestFoo.Property3Default;
            return foo;
        }

        // interrupted method chaining: TestFoo...AddBar(TestBar.Create(..))
        public static Foo AddBar(this Foo foo, Bar bar = null)
        {
            foo.Bars.Add(bar ?? TestBar.Create());
            return foo;
        }

        // shortcuts for frequently used variants
        public static Foo CreateAsVariantX() => Create().WithPropertyGroup1("X").WithPropertyGroup2(123, 12.34);
        public static Foo CreateAsVariantY() => Create().WithPropertyGroup1("Y").WithPropertyGroup2(234);
    }
}
