using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class DABDbContext : DbContext
{
    public DABDbContext()
    {
    }

    public DABDbContext(DbContextOptions<DABDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agence> Agences { get; set; }

    public virtual DbSet<Caisse> Caisses { get; set; }

    public virtual DbSet<Composant> Composants { get; set; }

    public virtual DbSet<Dab> Dabs { get; set; }

    public virtual DbSet<Imprimante> Imprimantes { get; set; }

    public virtual DbSet<Lecteurcode> Lecteurcodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=dab;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("agence_pkey");

            entity.ToTable("agence");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adresse)
                .HasMaxLength(200)
                .HasColumnName("adresse");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Caisse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("caisse_pkey");

            entity.ToTable("caisse");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombrebillets).HasColumnName("nombrebillets");
            entity.Property(e => e.Valeurbillet).HasColumnName("valeurbillet");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Caisse)
                .HasForeignKey<Caisse>(d => d.Id)
                .HasConstraintName("caisse_id_fkey");
        });

        modelBuilder.Entity<Composant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("composant_pkey");

            entity.ToTable("composant");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dateinstallation).HasColumnName("dateinstallation");
            entity.Property(e => e.Etat)
                .HasMaxLength(50)
                .HasColumnName("etat");
            entity.Property(e => e.Iddab).HasColumnName("iddab");
            entity.Property(e => e.NomType)
                .HasMaxLength(20)
                .HasColumnName("nom_type");

            entity.HasOne(d => d.IddabNavigation).WithMany(p => p.Composants)
                .HasForeignKey(d => d.Iddab)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("composant_iddab_fkey");
        });

        modelBuilder.Entity<Dab>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dab_pkey");

            entity.ToTable("dab");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idagence).HasColumnName("idagence");
            entity.Property(e => e.Localisation)
                .HasMaxLength(200)
                .HasColumnName("localisation");
            entity.Property(e => e.Montantdisponible).HasColumnName("montantdisponible");
            entity.Property(e => e.Statut)
                .HasMaxLength(50)
                .HasColumnName("statut");

            entity.HasOne(d => d.IdagenceNavigation).WithMany(p => p.Dabs)
                .HasForeignKey(d => d.Idagence)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("dab_idagence_fkey");
        });

        modelBuilder.Entity<Imprimante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("imprimante_pkey");

            entity.ToTable("imprimante");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Niveaupapier).HasColumnName("niveaupapier");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Imprimante)
                .HasForeignKey<Imprimante>(d => d.Id)
                .HasConstraintName("imprimante_id_fkey");
        });

        modelBuilder.Entity<Lecteurcode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lecteurcode_pkey");

            entity.ToTable("lecteurcode");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Typelecteur)
                .HasMaxLength(50)
                .HasColumnName("typelecteur");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Lecteurcode)
                .HasForeignKey<Lecteurcode>(d => d.Id)
                .HasConstraintName("lecteurcode_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
