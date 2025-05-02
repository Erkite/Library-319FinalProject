using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Members
{
    public class DetailsModel : PageModel
    {
        private readonly LibraryContext _context;

        public DetailsModel(LibraryContext context)
        {
            _context = context;
        }


        public Member Member { get; set; } = default!;


        public IList<Loan> LoanHistory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            Member = await _context.Members.FindAsync(id);
            if (Member == null)
            {
                return NotFound();
            }


            LoanHistory = await _context.Loans
                .Include(l => l.Book)
                .Where(l => l.MemberId == id)
                .OrderByDescending(l => l.CheckoutDate)
                .ToListAsync();

            return Page();
        }
    }
}
