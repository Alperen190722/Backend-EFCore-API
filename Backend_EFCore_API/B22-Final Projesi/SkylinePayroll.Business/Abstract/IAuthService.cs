using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Abstract
{
    public interface IAuthService
    {
        Task<User> Register(UserForRegisterDto userForRegisterDto, string password);
        Task<User> Login(UserForLoginDto userForLoginDto);
        Task<bool> UserExists(string email);
        Task<AccessToken> CreateAccessToken(User user);
        Task<string> CreatePasswordResetToken(string email);
        Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task LogOut(int userId);
        Task<bool> IsUserActive(int userId);
    }
}
