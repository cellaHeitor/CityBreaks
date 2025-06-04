using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .HasColumnName("City_Name");

            builder.Property(c => c.CountryId)
                   .HasColumnName("Country_Id");

            // Dados iniciais
            builder.HasData(
                new City { Id = 1, Name = "New York", CountryId = 1 },
                new City { Id = 2, Name = "Paris", CountryId = 2 },
                new City { Id = 3, Name = "Tokyo", CountryId = 3 }
            );
        }
    }
}
