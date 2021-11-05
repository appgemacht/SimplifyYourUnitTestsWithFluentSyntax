using System;

namespace FluentCompany.AcceptanceTests
{
    public class TestCompany
    {
        private TestCompany() => Company = new Company();
        public Company Company { get; }

        public static TestCompany Working() => new TestCompany();
        public TestCompany On(params DayOfWeek[] daysOfWeek)
        {
            Company.WorkingDays = daysOfWeek;
            return this;
        }

        public TestBusinessHours Daily => new TestBusinessHours(this);
    }
}