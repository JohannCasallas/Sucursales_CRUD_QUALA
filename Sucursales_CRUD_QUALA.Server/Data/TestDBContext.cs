using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Sucursales_CRUD_QUALA.Server.Models;

namespace Sucursales_CRUD_QUALA.Server.Data
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MonedaJc> MonedaJcs { get; set; } = null!;
        public virtual DbSet<SucursalJc> SucursalJcs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonedaJc>(entity =>
            {
                entity.HasKey(e => e.MonedaId)
                    .HasName("PK__Moneda_J__CEEBACBE64F744E9");

                entity.ToTable("Moneda_JC");

                entity.Property(e => e.Descripcion).HasMaxLength(50);
            });

            modelBuilder.Entity<SucursalJc>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Sucursal__06370DAD32A704C3");

                entity.ToTable("Sucursal_JC");

                entity.Property(e => e.Descripcion).HasMaxLength(250);

                entity.Property(e => e.Direccion).HasMaxLength(250);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Identificacion).HasMaxLength(50);

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.SucursalJcs)
                    .HasForeignKey(d => d.MonedaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sucursal_JC_Moneda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
