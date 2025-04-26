using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Domain;
using static Sistema_de_Gestion_de_asistencias.Domain.Materia;
using Sistema_de_Gestion_de_asistencias.Persistence;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class SubjectHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE MATERIAS------------\n");
                Console.WriteLine("1. Agregar materia\n");
                Console.WriteLine("2. Ver materias\n");
                Console.WriteLine("3. Editar materia\n");
                Console.WriteLine("4. Eliminar materia\n");
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
                        AddSubject();
                        break;
                    case 2:
                        ReadSubject();
                        break;
                    case 3:
                        EditSubject();
                        break;
                    case 4:
                        DeleteSubject();
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

        public static void AddSubject()
        {
            Console.Clear();
            Console.WriteLine("Nombre de la nueva materia: ");
            string? input = Console.ReadLine();
            string nombre = input?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            using var context = new DataContext();

            if (context.Materias.Any(m => m.Nombre.ToLower() == nombre.ToLower()))
            {
                Console.WriteLine("Ya existe una materia con ese nombre.");
                return;
            }

            var nuevaMateria = new Materia { Nombre = nombre };
            context.Materias.Add(nuevaMateria);
            context.SaveChanges();
            Console.WriteLine($"Materia '{nombre}' agregada exitosamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

        }

        public static void ReadSubject()
        {
            Console.Clear();
            using var context = new DataContext();

            var materias = context.Materias.ToList();
            if (materias.Count == 0)
            {
                Console.WriteLine("No hay materias registradas.");
                Program.Pausar();
                return;
            }
            Console.WriteLine("Lista de materias:");
            foreach (var materia in materias)
            {
                Console.WriteLine($"ID: {materia.IdMateria}, Nombre: {materia.Nombre}");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            return;
        }

        public static void EditSubject()
        {
            Console.Clear();
            using var context = new DataContext();

            if (!context.Materias.Any())
            {
                Console.WriteLine("No hay materias registradas para editar.");
                Program.Pausar();
                return;
            }

            ReadSubject();


            Console.Write("Ingrese el ID de la materia a editar: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("El ID ingresado no es válido.");
                Program.Pausar();
                return;
            }

            var materia = context.Materias.Find(id);

            if (materia == null)
            {
                Console.WriteLine("Materia no encontrada.");
                Program.Pausar();
                return;
            }          
            
            Console.Write("Ingrese el nuevo nombre de la materia: ");
            var nuevoNombre = Console.ReadLine();

            if (string.IsNullOrEmpty(nuevoNombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                Program.Pausar();
                return;
            }
            materia.Nombre = nuevoNombre;
            context.SaveChanges();

            Console.WriteLine($"Materia '{nuevoNombre}' editada exitosamente.");
            Program.Pausar();
            
        }

        public static void DeleteSubject()
        {
            Console.Clear();
            using var context = new DataContext();
            var materias = context.Materias.ToList();

            Console.WriteLine("Ingrese el ID de la materia a eliminar: ");
            if (int.TryParse(Console.ReadLine(),out int id))
            {                              
                var materia = context.Materias.Find(id);

                if (materia == null)
                {
                    Console.WriteLine("Materia no encontrada.");
                    Program.Pausar();
                    return;
                }
                Console.WriteLine($"¿Está seguro de que desea eliminar la materia '{materia.Nombre}'? (S/N): ");
                string? confirmacion = Console.ReadLine();
                if (confirmacion?.ToUpper() == "S")
                {
                    context.Materias.Remove(materia);
                    context.SaveChanges();
                    Console.WriteLine($"Materia '{materia.Nombre}' eliminada exitosamente.");
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
                Program.Pausar();
            }
        }
    }
}




    




