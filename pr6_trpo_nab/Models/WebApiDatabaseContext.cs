using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pr6_trpo_nab.Models;

public partial class WebApiDatabaseContext : DbContext
{
    public WebApiDatabaseContext()
    {
    }

    public WebApiDatabaseContext(DbContextOptions<WebApiDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sql.ects;Database=WebApiDatabase;User Id=student_00;Password=student_00;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC072ADF628C");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Teacher).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(180);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
