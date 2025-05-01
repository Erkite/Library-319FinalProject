using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using _318Project.Models;
using _318Project.Services;

namespace _318Project.Pages.Fines
{
    public class IndexModel : PageModel
    {
        private readonly FineService _fineService;
        public IndexModel(FineService fineService) => _fineService = fineService;

        public IList<Fine> UnpaidFines { get; set; } = default!;

        [BindProperty]
        public int SelectedFineId { get; set; }

        public async Task OnGetAsync()
        {
            UnpaidFines = await _fineService.GetUnpaidFinesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedFineId == 0)
            {
                ModelState.AddModelError("", "Please select a fine to mark as paid.");
                await OnGetAsync();
                return Page();
            }

            await _fineService.PayFineAsync(SelectedFineId);
            return RedirectToPage();  // refresh the list
        }
    }
}
