using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class NotificationViewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string NotificationType { get; set; } 
        public int? TargetActionId { get; set; }    
        public int? TargetUserId { get; set; }      
        public int? TargetDepartmentId { get; set; } 
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
