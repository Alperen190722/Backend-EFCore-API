using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities
{
    public interface IUserDetailed
    {
        int Id { get; }
        public int EmployeeId { get; set; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        int DepartmentId { get; }
        string DepartmentName { get; }
        public int Status { get; set; }
        public string RoleName { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
