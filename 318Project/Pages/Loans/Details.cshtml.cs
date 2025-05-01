using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Loans
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryContext _db;
        public DetailsModel(LibraryContext db) => _db = db;

        // The loan we're displaying
        public Loan Loan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            // Eager-load the related Book and Member
            Loan = await _db.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.LoanId == id);

            if (Loan == null) return NotFound();
            return Page();
        }
    }
}
