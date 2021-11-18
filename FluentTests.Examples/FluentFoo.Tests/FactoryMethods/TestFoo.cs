using System.Collections.Generic;
using System.Linq;

namespace FluentFoo.Tests.FactoryMethods
{
    // using Create... factory methods
    public static class TestFoo
    {
        public const double Property3Default = 1.23; // frequently used sample values as public constants
        public static Foo Create() => Create("A"); // shortcut for tests where all properties are irrelevant
        public static Foo Create(string property1, int? optionalProperty2 = null, IEnumerable<Bar> bars = null)
        {
            bars ??= new[] { TestBar.Create() };
            return new Foo
            {
                Property1 = property1,
                Property2 = optionalProperty2 ?? 1, // default value can be defined here or as const
                Property3 = Property3Default, // not relevant in tests (yet)
                Bars = bars.ToList()
            };
        }
    }
}
