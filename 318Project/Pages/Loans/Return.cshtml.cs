using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _318Project.Models;
using _318Project.Services;

namespace _318Project.Pages.Loans
{
    public class ReturnModel : PageModel
    {
        private readonly LoanService _loanService;
        public ReturnModel(LoanService loanService) => _loanService = loanService;

        [BindProperty]
        public int SelectedLoanId { get; set; }

        public SelectList ActiveLoansList { get; set; } = default!;

        public async Task OnGetAsync()
        {

            var active = await _loanService.GetActiveLoansAsync();


            var options = active
              .Select(l => new
              {
                  LoanId = l.LoanId,
                  Display = $"{l.LoanId} – {l.Book.Title} ({l.Member.FullName})"
              })
              .ToList();


            ActiveLoansList = new SelectList(options, "LoanId", "Display");
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedLoanId == 0)
            {
                ModelState.AddModelError("", "Please select a loan to return.");
                await OnGetAsync();
                return Page();
            }

            await _loanService.ReturnAsync(SelectedLoanId);
            return RedirectToPage("./Index");
        }
    }
}
