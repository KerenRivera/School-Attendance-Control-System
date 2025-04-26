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
                Console.WriteLine("2. Ver maestros\n");
                Console.WriteLine("3. Editar maestro\n");
                Console.WriteLine("4. Eliminar maestro\n");
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
                        AddProfessor();
                        break;
                    case 2:
                        ReadProfessor();
                        break;
                    case 3:
                        EditProfessor();
                        break;
                    case 4:
                        DeleteProfessor();
                        break;
                    case 5:
                        salir = true;
                        Console.Clear();
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
            using var context = new DataContext();


            Console.Write("Ingrese el nombre del maestro: ");
            var nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el apellido del maestro: ");
            var apellido = Console.ReadLine();
            if (string.IsNullOrEmpty(apellido))
            {
                Console.WriteLine("El apellido no puede estar vacío.");
                return;
            }

            var materias = context.Materias.ToList();

            if (materias.Count == 0)
            {
                Console.WriteLine("No hay materias disponibles actualmente. Por favor cree un curso primero.");
                Program.Pausar();
                return;
            }
            
            
            Console.WriteLine("Materias disponibles:");
            foreach (var m in materias)
            {
                Console.WriteLine($"Id: {m.IdMateria}, Nombre: {m.Nombre}");
            }
           

            Console.Write("Ingrese el Id de la materia que impartirá: ");
            if (!int.TryParse(Console.ReadLine(), out int idMateria)) //chequear
            {
                Console.WriteLine("El Id de la materia debe ser un número válido.");
                Program.Pausar();
                return;
            }

            var materia = context.Materias.FirstOrDefault(m => m.IdMateria == idMateria);
            if (materia == null)
            {
                Console.WriteLine("La materia no existe.");
                Program.Pausar();
                return;
            }

            var nuevomaestro = new Maestro
            {
                Nombre = nombre,
                Apellido = apellido,
                IdMateria = idMateria,
                Materia = materia
            };

            context.Maestros.Add(nuevomaestro);
            context.SaveChanges();

            Console.WriteLine("Maestro agregado exitosamente.");
            Program.Pausar();
            return;

        }

        public static void ReadProfessor() //chequear
        {
            Console.Clear();
            using var context = new DataContext();
            var maestros = context.Maestros.ToList();

            if (maestros.Count == 0)
            {
                Console.WriteLine("No hay maestros registrados.");
                Program.Pausar();
                return;
            }


            Console.WriteLine("Lista de maestros:");
            foreach (var maestro in maestros)
            {
                Console.WriteLine($"ID: {maestro.IdPersona}, Nombre: {maestro.Nombre}, Apellido: {maestro.Apellido}");
            }
            Program.Pausar();
            return;
        }

        public static void EditProfessor()
        {
            Console.Clear();
            Console.Write("Ingrese el ID del maestro a editar: ");
            if (!int.TryParse(Console.ReadLine(), out int idMaestro)) //chequear
            {
                Console.WriteLine("El ID ingresado no es válido.");
                Program.Pausar();
                return; 
            }

            using var context = new DataContext();
            var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == idMaestro);

            if (maestro == null)
            {
                Console.WriteLine("Maestro no encontrado.");
                Program.Pausar();
                return;
            }
            else
            {
                Console.WriteLine($"Nombre actual: {maestro.Nombre}");
                Console.Write("Nuevo nombre del maestro (Dejar vacío si no desea cambiarlo): ");
                var nuevoNombre = Console.ReadLine();

                if (!string.IsNullOrEmpty(nuevoNombre))
                {
                    maestro.Nombre = nuevoNombre;
                }
                Console.Write("Nuevo apellido del maestro (Dejar vacío si no desea cambiarlo): ");
                var nuevoApellido = Console.ReadLine();

                if (!string.IsNullOrEmpty(nuevoApellido))
                {
                    maestro.Apellido = nuevoApellido;
                }
                Console.Write("Nuevo ID de materia (Dejar vacío si no desea cambiarlo): ");
                var nuevoIdMateria = Console.ReadLine();

                if (int.TryParse(nuevoIdMateria, out int idMateria))
                {
                    var nuevaMateria = context.Materias.FirstOrDefault(m => m.IdMateria == idMateria);
                    if (nuevaMateria != null)
                    {
                        maestro.IdMateria = idMateria;
                        maestro.Materia = nuevaMateria;
                    }
                    else
                    {
                        Console.WriteLine("La materia no existe.");
                        Program.Pausar();
                        return;
                    }

                }
                context.SaveChanges();
                Console.WriteLine("Maestro editado exitosamente.");
                Program.Pausar();

            }
        }

        public static void DeleteProfessor()
        {
            Console.Clear();
            Console.WriteLine("ID del maestro a eliminar:");
            if (!int.TryParse(Console.ReadLine(), out int idMaestro)) //chequear
            {
                Console.WriteLine("El ID ingresado no es válido.");
                Program.Pausar();
                return;
            }

            using var context = new DataContext();
            var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == idMaestro);

            if (maestro == null)
            {
                Console.WriteLine("Maestro no encontrado.");
                Program.Pausar();
                return;
            }
            
            context.Maestros.Remove(maestro);
            context.SaveChanges();
            Console.WriteLine("Maestro eliminado exitosamente.");
            Program.Pausar();

        }
    }
}

