using Microsoft.AspNetCore.Mvc;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Dtos;
using System.Security.Claims;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpDelete("clear-notification/{id}")]
    public async Task<IActionResult> ClearNotification(int id)
    {
        var result = await _notificationService.ClearNotificationAsync(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("mark-as-read/{id}")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var result = await _notificationService.MarkNotificationAsReadAsync(id);
        if (result.Success) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("GetMyNotifications")]
    public async Task<IActionResult> GetMyNotifications()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
                          ?? User.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

        if (!int.TryParse(userIdClaim, out int currentUserId))
            return BadRequest(new { message = "Kimlik geçerli değil!" });

        var deptClaim = User.Claims.FirstOrDefault(c => c.Type == "Department")?.Value
                        ?? User.Claims.FirstOrDefault(c => c.Type == "department")?.Value;

        int currentUserDeptId = 0;
        
        if (!int.TryParse(deptClaim, out currentUserDeptId))
        {
            currentUserDeptId = deptClaim switch
            {
                "Human Resources" or "HumanResources" or "HR" => 4,
                "Accounting" or "Muhasebe" => 5,
                "Management" or "Yönetim" => 6,
                _ => 0
            };
        }

        var result = await _notificationService.GetMyNotificationsAsync(currentUserId, currentUserDeptId);
        return Ok(result);
    }

    [HttpPost("send-notification")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationViewDto dto)
    {
        
        await _notificationService.SendNotificationAsync(
            dto.Message,
            dto.NotificationType,
            dto.TargetActionId,
            dto.TargetUserId,
            dto.TargetDepartmentId
        );
        return Ok(new { success = true, message = "Bildirim hedefe ulaştı." });
    }
}