using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Loans
{
    public class EditModel : PageModel
    {
        private readonly _318Project.Models.LibraryContext _context;

        public EditModel(_318Project.Models.LibraryContext context)
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

            var loan =  await _context.Loans.FirstOrDefaultAsync(m => m.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }
            Loan = loan;
           ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BookId");
           ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(Loan.LoanId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }
    }
}
