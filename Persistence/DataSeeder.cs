using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Domain;
using Sistema_de_Gestion_de_asistencias.Helpers;


namespace Sistema_de_Gestion_de_asistencias.Persistence
{
    public static class DataSeeder
    {
        public static void Seed(DataContext context)
        {
            if (!context.Cursos.Any())
            {
                var curso1 = new Curso { Nombre = "3ro A" };
                var curso2 = new Curso { Nombre = "4to B" };
                context.Cursos.AddRange(curso1, curso2);


                var materia1 = new Materia { Nombre = "Matematicas" };
                var materia2 = new Materia { Nombre = "Sociales" };
                context.Materias.AddRange(materia1, materia2);


                var alumno1 = new Alumno { Nombre = "Bethania", Apellido = "Rodriguez", Matricula = 1001, Curso = curso1 };
                var alumno2 = new Alumno { Nombre = "Yeilin", Apellido = "Martinez", Matricula = 1002, Curso = curso1 };
                var alumno3 = new Alumno { Nombre = "Rose", Apellido = "Ortiz", Matricula = 1003, Curso = curso2 };
                context.Alumnos.AddRange(alumno1, alumno2, alumno3);


                var maestro1 = new Maestro { Nombre = "Daniela", Apellido = "De la Cruz", Materia = materia1 };
                var maestro2 = new Maestro { Nombre = "Fabiani", Apellido = "Castillo", Materia = materia2 };
                context.Maestros.AddRange(maestro1, maestro2);
                context.SaveChanges();


                var clase1 = new Clase
                {
                    IdMateria = materia1.IdMateria,
                    IdCurso = curso1.IdCurso,
                    Horario = new DateTime(2025, 4, 24, 8, 0, 0),
                    EsTitular = true,
                    Maestro = maestro1,
                    Materia = materia1,
                    Curso = curso1,

                };

                var clase2 = new Clase
                {
                    IdMateria = materia2.IdMateria,
                    IdCurso = curso2.IdCurso,
                    Horario = new DateTime(2025, 4, 24, 10, 0, 0),
                    EsTitular = false,
                    Maestro = maestro2,
                    Materia = materia2,
                    Curso = curso2,
                 
                };
                context.Clases.AddRange(clase1, clase2);
                context.SaveChanges();

                var asistencia1 = new Asistencia
                {
                    IdPersona = alumno1.IdPersona,
                    IdCurso = curso1.IdCurso,
                    IdClase = clase1.IdClase,
                    Estado = Asistencia.EstadoAsistencia.Ausente,
                    Fecha = new DateTime(2025, 4, 24),
                    Alumno = alumno1,
                    Curso = curso1,

                };

                var asistencia2 = new Asistencia
                {
                    IdPersona = alumno2.IdPersona,
                    IdCurso = curso1.IdCurso,
                    IdClase = clase1.IdClase,
                    Estado = Asistencia.EstadoAsistencia.Presente,
                    Fecha = new DateTime(2025, 4, 24),
                    Alumno = alumno2,
                    Curso = curso1,
                };

                var asistencia3 = new Asistencia
                {
                    IdPersona = alumno3.IdPersona,
                    IdCurso = curso2.IdCurso,
                    IdClase = clase2.IdClase,
                    Estado = Asistencia.EstadoAsistencia.Tarde,
                    Fecha = new DateTime(2025, 4, 24),
                    Alumno = alumno3,
                    Curso = curso2,
                };
                context.Asistencias.AddRange(asistencia1, asistencia2, asistencia3);
                context.SaveChanges();
            }
        }
    }
}
