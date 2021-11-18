namespace FluentFoo.Tests.Builders
{
    public class TestBar
    {
        private readonly TestFoo _parent;
        private string _property1;

        public TestBar() { }

        public TestBar(TestFoo parent) => _parent = parent;

        public TestBar WithBarProperty(string property1)
        {
            _property1 = property1;
            return this;
        }

        public Bar Build() => new Bar(_property1);

        public TestFoo Add() => _parent.AddBar(Build());
    }
}
