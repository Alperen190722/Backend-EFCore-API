using D38_WebApiDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D38_WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        [HttpGet("")]
        [Authorize(Roles = "Admin")]
        public List<ContactModel> Get() 
        {
            return new List<ContactModel> 
            { 
                new ContactModel{Id = 1, FirstName = "Alperen", LastName = "Pişkin" }
            };
        }
    }
}
