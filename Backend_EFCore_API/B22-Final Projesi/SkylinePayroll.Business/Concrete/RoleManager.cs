using SkylinePayroll.Business.Abstract;
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
    public class RoleManager : IRoleService
    {
        private readonly IRoleDal _roleDal;
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public async Task AddAsync(Role role) 
        {
            if (role.MinSalary <= 0 || role.MaxSalary <= 0)
            {
                throw new Exception("Error: Min and Max salary must be greater than zero.");
            }
            if (role.MinSalary >= role.MaxSalary)
            {
                throw new Exception("Error: Minimum salary cannot be equal to or greater than maximum salary.");
            }

            await _roleDal.AddAsync(role);
        }

        public async Task DeleteAsync(Role role)
        {
            await _roleDal.DeleteAsync(role);
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _roleDal.GetAsync(r => r.Id == id);
        }

        public async Task<List<RoleListDto>> GetRoleDetails()
        {
            return await _roleDal.GetRoleDetails();
        }

        public async Task UpdateAsync(Role role)
        {
            if (role.MinSalary >= role.MaxSalary)
            {
                throw new Exception("Error: Minimum salary cannot be greater than or equal to maximum salary.");
            }
            var roleToUpdate = await _roleDal.GetAsync(r => r.Id == role.Id);

            if (roleToUpdate == null)
            {
                throw new Exception("Error: Role not found!");
            }
            roleToUpdate.Name = role.Name;
            roleToUpdate.MinSalary = role.MinSalary;
            roleToUpdate.MaxSalary = role.MaxSalary;
            roleToUpdate.DepartmentId = role.DepartmentId;
            roleToUpdate.IsActive = role.IsActive;
            await _roleDal.UpdateAsync(roleToUpdate);
        }
    }
}
