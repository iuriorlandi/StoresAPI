using Microsoft.EntityFrameworkCore;
using StoresAPI.Domain;

namespace StoresAPI.Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Company> Companies { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(c => c.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
