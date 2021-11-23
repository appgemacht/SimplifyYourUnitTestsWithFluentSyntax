using System.Collections.Generic;

namespace FluentFoo
{
    public class Foo
    {
        public Foo() { }
        public Foo(string property1, int property2, double property3)
        {
            Property1 = property1;
            Property2 = property2;
            Property3 = property3;
        }

        public string Property1 { get; set; }
        public int Property2 { get; set; }
        public double Property3 { get; set; }
        public List<Bar> Bars { get; set; } = new();
    }
}
