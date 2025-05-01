using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly LibraryContext _db;
        public CreateModel(LibraryContext db) => _db = db;

        // We bind just the selected IDs
        [BindProperty]
        public int SelectedBookId { get; set; }

        [BindProperty]
        public int SelectedMemberId { get; set; }

        // Read-only dates
        public DateOnly CheckoutDate { get; set; }
        public DateOnly DueDate { get; set; }

        // Dropdown data
        public SelectList BooksList { get; set; } = default!;
        public SelectList MembersList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateListsAsync();

            CheckoutDate = DateOnly.FromDateTime(DateTime.Today);
            DueDate = CheckoutDate.AddDays(7);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await PopulateListsAsync();

            CheckoutDate = DateOnly.FromDateTime(DateTime.Today);
            DueDate = CheckoutDate.AddDays(7);

            // Validate selections
            if (SelectedBookId == 0)
                ModelState.AddModelError(nameof(SelectedBookId), "Please select a book.");
            if (SelectedMemberId == 0)
                ModelState.AddModelError(nameof(SelectedMemberId), "Please select a member.");

            // Prevent double‐checkout
            if (SelectedBookId != 0)
            {
                bool alreadyOut = await _db.Loans
                    .AnyAsync(l => l.BookId == SelectedBookId && l.ReturnDate == null);
                if (alreadyOut)
                    ModelState.AddModelError(nameof(SelectedBookId), "That book is already checked out.");
            }

            if (!ModelState.IsValid)
                return Page();

            // Compose and save the new Loan
            var loan = new Loan
            {
                BookId = SelectedBookId,
                MemberId = SelectedMemberId,
                CheckoutDate = CheckoutDate,
                DueDate = DueDate
            };

            _db.Loans.Add(loan);
            await _db.SaveChangesAsync();
            TempData["SuccessMessage"] = "Checked out successfully!";
            return RedirectToPage("./Index");
        }

        private async Task PopulateListsAsync()
        {
            var available = await _db.Books
                .Where(b => !_db.Loans.Any(l => l.BookId == b.BookId && l.ReturnDate == null))
                .OrderBy(b => b.Title)
                .ToListAsync();

            BooksList = new SelectList(available, nameof(Book.BookId), nameof(Book.Title));
            MembersList = new SelectList(
                await _db.Members.OrderBy(m => m.FullName).ToListAsync(),
                nameof(Member.MemberId),
                nameof(Member.FullName)
            );
        }
    }
}
