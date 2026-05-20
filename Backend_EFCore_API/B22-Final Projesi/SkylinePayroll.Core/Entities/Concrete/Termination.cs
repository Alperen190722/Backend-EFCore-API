using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Core.Entities.Concrete
{
    public class Termination : BaseEntity, IEntity
    {
        public int EmployeeId { get; set; }
        public DateTime NoticeDate { get; set; } = DateTime.Now; 
        public DateTime? TerminationDate { get; set; } 
        public decimal SeverancePay { get; set; } 
        public decimal NoticePay { get; set; }    
        public string Reason { get; set; }        
        public TerminationStatus Status { get; set; } = TerminationStatus.ManagerApproved;
        public Employee Employee { get; set; }
        public TerminationType Type { get; set; }
        public decimal CalculatedAmount { get; set; }
        public DateTime HrApprovalDate { get; set; } 
    }
}
