using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CentroCrud.Server.Models;

public partial class CentroFormacionContext : DbContext
{
  public CentroFormacionContext()
  {
  }

  public CentroFormacionContext(DbContextOptions<CentroFormacionContext> options)
      : base(options)
  {
  }

  public virtual DbSet<Alumno> Alumnos { get; set; }

  public virtual DbSet<Curso> Cursos { get; set; }

  public virtual DbSet<Inscripciones> Inscripciones { get; set; }

  public virtual DbSet<RecuperacionPassword> RecuperacionPasswords { get; set; }

  public virtual DbSet<Roles> Roles { get; set; }

  public virtual DbSet<Usuario> Usuarios { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Alumno>(entity =>
    {
      entity.HasKey(e => e.IdAlumno).HasName("PK__Alumnos__3214EC071732BB17");

      entity.Property(e => e.Email)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Nif)
              .HasMaxLength(20)
              .IsUnicode(false)
              .HasColumnName("nif");
      entity.Property(e => e.PrimerApellido)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("primerApellido");
      entity.Property(e => e.PrimerNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("primerNombre");
      entity.Property(e => e.SegundoApellido)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("segundoApellido");
      entity.Property(e => e.SegundoNombre)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("segundoNombre");
    });

    modelBuilder.Entity<Curso>(entity =>
    {
      entity.HasKey(e => e.IdCurso).HasName("PK__Cursos__3214EC07D038CC38");

      entity.Property(e => e.Codigo)
              .HasMaxLength(10)
              .IsUnicode(false);
      entity.Property(e => e.Descripcion)
              .HasMaxLength(200)
              .IsUnicode(false);
      entity.Property(e => e.Nombre)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Temario).IsUnicode(false);
    });

    modelBuilder.Entity<Inscripciones>(entity =>
    {
      entity.HasKey(e => e.IdInscripcion).HasName("PK__Inscripc__3214EC07E24C3B21");

      entity.HasOne(d => d.Alumno).WithMany(p => p.Inscripciones)
              .HasForeignKey(d => d.AlumnoId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Inscripci__Alumn__3B75D760");

      entity.HasOne(d => d.Curso).WithMany(p => p.Inscripciones)
              .HasForeignKey(d => d.CursoId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Inscripci__Curso__3C69FB99");
    });

    modelBuilder.Entity<RecuperacionPassword>(entity =>
    {
      entity.HasKey(e => e.IdRecPass).HasName("PK__Recupera__3214EC0713808DC5");

      entity.ToTable("Recuperacion_Password");

      entity.Property(e => e.Token)
              .HasMaxLength(200)
              .IsUnicode(false);

      entity.HasOne(d => d.Usuario).WithMany(p => p.RecuperacionPasswords)
              .HasForeignKey(d => d.UsuarioId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Recuperac__Usuar__44FF419A");
    });

    modelBuilder.Entity<Roles>(entity =>
    {
      entity.HasKey(e => e.IdRol).HasName("PK__Roles__3214EC07C66F2F4E");

      entity.Property(e => e.Nombre)
              .HasMaxLength(50)
              .IsUnicode(false);
    });

    modelBuilder.Entity<Usuario>(entity =>
    {
      entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__3214EC071BE74E9B");

      entity.Property(e => e.Email)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Password)
              .HasMaxLength(50)
              .IsUnicode(false);
      entity.Property(e => e.Usuario1)
              .HasMaxLength(50)
              .IsUnicode(false)
              .HasColumnName("Usuario");

      entity.HasOne(d => d.Alumno).WithMany(p => p.Usuarios)
              .HasForeignKey(d => d.AlumnoId)
              .HasConstraintName("FK__Usuarios__Alumno__4222D4EF");

      entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
              .HasForeignKey(d => d.RolId)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK__Usuarios__RolId__412EB0B6");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
