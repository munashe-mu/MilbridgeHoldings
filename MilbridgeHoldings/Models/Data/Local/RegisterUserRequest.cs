namespace MilbridgeHoldings.Models.Data.Local
{
    public class RegisterUserRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Email { get; set; }
    }
}
