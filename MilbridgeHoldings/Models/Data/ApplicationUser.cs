using Microsoft.AspNetCore.Identity;

namespace MilbridgeHoldings.Models.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string? FullName { get; set; }
        public int DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string? EmployeeNumber { get; set; }
    }
}
