using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Services
{
    public class LoanService
    {
        private readonly LibraryContext _db;
        public LoanService(LibraryContext db) => _db = db;

        // Get all loans that haven't been returned yet
        public Task<List<Loan>> GetActiveLoansAsync()
            => _db.Loans
                  .Include(l => l.Book)
                  .Include(l => l.Member)
                  .Where(l => l.ReturnDate == null)
                  .ToListAsync();

        // Mark as returned and create a Fine if overdue
        public async Task ReturnAsync(int loanId)
        {
            var loan = await _db.Loans.FindAsync(loanId)
                      ?? throw new KeyNotFoundException("Loan not found.");

            loan.ReturnDate = DateOnly.FromDateTime(DateTime.Today);

            if (loan.ReturnDate > loan.DueDate)
            {
                var daysOver = (loan.ReturnDate.Value.ToDateTime(TimeOnly.MinValue)
                                - loan.DueDate.ToDateTime(TimeOnly.MinValue)).Days;
                _db.Fines.Add(new Fine
                {
                    LoanId = loanId,
                    Amount = daysOver * 0.50m,
                    Paid = false
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}
