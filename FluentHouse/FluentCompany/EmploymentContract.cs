using System;

namespace FluentCompany
{
    public class EmploymentContract
    {
        public Employee Employee { get; set; }
        public Company Company { get; set; }
        public int SalaryPerUnit { get; set; }
        public TimeSpan SalaryUnit { get; set; }
    }
}