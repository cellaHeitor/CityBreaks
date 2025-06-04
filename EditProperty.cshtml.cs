using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CityBreaks.Pages
{
    public class EditPropertyModel : PageModel
    {
        private readonly CityBreaksContext _context;

        public EditPropertyModel(CityBreaksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Property Property { get; set; }

        public SelectList CitiesSelectList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Property = await _context.Properties.FindAsync(id);

            if (Property == null)
                return NotFound();

            var cities = await _context.Cities.AsNoTracking().ToListAsync();
            CitiesSelectList = new SelectList(cities, "Id", "Name", Property.CityId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var propertyToUpdate = await _context.Properties.FindAsync(id);

            if (propertyToUpdate == null)
                return NotFound();

            // Atualiza somente os campos especificados
            if (await TryUpdateModelAsync<Property>(
                propertyToUpdate,
                "Property",   // prefixo para vinculação do formulário
                p => p.Name,
                p => p.PricePerNight,
                p => p.CityId))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("/Index");
            }

            // Caso falhe, recarregue as cidades para dropdown e retorne a página
            var cities = await _context.Cities.AsNoTracking().ToListAsync();
            CitiesSelectList = new SelectList(cities, "Id", "Name", propertyToUpdate.CityId);

            return Page();
        }
    }
}
