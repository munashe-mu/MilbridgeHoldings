using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilbridgeHoldings.Data
{
    public class WorkCentre
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public int DepartmentId { get; set; } 
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
    }
}
