using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class UserForRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public int RoleId { get; set; }
        public string Gender { get; set; }
        public string Iban { get; set; }
    }
}
