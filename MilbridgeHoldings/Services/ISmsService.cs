using ModelLibrary.Models.Local;

namespace ModelLibrary.Services
{
    public interface ISmsService
    {
        Task<string> SendMessageAsync(SendMessageRequest messageRequest);
    }
}
