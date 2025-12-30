using Microsoft.AspNetCore.Identity;

namespace D36_AspNetCoreMvc2Introduction.Identity
{
    public class AppIdentityUser : IdentityUser
    {
        public int Age { get; set; }
    }
}
