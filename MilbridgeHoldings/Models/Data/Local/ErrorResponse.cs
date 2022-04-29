namespace MilbridgeHoldings.Models.Data.Local
{
    public class ErrorResponse
    {
        public ErrorResponse(string message)
        {
            Message = message;
        }
        public string? Message { get; set; }
    }
}
