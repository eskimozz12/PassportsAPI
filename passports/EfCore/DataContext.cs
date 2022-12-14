using Microsoft.EntityFrameworkCore;

namespace PassportsAPI.EfCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
       
        public DbSet<InactivePassports> InanctivePassports { get; set; }
        public DbSet<PassportsHistory> PassportsHistory { get; set; }
        public DbSet<TempTable> TempTable { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InactivePassports>()
                .HasIndex(p => new { p.Series, p.Number })
                .IsUnique(true);
            modelBuilder.Entity<TempTable>()
                .HasIndex(p => new { p.Series, p.Number })
                .IsUnique(true);
        }

    }
}
