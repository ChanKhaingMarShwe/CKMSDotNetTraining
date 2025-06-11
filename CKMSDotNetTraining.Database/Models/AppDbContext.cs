using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CKMSDotNetTraining.Database.Models;

public partial class AppDbContext : DbContext
{

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        String connectionString = "Data Source=localhost;Initial Catalog=CKMSDotNetTraining;User ID=sa;Password=YourPassword123!;TrustServerCertificate=true;";
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}
   


    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
