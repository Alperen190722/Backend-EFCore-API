using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities;
using SkylinePayroll.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Data.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaims(User user);
        Task<IUserDetailed> GetUserDetails(int userId);
    }
}
