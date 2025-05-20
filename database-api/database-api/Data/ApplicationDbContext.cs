using database_api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace database_api.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // public DbSet<Product> Products { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<GamingGear> GamingGears { get; set; }
        public DbSet<Entities.Monitor> Monitors { get; set; }
        
        public DbSet<WashingMachine> WashingMachines { get; set; }
        public DbSet<Television> Televisions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
