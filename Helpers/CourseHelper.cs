using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sistema_de_Gestion_de_asistencias.Domain;
using static Sistema_de_Gestion_de_asistencias.Domain.Curso;
using Sistema_de_Gestion_de_asistencias.Persistence;
using Microsoft.IdentityModel.Tokens;
using Google.Protobuf;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class CourseHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.WriteLine("------------GESTION DE CURSOS------------\n");
                Console.WriteLine("1. Agregar curso\n");
                Console.WriteLine("2. Ver cursos\n");
                Console.WriteLine("3. Editar curso\n");
                Console.WriteLine("4. Eliminar curso\n");
                Console.WriteLine("5. Volver al menú principal\n");

                Console.Write("Seleccione una opción: ");
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Entrada inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddCourse(); 
                        break;
                    case 2:
                        ReadCourses(); 
                        break;
                    case 3:
                        EditCourse();  
                        break;
                    case 4:
                        DeleteCourse();  
                        break;
                    case 5:
                        salir = true;
                        Console.WriteLine("Regresando al menú principal...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                        break;
                }
            } while (!salir);
        }

        public static void AddCourse()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el nombre del curso: ");
            var nombre = Console.ReadLine();

            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre del curso no puede estar vacio");
                return;
            }

            using var context = new DataContext();

            if (context.Cursos.Any(c => c.Nombre.ToLower() == nombre.ToLower()))
            {
                Console.WriteLine("El curso ya existe.");
                return;
            }

            var nuevoCurso = new Curso
            {
                Nombre = nombre
            };

            context.Add(nuevoCurso);
            context.SaveChanges();
            Console.WriteLine("Curso agregado exitosamente\n");
            Program.Pausar();
        }

        public static void ReadCourses()
        {
            Console.Clear();
            using var context = new DataContext();

            var cursos = context.Cursos.ToList();

            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos disponibles.");
                return;
            }
            Console.WriteLine("Lista de cursos:");
            foreach (var curso in cursos)
            {
                Console.WriteLine($"ID: {curso.IdCurso}, Nombre: {curso.Nombre}");
            }

            Program.Pausar();
        }

        public static void EditCourse()
        {
            Console.Clear();
            using var context = new DataContext();
            var cursos = context.Cursos.ToList();

            if (cursos.Count == 0)
            {
                Console.WriteLine("No hay cursos disponibles.");
                return;
            }

            Console.WriteLine("Ingrese el ID del curso a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var curso = context.Cursos.FirstOrDefault(c => c.IdCurso == idCurso);

            if (curso == null)
            {
                Console.WriteLine("Curso no encontrado.");
                return;
            }

            Console.WriteLine($"Nombre actual: {curso.Nombre}");
            Console.WriteLine("Ingrese el nuevo nombre del curso: (Presione la tecla Enter para mantener el nombre actual)) ");
            var nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                curso.Nombre = nuevoNombre;
            }

            context.SaveChanges();
            Console.WriteLine("Curso editado exitosamente.\n");
            Program.Pausar();
        }

        public static void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el ID del curso a eliminar: ");

            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using var context = new DataContext();

            var curso = context.Cursos.Include(c => c.Alumnos).FirstOrDefault(c => c.IdCurso == idCurso);

            if (curso == null)
            {
                Console.WriteLine("Curso no encontrado.");
                return;
            }

            if (curso.Alumnos.Any())
            {
                Console.WriteLine("No se puede eliminar el curso porque tiene estudiantes inscritos.");
                return;
            }
            Console.WriteLine($"¿Está seguro de que desea eliminar el curso '{curso.Nombre}'? (S/N): ");
            var confirmacion = Console.ReadLine();
            if (confirmacion?.ToUpper() != "S")
            {
                Console.WriteLine("Eliminación cancelada.");
                return;
            }

            context.Cursos.Remove(curso);
            context.SaveChanges();
            Console.WriteLine("Curso eliminado exitosamente.\n");
            Program.Pausar();
        }
    }
}
        
  
