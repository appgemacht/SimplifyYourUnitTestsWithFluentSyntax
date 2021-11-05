using System;

namespace FluentCompany.AcceptanceTests
{
    public static class TestEmployeeContract
    {
        public static EmploymentContract For(Company company) =>
            new EmploymentContract { Company = company };
        public static EmploymentContract Earning(this EmploymentContract contract, int salaryPerUnit)
        {
            contract.SalaryPerUnit = salaryPerUnit;
            return contract;
        }

        public static EmploymentContract Per(this EmploymentContract contract, TimeSpan salaryUnit)
        {
            contract.SalaryUnit = salaryUnit;
            return contract;
        }
    }
}