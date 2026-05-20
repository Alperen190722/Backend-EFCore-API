
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Abstract
{
    public interface INotificationService
    {
        
        Task SendNotificationAsync(string message, string type, int? targetActionId, int? targetUserId = null, int? targetDepartmentId = null);
        Task<Result<string>> ClearNotificationAsync(int notificationId);
        Task<Result<string>> MarkNotificationAsReadAsync(int notificationId);
        Task<List<NotificationViewDto>> GetMyNotificationsAsync(int userId, int departmentId);
        Task<Result<string>> ClearNotificationAsync(int targetActionId, string type);
    }
}
