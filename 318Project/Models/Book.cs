using System;
using System.Collections.Generic;

namespace _318Project.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public string? Isbn { get; set; }

    public DateOnly? PublishDate { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
