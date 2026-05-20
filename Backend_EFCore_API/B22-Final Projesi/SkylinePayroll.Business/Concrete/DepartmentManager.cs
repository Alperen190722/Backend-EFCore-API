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
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;
        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }
        public async Task AddAsync(Department department) 
        {
            var existingDept = await _departmentDal.GetAsync(d => d.Name.ToLower() == department.Name.ToLower());
            if (existingDept != null)
            {
                throw new Exception($"Error: A department named '{department.Name}' already exists.");
            }

            await _departmentDal.AddAsync(department);
        }

        public async Task DeleteAsync(Department department)
        {
            await _departmentDal.DeleteAsync(department);
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _departmentDal.GetAsync(d => d.Id == id);
        }

        public async Task<List<DepartmentListDto>> GetDepartmentDetails()
        {
            return await _departmentDal.GetDepartmentDetails();
        }

        public async Task UpdateAsync(Department department)
        {
            var existing = await _departmentDal.GetAsync(d => d.Name.ToLower() == department.Name.ToLower() && d.Id != department.Id);
            if (existing != null)
            {
                throw new Exception($"Error: The department name '{department.Name}' is already in use.");
            }

            await _departmentDal.UpdateAsync(department);
        }
    }
}
