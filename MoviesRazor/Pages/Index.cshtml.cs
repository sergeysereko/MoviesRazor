using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoviesRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace MoviesRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MovieContext db;

        public IndexModel(MovieContext context)
        {
            db = context;
        }

        public IList<Movie> Movie { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (db.Movies != null)
            {
                Movie = await db.Movies.ToListAsync();

            }
        }
    }
}