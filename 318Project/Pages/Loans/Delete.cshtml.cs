using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Loans
{
    public class DeleteModel : PageModel
    {
        private readonly _318Project.Models.LibraryContext _context;

        public DeleteModel(_318Project.Models.LibraryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Loan Loan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FirstOrDefaultAsync(m => m.LoanId == id);

            if (loan is not null)
            {
                Loan = loan;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                Loan = loan;
                _context.Loans.Remove(Loan);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
