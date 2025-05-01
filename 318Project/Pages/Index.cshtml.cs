using _318Project.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _318Project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly LibraryContext _context;

        public List<Member> MemberList { get; set; }

        public IndexModel(ILogger<IndexModel> logger, LibraryContext context)
        {
            _logger = logger;
            _context = context;
            MemberList = default!; 
        }

        public void OnGet()
        {
            MemberList = _context.Members.OrderBy(x => x.FullName).ToList();
        }
    }
}
