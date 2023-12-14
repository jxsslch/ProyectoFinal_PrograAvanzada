using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace G2_ProyectoFinal.Models;

public partial class SistemaEmpresaContext : DbContext
{
    public SistemaEmpresaContext()
    {
    }

    public SistemaEmpresaContext(DbContextOptions<SistemaEmpresaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Canton> Cantons { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Lenguaje> Lenguajes { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Monedum> Moneda { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Transaccione> Transacciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Canton>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Canton__3214EC2702C0D1FE");

            entity.ToTable("Canton");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProvinciaId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ProvinciaID");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Cantons)
                .HasForeignKey(d => d.ProvinciaId)
                .HasConstraintName("FK_PROVINCIA");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC2716FB0813");

            entity.ToTable("Cliente");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.CantonId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("CantonID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("EmpresaID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumTelefono).HasColumnName("numTelefono");
            entity.Property(e => e.ProvinciaId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ProvinciaID");

            entity.HasOne(d => d.Canton).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CantonId)
                .HasConstraintName("FK_CANTON");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EmpresaId)
                .HasConstraintName("FK_EMPRESA");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.ProvinciaId)
                .HasConstraintName("FK_PROVINCIA_CLIENTE");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empresa__3214EC27565927F9");

            entity.ToTable("Empresa");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumTelefono).HasColumnName("numTelefono");
        });

        modelBuilder.Entity<Lenguaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lenguaje__3214EC279814DD58");

            entity.ToTable("Lenguaje");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MetodoPa__3214EC27B1118747");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Monedum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Moneda__3214EC27C5CA662D");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provinci__3214EC279AB67F5B");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaccione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transacc__3214EC2719A57EC1");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.ClienteId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("ClienteID");
            entity.Property(e => e.LenguajeId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("LenguajeID");
            entity.Property(e => e.MetodoPagoId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("MetodoPagoID");
            entity.Property(e => e.MonedaId)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("MonedaID");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
           entity.Property(t => t.Fecha)
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_CLIENTE");

            entity.HasOne(d => d.Lenguaje).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.LenguajeId)
                .HasConstraintName("FK_LENGUAJE");

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("FK_METODOPAGO");

            entity.HasOne(d => d.Moneda).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.MonedaId)
                .HasConstraintName("FK_MONEDA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
