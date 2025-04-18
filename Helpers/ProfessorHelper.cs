using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class ProfessorHelper
    {
        public static void ShowSubmenu()
        {
            int option;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE MAESTROS------------\n");
                Console.WriteLine("1. Agregar maestro\n");
                Console.WriteLine("2. Editar maestro\n");
                Console.WriteLine("3. Eliminar maestro\n");
                Console.WriteLine("4. Atrás\n");
                Console.Write("Seleccione una opción: ");
                option = Convert.ToInt32(Console.ReadLine());

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
                        Console.WriteLine("Regresando al menú principal...");
                        Program.MainMenu();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                        break;
                }
            } while (option != 4);
        }

        public static void AddProfessor()
        {
            Console.Write("Ingrese el nombre del maestro: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese el apellido del maestro: ");
            string apellido = Console.ReadLine();
            Console.Write("Ingrese el Id de la materia que impartira: "); //corregir ortografia.
            int idMateria = int.Parse(Console.ReadLine());

            using (var context = new DdContext())
            {
                var maestro = new Maestro
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    IdMateria = idMateria
                };

                context.Maestros.Add(maestro);
                context.SaveChanges();
                Console.WriteLine("Maestro agregado exitosamente.");
            }
        }

        public static void EditProfessor()
        {
            Console.Write("Ingrese el ID del maestro a editar: ");
            int id = int.Parse(Console.ReadLine()); //check

            using (var context = new DbContext())
            {
                var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == id);

                if (maestro == null)
                {
                    Console.WriteLine("maestro no encontrado.");
                    return;
                }
                else
                {
                    Console.Write("Nuevo nombre del maestro (Dejar vacio si no desea cambiarlo): ");
                    string nuevoNombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombre))
                    {
                        maestro.Nombre = nuevoNombre;
                    }
                    Console.Write("Nuevo apellido del maestro (Dejar vacio si no desea cambiarlo): ");
                    string nuevoApellido = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoApellido))
                    {
                        maestro.Apellido = nuevoApellido;
                    }
                    Console.Write("Nuevo ID de materia (Dejar vacio si no desea cambiarlo): ");
                    string nuevoIDCurso = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoIDMateria))
                    {
                        maestro.IdMateria = int.Parse(nuevoIDMateria);
                    }
                    context.SaveChanges();
                    Console.WriteLine("Maestro editado exitosamente.");
                }
            }
        }

        public static void DeleteProfessor()
        {
            Console.Write("Ingrese el ID del maestro a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            using (var context = new DbContext())
            {
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
                }
            }
        }
    }
}

