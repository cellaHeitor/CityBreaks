using CityBreaks.Models;
using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityBreaks.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICityService _cityService;

        public IndexModel(ICityService cityService)
        {
            _cityService = cityService;
        }

        public List<City> Cities { get; set; }

        public async Task OnGetAsync()
        {
            Cities = await _cityService.GetAllAsync();
        }
    }
}
