using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Concrete
{
    public class Notification : BaseEntity, IEntity
    {
        public string Message { get; set; }
        public int? TargetUserId { get; set; }
        [ForeignKey("TargetUserId")] 
        public virtual User User { get; set; } 
        public int? TargetDepartmentId { get; set; }
        [ForeignKey("TargetDepartmentId")] 
        public virtual Department Department { get; set; }
        public string NotificationType { get; set; }
        public int? TargetActionId { get; set; } 
        public bool IsRead { get; set; }
    }
}
