using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Reports
{
    public class OverdueModel : PageModel
    {
        private readonly LibraryContext _db;
        public OverdueModel(LibraryContext db) => _db = db;


        public IList<OverdueLoanViewModel> OverdueLoans { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            OverdueLoans = await _db.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .Where(l => l.ReturnDate == null && l.DueDate < today)
                .Select(l => new OverdueLoanViewModel
                {
                    LoanId = l.LoanId,
                    Title = l.Book.Title,
                    FullName = l.Member.FullName,
                    DaysOverdue = (today.ToDateTime(TimeOnly.MinValue)
                                   - l.DueDate.ToDateTime(TimeOnly.MinValue)).Days
                })
                .ToListAsync();
        }
    }

    public class OverdueLoanViewModel
    {
        public int LoanId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int DaysOverdue { get; set; }
    }
}

