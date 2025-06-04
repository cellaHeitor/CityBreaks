using CityBreaks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace CityBreaks.Pages
{
    public class DeletePropertyModel : PageModel
    {
        private readonly ICityService _cityService;

        public DeletePropertyModel(ICityService cityService)
        {
            _cityService = cityService;
        }

        [BindProperty]
        public int PropertyId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PropertyId = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _cityService.DeletePropertyAsync(PropertyId);
            return RedirectToPage("/Index");
        }
    }
}
