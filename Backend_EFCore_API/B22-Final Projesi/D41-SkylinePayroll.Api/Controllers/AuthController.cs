using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Data.Concrete.EntityFramework;

namespace D41_SkylinePayroll.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private readonly SkylineContext _context;
        public AuthController(IAuthService authService, SkylineContext context)
        {
            _authService = authService;
            _context = context;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (userToLogin == null)
            {
                return BadRequest("User not found or Password is incorrect");
            }

            var result = await _authService.CreateAccessToken(userToLogin);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExists(userForRegisterDto.Email);
            if (userExists)
            {
                return BadRequest("User already exists");
            }

            var registerResult = await _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = await _authService.CreateAccessToken(registerResult);

            return Ok(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var token = await _authService.CreatePasswordResetToken(dto.Email);

            if (token == null)
            {
                return BadRequest("User not found with this email address!");
            }
            return Ok(new { Token = token, Message = "Şifreniz başarıyla değiştirildi. Lütfen mail adresinizi kontrol ediniz." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await _authService.ResetPassword(resetPasswordDto);

            if (result)
            {
                return Ok("Şifreniz başarıyla güncellendi");
            }

            return BadRequest("Something went wrong. Please check your information and try again.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut(int userId)
        {
            await _authService.LogOut(userId);
            return Ok(new { Message = "Oturum başarıyla sonlandırıldı." });
        }

        [HttpGet("check-status/{userId}")]
        public async Task<IActionResult> CheckStatus(int userId)
        {
            var userStatus = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .Select(u => u.Status)
                .FirstOrDefaultAsync();
            if (userStatus == false)
            {
                return Ok(new { Status = 3, Message = "Hesabınız pasife alınmıştır." });
            }

            return Ok(new { Status = 1, Message = "Aktif" });
        }
    }
}
