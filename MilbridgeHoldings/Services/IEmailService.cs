using MilbridgeHoldings.Models.Data.Local;

namespace MilbridgeHoldings.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(EmailMessage message);
    }
}
