using CityBreaks.Data;
using CityBreaks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityBreaks.Pages
{
    public class CreatePropertyModel : PageModel
    {
        private readonly CityBreaksContext _context;

        public CreatePropertyModel(CityBreaksContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Property Property { get; set; }

        public SelectList CitiesSelectList { get; set; }

        public async Task OnGetAsync()
        {
            var cities = await _context.Cities.AsNoTracking().ToListAsync();
            CitiesSelectList = new SelectList(cities, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var cities = await _context.Cities.AsNoTracking().ToListAsync();
                CitiesSelectList = new SelectList(cities, "Id", "Name");
                return Page();
            }

            await _context.Properties.AddAsync(Property);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
