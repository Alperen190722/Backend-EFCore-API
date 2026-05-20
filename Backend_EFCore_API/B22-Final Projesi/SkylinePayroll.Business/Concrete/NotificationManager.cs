using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities;
using SkylinePayroll.Data.Concrete.EntityFramework;
using SkylinePayroll.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Concrete
{
    public class NotificationManager : INotificationService
    {
        private readonly SkylineContext _context;
        public NotificationManager(SkylineContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> ClearNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return Result<string>.Error("Bildirim bulunamadı.");

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return Result<string>.Ok(null, "Bildirim kalıcı olarak silindi.");
        }

        public async Task<Result<string>> ClearNotificationAsync(int targetActionId, string type)
        {
            var notifications = await _context.Notifications
                .Where(n => n.TargetActionId == targetActionId && n.NotificationType == type)
                .ToListAsync();

            if (notifications.Any())
            {
                _context.Notifications.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }

            return Result<string>.Ok(null, "Süreç bildirimleri temizlendi.");
        }

        public async Task<List<NotificationViewDto>> GetMyNotificationsAsync(int userId, int departmentId)
        {
            return await _context.Notifications
                .Where(n => (n.TargetUserId == userId) ||
                            (n.TargetDepartmentId == departmentId) ||
                            (n.TargetUserId == null && n.TargetDepartmentId == null))
                .OrderByDescending(n => n.CreatedDate)
                .Select(n => new NotificationViewDto
                {
                    Id = n.Id,
                    Message = n.Message,
                    CreatedDate = n.CreatedDate,
                    IsRead = n.IsRead,
                    NotificationType = n.NotificationType,
                    TargetActionId = n.TargetActionId,
                    TargetUserId = n.TargetUserId,
                    TargetDepartmentId = n.TargetDepartmentId
                })
                .ToListAsync();
        }

        public async Task<Result<string>> MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return Result<string>.Error("Bildirim bulunamadı.");

            notification.IsRead = true;
            await _context.SaveChangesAsync();
            return Result<string>.Ok(null, "Okundu olarak işaretlendi.");
        }

        public async Task SendNotificationAsync(string message, string type, int? targetActionId, int? targetUserId = null, int? targetDepartmentId = null)
        {
            if (targetActionId.HasValue)
            {
                var oldNotifications = await _context.Notifications
                    .Where(n => n.TargetActionId == targetActionId.Value && n.NotificationType == type)
                    .ToListAsync();

                if (oldNotifications.Any())
                {
                    _context.Notifications.RemoveRange(oldNotifications);
                    await _context.SaveChangesAsync();
                }
            }

            if (targetUserId.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.Id == targetUserId.Value);
                if (!userExists)
                {
                    targetUserId = null;
                }
            }

            var notification = new Notification
            {
                Message = message,
                NotificationType = type,
                TargetActionId = targetActionId,
                TargetUserId = targetUserId,
                TargetDepartmentId = targetDepartmentId,
                IsRead = false,
                CreatedDate = DateTime.Now
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}