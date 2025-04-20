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
                Console.Clear();
                Console.WriteLine("------------GESTION DE CURSOS------------\n");
                Console.WriteLine("1. Agregar curso\n");
                Console.WriteLine("2. Editar curso\n");
                Console.WriteLine("3. Eliminar curso\n");
                Console.WriteLine("4. Volver al menú principal\n");

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
                        EditCourse();
                        break;
                    case 3:
                        DeleteCourse();
                        break;
                    case 4:
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
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) return;
            string nombre = input;

            using var context = new DataContext();
            {
                var curso = new Curso
                {
                    Nombre = nombre
                };
                context.Add(curso);
                context.SaveChanges();
                Console.WriteLine("Curso agregado exitosamente\n");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void EditCourse()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el ID del curso a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using var context = new DataContext();
            var curso = context.Cursos.Find(id);

            if (curso == null)
            {
                Console.WriteLine("Curso no encontrado.");
                return;
            }

            Console.WriteLine($"Nombre actual: {curso.Nombre}");
            Console.WriteLine("Ingrese el nuevo nombre del curso: ");
            string? nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                curso.Nombre = nuevoNombre;
            }

            context.SaveChanges();
            Console.WriteLine("Curso editado exitosamente.\n");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

        }

        public static void DeleteCourse()
        {
            Console.Clear();
            Console.WriteLine("Ingrese el ID del curso a eliminar: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("ID no proporcionado.");
                return;
            }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            using var context = new DataContext();
            {
                var curso = context.Cursos.Find(id);
                if (curso == null)
                {
                    Console.WriteLine("Curso no encontrado.");
                    return;
                }
                context.Cursos.Remove(curso);
                context.SaveChanges();
                Console.WriteLine("Curso eliminado exitosamente.\n");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();

            }
        }
    }
}
        
  
