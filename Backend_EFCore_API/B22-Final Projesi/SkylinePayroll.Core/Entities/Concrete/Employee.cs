using SkylinePayroll.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Core.Entities.Concrete
{
    public class Employee : BaseEntity, IEntity
    {
        [Required]
        public string IdentityNumber { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime HireDate { get; set; } = DateTime.Now;
        [Required]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;
        public int UserId { get; set; } 
        public User User { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
        public ICollection<Termination> Terminations { get; set; }
        public string Iban { get; set; }
    }
}
