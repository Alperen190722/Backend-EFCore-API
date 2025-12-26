using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D37_AspWithAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        public List<Customer> Get()
        {
            return new List<Customer> 
            {
                new Customer { Id = 1, FirstName = "Alperen", LastName = "Pişkin" },
                new Customer { Id = 2, FirstName = "Furkan", LastName = "Sülek" },
                new Customer { Id = 3, FirstName = "Semih", LastName = "Tecer" },
                new Customer {Id = 4, FirstName = "Sina" , LastName = "Sasounpur"}
            };
        }
    }

    public class Customer 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
