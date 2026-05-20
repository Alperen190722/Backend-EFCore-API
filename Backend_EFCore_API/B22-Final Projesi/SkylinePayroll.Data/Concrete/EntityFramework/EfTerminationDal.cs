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
    public class EfTerminationDal : EfEntityRepositoryBase<Termination, SkylineContext>, ITerminationDal
    {
        public EfTerminationDal(SkylineContext context) : base(context)
        {
            
        }
    }
}
