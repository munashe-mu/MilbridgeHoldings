using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data
{
    public class EmployeeInfo
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int? JobTitleId { get; set; }
        public string? EmployeeNumber { get; set; }

        public virtual Department? Department { get; set; }  
        public virtual JobTitle? JobTitle { get; set;}
        
    }
}
