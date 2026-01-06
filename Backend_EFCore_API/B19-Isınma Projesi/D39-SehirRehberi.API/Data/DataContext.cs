using D39_SehirRehberi.API.Models;
using Microsoft.EntityFrameworkCore;

namespace D39_SehirRehberi.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
    }
}
