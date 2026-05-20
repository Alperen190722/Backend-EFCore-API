using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class DashboardSummaryDto
    {
        public int TotalActiveEmployees { get; set; } 
        public int PendingPayrolls { get; set; } 
        public decimal TotalMonthlyCost { get; set; } 
        public string LastProcessedMonth { get; set; } 
    }
}
