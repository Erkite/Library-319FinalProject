using System;
using System.Collections.Generic;

namespace _318Project.Models;

public partial class Fine
{
    public int FineId { get; set; }

    public int LoanId { get; set; }

    public decimal Amount { get; set; }

    public bool Paid { get; set; }

    public virtual Loan Loan { get; set; } = null!;
}
