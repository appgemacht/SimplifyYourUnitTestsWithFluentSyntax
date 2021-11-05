using FluentAssertions;
using System;
using System.Linq;
using Xbehave;

namespace FluentCompany.AcceptanceTests.Classic
{
    public class SalaryCalculationFeature
    {
        [Scenario]
        public void CalculateSalary(Employee employee, Company company, double salary)
        {
            "Given a company working Mo-Fr between 08:00-12:00 and 13:00-17:00"
                .x(() =>
                {
                    company = new Company
                    {
                        BusinessHours = new[]
                        {
                            new TimeSpan(12, 00, 00) - new TimeSpan(08, 00, 00),
                            new TimeSpan(17, 00, 00) - new TimeSpan(13, 00, 00)
                        }.ToList(),
                        WorkingDays = new[]
                        {
                            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday
                        },
                    };
                });

            "And an employee payed 30$ per hour"
                .x(() =>
                {
                    employee = new Employee
                    {
                        Contract = new EmploymentContract
                        {
                            Company = company,
                            Employee = employee,
                            SalaryPerUnit = 30,
                            SalaryUnit = TimeSpan.FromHours(1)
                        }
                    };
                });

            "When the employee has worked November 2021"
                .x(() =>
                {
                    salary = SalaryCalculator.CalculateSalary(employee, DateTime.Parse("2021-11-01"), DateTime.Parse("2021-12-01"));
                });

            "Then his salary is "
                .x(() =>
                {
                    salary.Should().Be(5280);
                });
        }
    }
}
