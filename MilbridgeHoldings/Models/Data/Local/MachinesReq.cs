namespace MilbridgeHoldings.Models.Data.Local
{
    public class MachinesReq
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double HoursWorked { get; set; }
        public int WorkCentreId { get; set; }
    }
}
