﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sistema_de_Gestion_de_asistencias.Persistence;

#nullable disable

namespace Sistema_de_Gestion_de_asistencias.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250426012943_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Asistencia", b =>
                {
                    b.Property<int>("IdAsistencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAsistencia"));

                    b.Property<char>("Estado")
                        .HasColumnType("char(1)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdClase")
                        .HasColumnType("int");

                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdPersona")
                        .HasColumnType("int");

                    b.HasKey("IdAsistencia");

                    b.HasIndex("IdClase");

                    b.HasIndex("IdCurso");

                    b.HasIndex("IdPersona");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Clase", b =>
                {
                    b.Property<int>("IdClase")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClase"));

                    b.Property<int>("CursoIdCurso")
                        .HasColumnType("int");

                    b.Property<bool?>("EsTitular")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Horario")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdMateria")
                        .HasColumnType("int");

                    b.Property<int>("MaestroIdPersona")
                        .HasColumnType("int");

                    b.Property<int>("MateriaIdMateria")
                        .HasColumnType("int");

                    b.HasKey("IdClase");

                    b.HasIndex("CursoIdCurso");

                    b.HasIndex("MaestroIdPersona");

                    b.HasIndex("MateriaIdMateria");

                    b.ToTable("Clases", (string)null);
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Curso", b =>
                {
                    b.Property<int>("IdCurso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCurso"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCurso");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Materia", b =>
                {
                    b.Property<int>("IdMateria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMateria"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdMateria");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersona"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IdPersona");

                    b.ToTable("Personas", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Alumno", b =>
                {
                    b.HasBaseType("Sistema_de_Gestion_de_asistencias.Domain.Persona");

                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("Matricula")
                        .HasColumnType("int");

                    b.HasIndex("IdCurso");

                    b.HasIndex("Matricula")
                        .IsUnique()
                        .HasFilter("[Matricula] IS NOT NULL");

                    b.ToTable("Alumnos", (string)null);
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Maestro", b =>
                {
                    b.HasBaseType("Sistema_de_Gestion_de_asistencias.Domain.Persona");

                    b.Property<int>("IdMateria")
                        .HasColumnType("int");

                    b.Property<int>("MateriaIdMateria")
                        .HasColumnType("int");

                    b.HasIndex("MateriaIdMateria");

                    b.ToTable("Maestros", (string)null);
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Asistencia", b =>
                {
                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Clase", "Clase")
                        .WithMany()
                        .HasForeignKey("IdClase")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Curso", "Curso")
                        .WithMany("Asistencias")
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Alumno", "Alumno")
                        .WithMany("Asistencias")
                        .HasForeignKey("IdPersona")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Alumno");

                    b.Navigation("Clase");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Clase", b =>
                {
                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Curso", "Curso")
                        .WithMany("Clases")
                        .HasForeignKey("CursoIdCurso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Maestro", "Maestro")
                        .WithMany("Clases")
                        .HasForeignKey("MaestroIdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Materia", "Materia")
                        .WithMany("Clases")
                        .HasForeignKey("MateriaIdMateria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Maestro");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Alumno", b =>
                {
                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Curso", "Curso")
                        .WithMany("Alumnos")
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Persona", null)
                        .WithOne("Alumno")
                        .HasForeignKey("Sistema_de_Gestion_de_asistencias.Domain.Alumno", "IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Maestro", b =>
                {
                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Persona", null)
                        .WithOne("Maestro")
                        .HasForeignKey("Sistema_de_Gestion_de_asistencias.Domain.Maestro", "IdPersona")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sistema_de_Gestion_de_asistencias.Domain.Materia", "Materia")
                        .WithMany("Maestros")
                        .HasForeignKey("MateriaIdMateria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Curso", b =>
                {
                    b.Navigation("Alumnos");

                    b.Navigation("Asistencias");

                    b.Navigation("Clases");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Materia", b =>
                {
                    b.Navigation("Clases");

                    b.Navigation("Maestros");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Persona", b =>
                {
                    b.Navigation("Alumno");

                    b.Navigation("Maestro");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Alumno", b =>
                {
                    b.Navigation("Asistencias");
                });

            modelBuilder.Entity("Sistema_de_Gestion_de_asistencias.Domain.Maestro", b =>
                {
                    b.Navigation("Clases");
                });
#pragma warning restore 612, 618
        }
    }
}
