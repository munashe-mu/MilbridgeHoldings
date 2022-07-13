using MilbridgeHoldings.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace MilbridgeHoldings.Models.Data
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string? EmployeeNumber { get; set; }
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        [ForeignKey("JobTitleId")]
        public JobTitle? JobTitle { get; set;}
    }
}
