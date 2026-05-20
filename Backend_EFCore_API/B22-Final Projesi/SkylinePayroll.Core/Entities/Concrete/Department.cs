using SkylinePayroll.Core.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Concrete
{
    public class Department : BaseEntity, IEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
