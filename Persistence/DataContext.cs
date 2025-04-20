using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sistema_de_Gestion_de_asistencias.Domain;

namespace Sistema_de_Gestion_de_asistencias.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Maestro> Maestros { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<AsignacionDocente> AsignacionesDocente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .ToTable("Personas");

            modelBuilder.Entity<Alumno>()
                .ToTable("ALumnos");

            modelBuilder.Entity<Maestro>()
                .ToTable("Maestros");

            modelBuilder.Entity<AsignacionDocente>()
                .ToTable("AsignacionesDocente");

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Alumno)
                .WithMany(al => al.Asistencias)
                .HasForeignKey(a => a.IdPersona)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Asistencias)
                .HasForeignKey(a => a.IdCurso)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Alumno)
                .WithOne()
                .HasForeignKey<Alumno>(a => a.IdPersona);

            modelBuilder.Entity<Persona>()
                .HasOne(p => p.Maestro)
                .WithOne()
                .HasForeignKey<Maestro>(m => m.IdPersona);

            modelBuilder.Entity<Curso>();

            modelBuilder.Entity<Materia>();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SistemaAsistencia;User Id=sa;Password=Jw#ilovepizza9;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
