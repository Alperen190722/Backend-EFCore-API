using SkylinePayroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class UserDetailDto : IUserDetailed
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int EmployeeId { get; set; }
        public int Status { get; set; }
        public string RoleName { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
