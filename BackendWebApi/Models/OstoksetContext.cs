using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendWebApi.Models;

public partial class OstoksetContext : DbContext
{
    public OstoksetContext()
    {
    }

    public OstoksetContext(DbContextOptions<OstoksetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Osto> Ostos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Kanettava1; Database=Ostokset;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Osto>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Hinta).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Kauppa).HasMaxLength(50);
            entity.Property(e => e.Osoite).HasMaxLength(500);
            entity.Property(e => e.Tuote).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
