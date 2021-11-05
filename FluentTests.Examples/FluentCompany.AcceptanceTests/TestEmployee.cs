namespace FluentCompany.AcceptanceTests
{
    public static class TestEmployee
    {
        public static Employee Create() => new Employee();
        public static Employee WithContract(this Employee employee, EmploymentContract contract)
        {
            employee.Contract = contract;
            return employee;
        }
    }
}