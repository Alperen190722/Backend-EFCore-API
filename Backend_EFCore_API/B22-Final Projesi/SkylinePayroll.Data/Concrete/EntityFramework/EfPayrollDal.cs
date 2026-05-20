using SkylinePayroll.Core.Concrete.EntityFramework;
using SkylinePayroll.Core.Entities.Concrete;
using SkylinePayroll.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Data.Concrete.EntityFramework
{
    public class EfPayrollDal : EfEntityRepositoryBase<Payroll, SkylineContext>, IPayrollDal
    {
        public EfPayrollDal(SkylineContext context) :base(context)
        {
            
        }
    }
}
