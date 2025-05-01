using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Services
{
    public class FineService
    {
        private readonly LibraryContext _db;
        public FineService(LibraryContext db) => _db = db;

        /// <summary>
        /// Get all fines that have not yet been paid.
        /// </summary>
        public Task<List<Fine>> GetUnpaidFinesAsync()
            => _db.Fines
                  .Include(f => f.Loan!)
                    .ThenInclude(l => l.Book)
                  .Include(f => f.Loan!)
                    .ThenInclude(l => l.Member)
                  .Where(f => !f.Paid)
                  .ToListAsync();

        /// <summary>
        /// Mark the given fine as paid.
        /// </summary>
        public async Task PayFineAsync(int fineId)
        {
            var fine = await _db.Fines.FindAsync(fineId)
                       ?? throw new KeyNotFoundException("Fine not found.");
            fine.Paid = true;
            await _db.SaveChangesAsync();
        }
    }
}
