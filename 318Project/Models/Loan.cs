using System;
using System.Collections.Generic;            // ← needed for ICollection<>
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _318Project.Models
{
    public partial class Loan
    {
        [Key]
        public int LoanId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        [Required]
        [Display(Name = "Member")]
        public int MemberId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Checked Out")]
        public DateOnly CheckoutDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Due On")]
        public DateOnly DueDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Returned On")]
        public DateOnly? ReturnDate { get; set; }

        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;

        [InverseProperty("Loan")]
        public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

        [NotMapped]
        public bool IsOverdue =>
            ReturnDate == null &&
            DateOnly.FromDateTime(DateTime.Today) > DueDate;
    }
}
