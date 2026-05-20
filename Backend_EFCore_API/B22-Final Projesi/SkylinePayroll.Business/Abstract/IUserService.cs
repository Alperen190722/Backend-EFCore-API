using SkylinePayroll.Core.Entities;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Core.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Business.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
        Task AddAsync(User user);
        Task<User> GetByMailAsync(string email);
        Task<User> GetByIdAsync(int userId);
        Task<IUserDetailed> GetUserDetailedAsync(int userId);
        Task<bool> UpdateUserProfileAsync(int userId, UserDetailDto userUpdateDto);
        Task UpdateAsync(User user);
    }
}
