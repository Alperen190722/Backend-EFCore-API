using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkylinePayroll.Core.Entities.Dtos
{
    public class MarkReadDto
    {
        public int? ActionId { get; set; }
        public string Type { get; set; }
        public int? UserId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
