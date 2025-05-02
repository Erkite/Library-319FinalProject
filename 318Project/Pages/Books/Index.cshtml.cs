using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _318Project.Models;

namespace _318Project.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly LibraryContext _context;

        public IndexModel(LibraryContext context)
        {
            _context = context;
            SearchTitle = string.Empty;
            SearchAuthorId = null;
            SearchCategoryId = null;
            AuthorsList = new SelectList(Array.Empty<Author>(), nameof(Author.AuthorId), nameof(Author.Name));
            CategoriesList = new SelectList(Array.Empty<Category>(), nameof(Category.CategoryId), nameof(Category.Name));
            Books = new List<Book>();
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchAuthorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchCategoryId { get; set; }

        public SelectList AuthorsList { get; set; }
        public SelectList CategoriesList { get; set; }

        public IList<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            var authors = await _context.Authors.OrderBy(a => a.Name).ToListAsync();
            var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            AuthorsList = new SelectList(authors, nameof(Author.AuthorId), nameof(Author.Name));
            CategoriesList = new SelectList(categories, nameof(Category.CategoryId), nameof(Category.Name));

            var query = _context.Books
                                .Include(b => b.Author)
                                .Include(b => b.Category)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTitle))
            {
                query = query.Where(b =>
                    EF.Functions.Like(b.Title, $"%{SearchTitle}%"));
            }
            if (SearchAuthorId.HasValue)
            {
                query = query.Where(b => b.AuthorId == SearchAuthorId.Value);
            }
            if (SearchCategoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == SearchCategoryId.Value);
            }

            Books = await query.ToListAsync();
        }
    }
}
