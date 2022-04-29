using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilbridgeHoldings.Data
{
    public class Machines
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double HoursWorked { get; set; }
        public int WorkCentreId { get; set; }
    }
}
