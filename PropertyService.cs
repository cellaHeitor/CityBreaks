using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityBreaks.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly CityBreaksContext _context;

        public PropertyService(CityBreaksContext context)
        {
            _context = context;
        }

        public async Task<List<Property>> GetFilteredAsync(
            decimal? minPrice, decimal? maxPrice, string cityName, string propertyName)
        {
            IQueryable<Property> query = _context.Properties
                .Include(p => p.City)
                .ThenInclude(c => c.Country)
                .Where(p => p.DeletedAt == null);  // Ignorar propriedades deletadas

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.PricePerNight >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.PricePerNight <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(cityName))
            {
                query = query.Where(p => EF.Functions.Collate(p.City.Name, "NOCASE").Contains(cityName));
            }

            if (!string.IsNullOrEmpty(propertyName))
            {
                query = query.Where(p => EF.Functions.Collate(p.Name, "NOCASE").Contains(propertyName));
            }

            return await query.ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null && property.DeletedAt == null)
            {
                property.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
