using D36_AspNetCoreMvc2Introduction.Entities;
using Microsoft.EntityFrameworkCore;
namespace D36_AspNetCoreMvc2Introduction.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
