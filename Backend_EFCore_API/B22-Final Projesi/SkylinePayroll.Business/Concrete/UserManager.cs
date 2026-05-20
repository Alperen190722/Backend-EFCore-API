
using SkylinePayroll.Business.Abstract;
using SkylinePayroll.Core.Entities;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using SkylinePayroll.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        private readonly IEmployeeDal _employeeDal;
        public UserManager(IUserDal userDal, IEmployeeDal employeeDal)
        {
            _userDal = userDal;
            _employeeDal = employeeDal;
        }
        public async Task AddAsync(User user)
        {
            await _userDal.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _userDal.GetAsync(u => u.Id == userId);
        }

        public async Task<User> GetByMailAsync(string email)
        {
           return await _userDal.GetAsync(u => u.Email == email);
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            return await _userDal.GetClaims(user);
        }

        public async Task<IUserDetailed> GetUserDetailedAsync(int userId)
        {
            return await _userDal.GetUserDetails(userId);
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UserDetailDto userUpdateDto)
        {
            var user = await _userDal.GetAsync(u => u.Id == userId);
            if (user == null) return false;
            user.Email = userUpdateDto.Email;
            var employee = await _employeeDal.GetAsync(e => e.UserId == user.Id);
            if (employee != null)
            {
                employee.FirstName = userUpdateDto.FirstName;
                employee.LastName = userUpdateDto.LastName;
                employee.DepartmentId = userUpdateDto.DepartmentId;
            }
            await _userDal.UpdateAsync(user);
            await _employeeDal.UpdateAsync(employee);
            return true;
        }

        public async Task UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
        }
    }
}
