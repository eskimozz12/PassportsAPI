using Microsoft.EntityFrameworkCore;

namespace PassportsAPI.EfCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
       
        public DbSet<InactivePassports> IanctivePassports { get; set; }
        public DbSet<PassportsHistory> PassportsHistory { get; set; }
    }
}
