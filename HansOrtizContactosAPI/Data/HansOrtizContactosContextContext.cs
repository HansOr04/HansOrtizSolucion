using System;
using System.Collections.Generic;
using HansOrtizContactosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HansOrtizContactosAPI.Data;

public partial class HansOrtizContactosContextContext : DbContext
{
    public HansOrtizContactosContextContext()
    {
    }

    public HansOrtizContactosContextContext(DbContextOptions<HansOrtizContactosContextContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HO_Contacto> HoContactos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HansOrtizContactosContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HO_Contacto>(entity =>
        {
            entity.HasKey(e => e.IdHO_Contactos);

            entity.ToTable("HO_Contactos");

            entity.Property(e => e.IdHO_Contactos).HasColumnName("IdHO_Contactos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
