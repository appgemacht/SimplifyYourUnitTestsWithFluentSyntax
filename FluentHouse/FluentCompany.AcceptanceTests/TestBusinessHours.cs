using System;

namespace FluentCompany.AcceptanceTests
{
    public class TestBusinessHours
    {
        private readonly TestCompany _parent;
        private TimeSpan _timeFrom;

        public TestBusinessHours(TestCompany parent) => _parent = parent;

        public TestBusinessHours From(int hour, int minute)
        {
            _timeFrom = new TimeSpan(hour, minute, 0);
            return this;
        }

        public TestCompany To(int hour, int minute)
        {
            _parent.Company.BusinessHours.Add(new TimeSpan(hour, minute, 0) - _timeFrom);
            return _parent;
        }
    }
}