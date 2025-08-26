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
