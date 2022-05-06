namespace MilbridgeHoldings.Models.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int ? JobTitleId { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
