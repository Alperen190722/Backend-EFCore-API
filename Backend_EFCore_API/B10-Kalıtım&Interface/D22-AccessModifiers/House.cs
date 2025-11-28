using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D22_AccessModifiers
{
    public class House
    {

        public House()
        {
            
        }
        public int Id { get; set; }
        public string City { get; set; }

        protected string Suburb { get; set; }
    }

    public class HouseTest : House
    {
        public HouseTest()
        {
            
        }

    }
}
