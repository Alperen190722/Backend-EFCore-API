using Microsoft.EntityFrameworkCore;
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Core.Utilities.Security.Hashing;
using SkylinePayroll.Core.Utilities.Security.JWT;
using SkylinePayroll.Data.Abstract;
using SkylinePayroll.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkylinePayroll.Core.Enums.EmployeeEnums;

namespace SkylinePayroll.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IEmployeeService _employeeService;
        private readonly SkylineContext _context;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IEmployeeService employeeService, SkylineContext context)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _employeeService = employeeService;
            _context = context;
        }
        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var userDetailed = await _userService.GetUserDetailedAsync(user.Id);
            var claims = await _userService.GetClaimsAsync(user);
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == user.Id);
            var accessToken = _tokenHelper.CreateToken(userDetailed, claims);

            return accessToken;
        }

        public async Task<string> CreatePasswordResetToken(string email)
        {
            var userToCheck = await _userService.GetByMailAsync(email);
            if (userToCheck == null) return null;
            var token = Guid.NewGuid().ToString();
            userToCheck.ResetToken = token;
            userToCheck.ResetTokenExpires = DateTime.Now.AddHours(1);

            await _userService.UpdateAsync(userToCheck);

            return token;
        }
        public async Task<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (userToCheck == null) return null;

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return null;
            }

            var employee = await _context.Employees
                    .AsNoTracking() 
                    .FirstOrDefaultAsync(e => e.UserId == userToCheck.Id);

            if (employee != null && employee.Status == EmployeeStatus.Passive)
            {
                throw new Exception("Erişim yetkiniz yoktur.");
            }

            return userToCheck;
        }
        public async Task LogOut(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user != null)
            {
                await _userService.UpdateAsync(user);
            }
        }

        public async Task<bool> IsUserActive(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return false;

            if (user.Status == false) return false;

            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId);

            if (employee != null && employee.Status == EmployeeStatus.Passive)
            {
                return false;
            }

            return true;
        }

        public async Task<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                    var user = new User
                    {
                        Email = userForRegisterDto.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        RoleId = userForRegisterDto.RoleId,
                        Status = true
                    };
                    await _userService.AddAsync(user);

                    var employee = new Employee
                    {
                        UserId = user.Id,
                        FirstName = userForRegisterDto.FirstName,
                        LastName = userForRegisterDto.LastName,
                        Salary = userForRegisterDto.Salary,
                        DepartmentId = userForRegisterDto.DepartmentId,
                        RoleId = userForRegisterDto.RoleId, 
                        IdentityNumber = userForRegisterDto.IdentityNumber, 
                        PhoneNumber = userForRegisterDto.PhoneNumber, 
                        Gender = userForRegisterDto.Gender, 
                        Iban = userForRegisterDto.Iban, 
                        CreatedDate = DateTime.Now,
                        Status = EmployeeStatus.Active 
                    };

                    await _employeeService.AddAsync(employee);

                    await transaction.CommitAsync();
                    return user;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Veri bütünlüğü bozuldu, tüm işlemler geri alındı! Detay: " + ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordDto dto)
        {
            var userToUpdate = await _userService.GetByMailAsync(dto.Email);
            if (userToUpdate == null ||
                userToUpdate.ResetToken != dto.Token ||
                userToUpdate.ResetTokenExpires < DateTime.Now)
            {
                return false;
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(dto.NewPassword, out passwordHash, out passwordSalt);

            userToUpdate.PasswordHash = passwordHash;
            userToUpdate.PasswordSalt = passwordSalt;
            userToUpdate.ResetToken = null;
            userToUpdate.ResetTokenExpires = null;

            await _userService.UpdateAsync(userToUpdate);

            return true;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _userService.GetByMailAsync(email) != null)
            {
                return true;
            }
            return false;
        }
    }
}
