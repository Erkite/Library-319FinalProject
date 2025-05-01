using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _db;
        public IndexModel(LibraryContext db) => _db = db;

        // rename to plural for clarity
        public IList<Loan> Loans { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Loans = await _db.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                // if you only want active (not returned) loans:
                .Where(l => l.ReturnDate == null)
                .ToListAsync();
        }
    }
}
