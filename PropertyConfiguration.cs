using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityBreaks.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.Name)
                   .HasMaxLength(150)
                   .HasColumnName("Property_Name");

            builder.Property(p => p.PricePerNight)
                   .HasColumnName("Price_Per_Night");

            builder.Property(p => p.CityId)
                   .HasColumnName("City_Id");

            // Dados iniciais
            builder.HasData(
                new Property { Id = 1, Name = "Central Park Apartment", PricePerNight = 150.00m, CityId = 1 },
                new Property { Id = 2, Name = "Eiffel Tower View Flat", PricePerNight = 200.00m, CityId = 2 },
                new Property { Id = 3, Name = "Shinjuku Modern Condo", PricePerNight = 180.00m, CityId = 3 }
            );
        }
    }
}
