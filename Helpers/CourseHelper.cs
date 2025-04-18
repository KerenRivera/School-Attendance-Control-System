using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class CourseHelper
    {
        public static void ShowSubmenu()
        {
            int option;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE CURSOS------------\n");
                Console.WriteLine("1. Agregar curso\n");
                Console.WriteLine("2. Editar curso\n");
                Console.WriteLine("3. Eliminar curso\n");
                Console.WriteLine("4. Atrás\n");
                Console.Write("Seleccione una opción: ");
                option = Convert.ToInt32(Console.ReadLine());

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
                        Console.WriteLine("Regresando al menú principal...");
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                        break;
                }
            } while (option != 4);
        }

        public static void AddCourse()
        {
            Console.WriteLine("Ingrese el nombre del curso: ");
            string nombre = Console.ReadLine();

            using (var context = new DbContext())
            {
                var curso = new Curso
                {
                    Nombre = nombre
                };
                context.Add(curso);
                context.SaveChanges();
                Console.WriteLine("Curso agregado exitosamente");
            }
        }

        public static void EditCourse()
        {
            Console.WriteLine("Ingrese el ID del curso a editar: ");
            int id = int.Parse(Console.ReadLine());

            using (var context = new DbContext())
            {
                var curso = context.Cursos.Find(id);

                if (curso == null)
                {
                    Console.WriteLine("Curso no encontrado.");
                    return;
                }
                Console.WriteLine("Ingrese el nuevo nombre del curso: ");
                string nuevoNombre = Console.ReadLine();

                if (!string.IsNullOrEmpty(nuevoNombre))
                {
                    curso.Nombre = nuevoNombre;
                }

                context.SaveChanges();
                Console.WriteLine("Curso editado exitosamente.");

            }

        }

        public static void DeleteCourse()
        {
            Console.WriteLine("Ingrese el ID del curso a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            using (var context = new DbContext())
            {
                var curso = context.Cursos.Find(id);
                if (curso == null)
                {
                    Console.WriteLine("Curso no encontrado.");
                    return;
                }
                context.Cursos.Remove(curso);
                context.SaveChanges();
                Console.WriteLine("Curso eliminado exitosamente.");
            }

        }
}
        
  
