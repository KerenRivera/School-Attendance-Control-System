using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Domain;
using static Sistema_de_Gestion_de_asistencias.Domain.Maestro;
using Sistema_de_Gestion_de_asistencias.Persistence;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class ProfessorHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE MAESTROS------------\n");
                Console.WriteLine("1. Agregar maestro\n");
                Console.WriteLine("2. Editar maestro\n");
                Console.WriteLine("3. Eliminar maestro\n");
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
                        AddProfessor();
                        break;
                    case 2:
                        EditProfessor();
                        break;
                    case 3:
                        DeleteProfessor();
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

        public static void AddProfessor()
        {
            Console.Clear();
            Console.Write("Ingrese el nombre del maestro: ");
            string? nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el apellido del maestro: ");
            string? apellido = Console.ReadLine();
            if (string.IsNullOrEmpty(apellido))
            {
                Console.WriteLine("El apellido no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el Id de la materia que impartirá: ");
            if (!int.TryParse(Console.ReadLine(), out int idMateria))
            {
                Console.WriteLine("El Id de la materia debe ser un número válido.");
                return;
            }

            using var context = new DataContext();
            var maestro = new Maestro
            {
                Nombre = nombre,
                Apellido = apellido,
                IdMateria = idMateria
            };

            context.Maestros.Add(maestro);
            context.SaveChanges();
            Console.WriteLine("Maestro agregado exitosamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

        } //funciona

        public static void EditProfessor()
        {
            Console.Clear();
            Console.Write("Ingrese el ID del maestro a editar: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int id))
            {
                Console.WriteLine("El ID ingresado no es válido.");
                return;
            }

            using var context = new DataContext();
            var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == id);

            if (maestro == null)
            {
                Console.WriteLine("Maestro no encontrado.");
                return;
            }
            else
            {
                Console.WriteLine($"Nombre actual: {maestro.Nombre}");
                Console.Write("Nuevo nombre del maestro (Dejar vacío si no desea cambiarlo): ");
                string? nuevoNombre = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoNombre))
                {
                    maestro.Nombre = nuevoNombre;
                }
                Console.Write("Nuevo apellido del maestro (Dejar vacío si no desea cambiarlo): ");
                string? nuevoApellido = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoApellido))
                {
                    maestro.Apellido = nuevoApellido;
                }
                Console.Write("Nuevo ID de materia (Dejar vacío si no desea cambiarlo): ");
                string? nuevoIdMateria = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoIdMateria) && int.TryParse(nuevoIdMateria, out int idMateria))
                {
                    maestro.IdMateria = idMateria;
                }
                context.SaveChanges();
                Console.WriteLine("Maestro editado exitosamente.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();

            }
        }

        public static void DeleteProfessor()
        {
            Console.Clear();
            Console.Write("Ingrese el ID del maestro a eliminar: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int id))
            {
                Console.WriteLine("El ID ingresado no es válido.");
                return;
            }

            using var context = new DataContext();
            var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == id);

            if (maestro == null)
            {
                Console.WriteLine("Maestro no encontrado.");
                return;
            }
            else
            {
                context.Maestros.Remove(maestro);
                context.SaveChanges();
                Console.WriteLine("Maestro eliminado exitosamente.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}

