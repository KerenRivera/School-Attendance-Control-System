using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_de_Gestion_de_asistencias.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.IdCurso);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.IdMateria);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    Matricula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Alumnos_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alumnos_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maestros",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    MateriaIdMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maestros", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Maestros_Materias_MateriaIdMateria",
                        column: x => x.MateriaIdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maestros_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    IdClase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMateria = table.Column<int>(type: "int", nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    Horario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsTitular = table.Column<bool>(type: "bit", nullable: true),
                    MaestroIdPersona = table.Column<int>(type: "int", nullable: false),
                    MateriaIdMateria = table.Column<int>(type: "int", nullable: false),
                    CursoIdCurso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.IdClase);
                    table.ForeignKey(
                        name: "FK_Clases_Cursos_CursoIdCurso",
                        column: x => x.CursoIdCurso,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clases_Maestros_MaestroIdPersona",
                        column: x => x.MaestroIdPersona,
                        principalTable: "Maestros",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clases_Materias_MateriaIdMateria",
                        column: x => x.MateriaIdMateria,
                        principalTable: "Materias",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    IdAsistencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    IdCurso = table.Column<int>(type: "int", nullable: false),
                    IdClase = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "char(1)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.IdAsistencia);
                    table.ForeignKey(
                        name: "FK_Asistencias_Alumnos_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Alumnos",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistencias_Clases_IdClase",
                        column: x => x.IdClase,
                        principalTable: "Clases",
                        principalColumn: "IdClase",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencias_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_IdCurso",
                table: "Alumnos",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_Matricula",
                table: "Alumnos",
                column: "Matricula",
                unique: true,
                filter: "[Matricula] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdClase",
                table: "Asistencias",
                column: "IdClase");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdCurso",
                table: "Asistencias",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_IdPersona",
                table: "Asistencias",
                column: "IdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Clases_CursoIdCurso",
                table: "Clases",
                column: "CursoIdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_Clases_MaestroIdPersona",
                table: "Clases",
                column: "MaestroIdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Clases_MateriaIdMateria",
                table: "Clases",
                column: "MateriaIdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_Maestros_MateriaIdMateria",
                table: "Maestros",
                column: "MateriaIdMateria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Maestros");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
