using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Domain;
using static Sistema_de_Gestion_de_asistencias.Domain.Alumno;
using Sistema_de_Gestion_de_asistencias.Persistence;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class StudentHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE ALUMNOS------------\n");
                Console.WriteLine("1. Agregar alumno\n");
                Console.WriteLine("2. Editar alumno\n");
                Console.WriteLine("3. Eliminar alumno\n");
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
                        AddStudent();
                        break;
                    case 2:
                        EditStudent();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    case 4:
                        salir = true;
                        Console.WriteLine("Regresando al menú principal...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                        break;
                }
            } while (salir != false);
        }

        public static void AddStudent()
        {
            Console.Write("Ingrese el nombre del alumno: ");
            string? nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el apellido del alumno: ");
            string? apellido = Console.ReadLine();
            if (string.IsNullOrEmpty(apellido))
            {
                Console.WriteLine("El apellido no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el curso asignado: ");
            string? nombreCurso = Console.ReadLine();
            if (string.IsNullOrEmpty(nombreCurso))
            {
                Console.WriteLine("El nombre del curso no puede estar vacío.");
                return;
            }

            using var context = new DataContext();

            var cursoEncontrado = context.Cursos.FirstOrDefault(c => c.Nombre == nombreCurso);

            if (cursoEncontrado == null)
            {
                Console.WriteLine("Curso no encontrado.");
                return;
            }

            var alumno = new Alumno
            {
                Nombre = nombre!,  
                Apellido = apellido!,   
                Curso = cursoEncontrado
            };

            context.Alumnos.Add(alumno);
            context.SaveChanges();
            Console.WriteLine("Alumno agregado exitosamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        public static void EditStudent()
        {
            Console.Write("Ingrese la matricula del alumno a editar: ");
            string? matriculaInput = Console.ReadLine();
            if (string.IsNullOrEmpty(matriculaInput) || !int.TryParse(matriculaInput, out int matricula))
            {
                Console.WriteLine("Matricula no válida.");
                return;
            }

            using var context = new DataContext();
            {
                var alumno = context.Alumnos.Include(a => a.Curso).FirstOrDefault(a => a.Matricula == matricula);

                if (alumno == null)
                {
                    Console.WriteLine("Alumno no encontrado.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Alumno encontrado: {alumno.Nombre} {alumno.Apellido}");

                    Console.Write("Nuevo nombre del alumno(Dejar vacio si no desea cambiarlo): ");
                    string? nuevoNombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombre))
                    {
                        alumno.Nombre = nuevoNombre!; // Use null-forgiving operator to suppress CS8600  
                    }
                    Console.Write("Nuevo apellido del alumno(Dejar vacio si no desea cambiarlo): ");
                    string? nuevoApellido = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoApellido))
                    {
                        alumno.Apellido = nuevoApellido!; // Use null-forgiving operator to suppress CS8600  
                    }
                    Console.Write("Nuevo ID de curso asignado(Dejar vacio si no desea cambiarlo): ");
                    string? nuevoIDCurso = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoIDCurso))
                    {
                        alumno.IdCurso = int.Parse(nuevoIDCurso);
                    }
                    context.SaveChanges();
                    Console.WriteLine("Alumno editado exitosamente.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();

                }
            }
        }

        public static void DeleteStudent()
        {
            Console.Write("Ingrese la matricula del alumno a eliminar: ");
            string? matriculaInput = Console.ReadLine();
            if (string.IsNullOrEmpty(matriculaInput) || !int.TryParse(matriculaInput, out int matricula))
            {
                Console.WriteLine("Matricula no válida.");
                return;
            }

            using var context = new DataContext();
            var alumno = context.Alumnos.Include(a => a.Curso).FirstOrDefault(a => a.Matricula == matricula);

            if (alumno == null)
            {
                Console.WriteLine("Alumno no encontrado.");
                return;
            }
            else
            {
                context.Alumnos.Remove(alumno);
                context.SaveChanges();
                Console.WriteLine("Alumno eliminado exitosamente.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
