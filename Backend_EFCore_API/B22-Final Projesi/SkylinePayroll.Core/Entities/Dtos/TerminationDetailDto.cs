using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class TerminationDetailDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal TotalAmount { get; set; } 
        public string Iban { get; set; } 
        public string Reason { get; set; } 
        public DateTime HrApprovalDate { get; set; }                                     
        public string Status { get; set; }
    }
}
