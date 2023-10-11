using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesRazor.Models;

namespace MoviesRazor.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MoviesRazor.Models.MovieContext _context;
        
        IWebHostEnvironment _appEnvironment;
        public CreateModel(MoviesRazor.Models.MovieContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public IFormFile Poster { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Movies == null || Movie == null)
            {
                return Page();
            }

            if (Poster != null)
            {
                // Путь к папке Files
                string path = "/Image/" + Poster.FileName; // имя файла
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Poster.CopyToAsync(fileStream); // копируем файл в поток
                }

                Movie.Poster = "~" + path;
                _context.Movies.Add(Movie);
                await _context.SaveChangesAsync();

               
            }
            return RedirectToPage("./Index");
        }
    }
}
