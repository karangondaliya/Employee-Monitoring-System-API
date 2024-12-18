using Microsoft.EntityFrameworkCore;
using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
