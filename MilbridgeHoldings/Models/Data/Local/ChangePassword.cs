using System.ComponentModel.DataAnnotations;

namespace MilbridgeHoldings.Models.Data.Local
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "User ID is required")]
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Old password is required")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string? NewPassword { get; set; }
    }
}
