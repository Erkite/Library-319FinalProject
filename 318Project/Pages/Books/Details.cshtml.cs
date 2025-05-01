using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly _318Project.Models.LibraryContext _context;

        public DetailsModel(_318Project.Models.LibraryContext context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);

            if (book is not null)
            {
                Book = book;

                return Page();
            }

            return NotFound();
        }
    }
}
