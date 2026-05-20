using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string SalaryDisplay { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public string Iban { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
