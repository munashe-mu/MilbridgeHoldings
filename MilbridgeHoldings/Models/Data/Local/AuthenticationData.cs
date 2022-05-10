namespace MilbridgeHoldings.Models.Data.Local
{
    public class AuthenticationData
    {
        public string? Token { get; set; }
        public string? UserId { get; set; }
        public IList<string>? Roles { get; set; }
        public string? UserName { get; set; }
        public IList<string>? Permissions { get; set; }
    }
}
