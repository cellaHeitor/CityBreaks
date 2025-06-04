using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(c => c.CountryName)
                   .HasMaxLength(100)
                   .HasColumnName("Country_Name");

            builder.Property(c => c.CountryCode)
                   .HasColumnName("Country_Code");

            // Dados iniciais
            builder.HasData(
                new Country { Id = 1, CountryCode = "US", CountryName = "United States" },
                new Country { Id = 2, CountryCode = "FR", CountryName = "France" },
                new Country { Id = 3, CountryCode = "JP", CountryName = "Japan" }
            );
        }
    }
}
