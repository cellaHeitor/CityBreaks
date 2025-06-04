using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityBreaks.Services
{
    public class CityService : ICityService
    {
        private readonly CityBreaksContext _context;

        public CityService(CityBreaksContext context)
        {
            _context = context;
        }

        public async Task<List<City>> GetAllAsync()
        {
            return await _context.Cities
                .Include(c => c.Country)
                // Filtrar propriedades não deletadas
                .Include(c => c.Properties.Where(p => p.DeletedAt == null))
                .ToListAsync();
        }

        public async Task<City> GetByNameAsync(string name)
        {
            return await _context.Cities
                .Include(c => c.Country)
                // Mesma filtragem aqui
                .Include(c => c.Properties.Where(p => p.DeletedAt == null))
                .FirstOrDefaultAsync(c =>
                    EF.Functions.Collate(c.Name, "NOCASE") == name);
        }

        public async Task DeletePropertyAsync(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property != null && property.DeletedAt == null)
            {
                property.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
