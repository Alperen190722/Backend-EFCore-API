using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class ResignationDto
    {
        public int EmployeeId { get; set; }
        public string Reason { get; set; }
        public int DepartmentId { get; set; }
    }
}
