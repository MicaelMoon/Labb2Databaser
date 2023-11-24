using System;
using System.Collections.Generic;
using Labb2Databaser.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb2Databaser.Data;

public partial class LibraryDBContext : DbContext
{
    public LibraryDBContext()
    {
    }

    public LibraryDBContext(DbContextOptions<LibraryDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Butiker> Butikers { get; set; }

    public virtual DbSet<Böcker> Böckers { get; set; }

    public virtual DbSet<Författare> Författares { get; set; }

    public virtual DbSet<Förlag> Förlags { get; set; }

    public virtual DbSet<KundRecensioner> KundRecensioners { get; set; }

    public virtual DbSet<Kunder> Kunders { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldos { get; set; }

    public virtual DbSet<Ordrar> Ordrars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Labb 1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Butiker>(entity =>
        {
            entity.HasKey(e => e.ButikId).HasName("PK__Butiker__B5D66BFADC45D08C");

            entity.ToTable("Butiker");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.ButikAdress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ButikNamn)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Böcker>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__Böcker__447D36EB139ADF2E");

            entity.ToTable("Böcker");

            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.FörfattarId).HasColumnName("FörfattarID");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titel)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Utgivningsdatum)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Författar).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.FörfattarId)
                .HasConstraintName("FK__Böcker__Författa__267ABA7A");
        });

        modelBuilder.Entity<Författare>(entity =>
        {
            entity.HasKey(e => e.FörfattarId).HasName("PK__Författa__C726F5D33F629EEE");

            entity.ToTable("Författare");

            entity.Property(e => e.FörfattarId).HasColumnName("FörfattarID");
            entity.Property(e => e.EfterNamn)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FödelseDatum)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FörNamn)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Förlag>(entity =>
        {
            entity.HasKey(e => e.FörLagId).HasName("PK__Förlag__83C9228912809CD5");

            entity.ToTable("Förlag");

            entity.Property(e => e.FörLagId).HasColumnName("FörLagID");
            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FörLagNamn)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<KundRecensioner>(entity =>
        {
            entity.HasKey(e => e.RecensionId).HasName("PK__KundRece__B8ADC8EB644A32DD");

            entity.ToTable("KundRecensioner");

            entity.Property(e => e.RecensionId).HasColumnName("RecensionID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.RecentionDatum).HasColumnType("datetime");
            entity.Property(e => e.RecesionText).HasColumnType("text");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.KundRecensioners)
                .HasForeignKey(d => d.Isbn)
                .HasConstraintName("FK__KundRecens__ISBN__3E52440B");

            entity.HasOne(d => d.Kund).WithMany(p => p.KundRecensioners)
                .HasForeignKey(d => d.KundId)
                .HasConstraintName("FK__KundRecen__KundI__3D5E1FD2");
        });

        modelBuilder.Entity<Kunder>(entity =>
        {
            entity.HasKey(e => e.KundId).HasName("PK__Kunder__F2B5DEAC95545224");

            entity.ToTable("Kunder");

            entity.Property(e => e.KundId).HasColumnName("KundID");
            entity.Property(e => e.EfterNamn)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FörNamn)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LagerSaldo>(entity =>
        {
            entity.HasKey(e => new { e.ButikId, e.Isbn }).HasName("PK__LagerSal__8FA134D686B239F7");

            entity.ToTable("LagerSaldo");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN");

            entity.HasOne(d => d.Butik).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.ButikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSald__Butik__30F848ED");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSaldo__ISBN__31EC6D26");
        });

        modelBuilder.Entity<Ordrar>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Ordrar__C3905BAF0F1AA596");

            entity.ToTable("Ordrar");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Datum).HasColumnType("datetime");
            entity.Property(e => e.Isbn)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.KundId).HasColumnName("KundID");

            entity.HasOne(d => d.Butik).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.ButikId)
                .HasConstraintName("FK__Ordrar__ButikID__38996AB5");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.Isbn)
                .HasConstraintName("FK__Ordrar__ISBN__36B12243");

            entity.HasOne(d => d.Kund).WithMany(p => p.Ordrars)
                .HasForeignKey(d => d.KundId)
                .HasConstraintName("FK__Ordrar__KundID__37A5467C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
