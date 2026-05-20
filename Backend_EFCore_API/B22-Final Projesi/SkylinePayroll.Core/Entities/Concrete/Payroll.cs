using SkylinePayroll.Core.Abstract;
using SkylinePayroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Concrete
{
    public class Payroll : BaseEntity, IEntity
    {
        public int EmployeeId { get; set; }
        public DateTime Period { get; set; }
        public decimal GrossSalary { get; set; } 
        public decimal Bonus { get; set; }      
        public decimal TaxAmount { get; set; }   
        public decimal NetSalary { get; set; }
        public Employee Employee { get; set; }
    }
}
