using Microsoft.AspNetCore.Mvc;

namespace D36_AspNetCoreMvc2Introduction.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("")]
        [Route("save")]
        [Route("~/save")]
        [HttpPost]
        public string Save()
        {
            return "Saved";
        }

        [Route("delete/{id?}")]
        [HttpDelete]
        public string Delete(int id=0)
        {
            return String.Format( "Deleted {0}",id);
        }

        [Route("update")]
        [HttpPut]
        public string Update()
        {
            return "Updated";
        }
    }
}
