namespace MilbridgeHoldings.Models.Data.Local
{
    public class RegisterUserRequest
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}
