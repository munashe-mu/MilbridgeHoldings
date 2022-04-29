using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilbridgeHoldings.Data
{
    public class WorkCentre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DepartmentId { get; set; } = string.Empty;
    }
}
