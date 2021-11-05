using System;
using System.Linq;

namespace FluentCompany
{
    public class SalaryCalculator
    {
        public static double CalculateSalary(Employee employee, DateTime from, DateTime to)
        {
            var workingDays = GetWorkingDays(from, to, employee.Contract.Company.WorkingDays);
            var workingHours = workingDays * (employee.Contract.Company.BusinessHours.Sum(h => h.TotalHours));
            var salaryPerHour = employee.Contract.SalaryPerUnit / employee.Contract.SalaryUnit.TotalHours;
            return workingHours * salaryPerHour;
        }

        internal static int GetWorkingDays(DateTime from, DateTime to, DayOfWeek[] workingDays)
        {
            var totalDays = 0;
            for (var date = from; date < to; date = date.AddDays(1))
            {
                if (workingDays.Contains(date.DayOfWeek))
                    totalDays++;
            }

            return totalDays;
        }
    }
}