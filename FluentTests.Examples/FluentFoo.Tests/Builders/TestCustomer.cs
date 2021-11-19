namespace FluentFoo.Tests.Builders
{
    public class TestCustomer
    {
        private readonly Customer _customer = new();

        public TestCustomer Named(string firstName, string lastName)
        {
            _customer.FirstName = firstName;
            _customer.LastName = lastName;
            return this;
        }

        public TestCustomer LivingAt(string address, int zipCode, string town)
        {
            _customer.Address = address;
            _customer.ZipCode = zipCode;
            _customer.Town = town;
            return this;
        }

        public Customer Build() => _customer;

        public static Customer Create() => new TestCustomer()
            .Named("Hans", "Muster")
            .LivingAt("Bahnhofstr. 1", 3000, "Bern")
            .Build();
    }
}
