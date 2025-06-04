using CityBreaks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPropertyService
{
    Task<List<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, string cityName, string propertyName);
    Task DeleteAsync(int id);
}
