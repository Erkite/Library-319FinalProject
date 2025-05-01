using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _318Project.Models;

public partial class Member
{
    public int MemberId { get; set; }

    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly JoinDate { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
