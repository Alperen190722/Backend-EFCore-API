using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace D40_Abc.Northwind.MvcWebUI.Entities
{
    public class CustomIdentityDbContext:IdentityDbContext<CustomIdentityUser,CustomIdentityRole,string>
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options):base(options)
        {
            
        }
    }
}
