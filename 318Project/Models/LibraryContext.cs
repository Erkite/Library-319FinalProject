using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _318Project.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Fine> Fines { get; set; }

    public virtual DbSet<Loan> Loans { get; set; }

    public virtual DbSet<Member> Members { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC1494821356");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C22711D02BD7");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA27B08B2F").IsUnique();

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__AuthorID__3C69FB99");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Books__CategoryI__3D5E1FD2");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BF960B95E");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Fine>(entity =>
        {
            entity.HasKey(e => e.FineId).HasName("PK__Fines__9D4A9BCCB50A8BB5");

            entity.Property(e => e.FineId).HasColumnName("FineID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LoanId).HasColumnName("LoanID");

            entity.HasOne(d => d.Loan).WithMany(p => p.Fines)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fines__LoanID__48CFD27E");
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loans__4F5AD4379F3C85DD");

            entity.Property(e => e.LoanId).HasColumnName("LoanID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.CheckoutDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Book).WithMany(p => p.Loans)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__BookID__440B1D61");

            entity.HasOne(d => d.Member).WithMany(p => p.Loans)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Loans__MemberID__44FF419A");
            entity.ToTable(tb => tb.HasTrigger("trg_LoanReturn_InsertFine"));
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B38822890BC");

            entity.HasIndex(e => e.Email, "UQ__Members__A9D10534B5DF3BF3").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.JoinDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
