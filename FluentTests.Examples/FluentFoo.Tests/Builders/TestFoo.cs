using FluentFoo.Tests.Extensions;
using System.Collections.Generic;

namespace FluentFoo.Tests.Builders
{
    public class TestFoo
    {
        public const double Property3Default = 1.23; // frequently used sample values as public constants

        private string _property1 = "A";
        private int _property2 = 1;
        private double _property3 = Property3Default;
        private readonly List<Bar> _bars = new();

        public TestFoo WithPropertyGroup1(string property1)
        {
            _property1 = property1;
            return this;
        }

        public TestFoo WithPropertyGroup2(int property2, double? optionalProperty3 = null)
        {
            _property2 = property2;
            _property3 = optionalProperty3 ?? Property3Default;
            return this;
        }

        // continuous method chaining: TestFoo...AddBar().WithBarProperty(..).Add()
        public TestBar AddBar() => new TestBar(this);

        // interrupted method chaining: TestFoo...AddBar(new TestBar().WithBarProperty(..).Build())
        public TestFoo AddBar(Bar bar)
        {
            _bars.Add(bar);
            return this;
        }

        // shortcuts for frequently used variants
        public static Foo AsVariantX() => new TestFoo().WithPropertyGroup1("X").WithPropertyGroup2(123, 12.34).Build();
        public static Foo AsVariantY() => new TestFoo().WithPropertyGroup1("Y").WithPropertyGroup2(234).Build();

        // the builder syntax is useful when productive class (partly) immutable
        public Foo Build() => new Foo(_property1, _property2, _property3) { Bars = _bars };
    }
}
