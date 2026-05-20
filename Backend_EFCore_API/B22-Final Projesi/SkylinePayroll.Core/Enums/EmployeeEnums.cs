using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Enums
{
    public class EmployeeEnums
    {
        public enum EmployeeStatus
        {
            Active = 1,          
            PendingTermination = 2,
            Passive = 3,
            PendingResignation = 4 
        }

        public enum TerminationStatus
        {
            ManagerApproved = 1,
            WaitingForEmployee = 2,
            HRProcessed = 3,
            AccountingPaid = 4,
            ResignationSubmitted = 5
        }

        public enum TerminationType
        {
            Dismissal = 1,
            Resignation = 2
        }
    }
}
