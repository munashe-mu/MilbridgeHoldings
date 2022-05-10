using Microsoft.AspNetCore.Identity;

namespace MilbridgeHoldings.Models.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string? FullName { get; set; }
    }
}
