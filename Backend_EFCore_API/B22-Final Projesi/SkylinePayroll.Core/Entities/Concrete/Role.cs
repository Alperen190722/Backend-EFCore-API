using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Concrete
{
    public class Role : BaseEntity, IEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        [Required]
        public decimal MinSalary { get; set; }
        [Required]
        public decimal MaxSalary { get; set; }
        public int HierarchyLevel { get; set; }
    }
}
