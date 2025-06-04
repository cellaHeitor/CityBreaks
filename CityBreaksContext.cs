using Microsoft.EntityFrameworkCore;
using CityBreaks.Models;
using CityBreaks.Data.Configurations;

namespace CityBreaks.Data
{
    public class CityBreaksContext : DbContext
    {
        public CityBreaksContext(DbContextOptions<CityBreaksContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        }
    }
}
