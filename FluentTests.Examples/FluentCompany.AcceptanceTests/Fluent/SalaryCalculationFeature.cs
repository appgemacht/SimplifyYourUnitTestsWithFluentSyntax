using FluentAssertions;
using System;
using Xbehave;

namespace FluentCompany.AcceptanceTests.Fluent
{
    public class SalaryCalculationFeature
    {
        [Scenario]
        public void CalculateSalary(Employee employee, Company company, double salary)
        {
            "Given a company working Mo-Fr between 08:00-12:00 and 13:00-17:00"
                .x(() => company = TestCompany.Working()
                    .On(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday)
                    .Daily.From(08, 00).To(12,00)
                    .Daily.From(13, 00).To(17,00).Company);

            "And an employee payed 30$ per hour"
                .x(() => employee = TestEmployee.Create()
                    .WithContract(TestEmployeeContract.For(company).Earning(30).Per(TimeSpan.FromHours(1))));

            "When the employee has worked November 2021"
                .x(() => salary = SalaryCalculator
                    .CalculateSalary(employee, DateTime.Parse("2021-11-01"), DateTime.Parse("2021-12-01")));

            "Then his salary is "
                .x(() => salary.Should().Be(5280));
        }
    }
}
