using System;
using System.Collections.Generic;

namespace FluentCompany
{
    public class Company
    {
        public DayOfWeek[] WorkingDays { get; set; } = { };
        public List<TimeSpan> BusinessHours { get; set; } = new();
    }
}
