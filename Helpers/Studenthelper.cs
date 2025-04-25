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
                Console.WriteLine("------------GESTION DE ALUMNOS------------\n");
                Console.WriteLine("1. Agregar alumno\n");
                Console.WriteLine("2. Ver alumno\n");
                Console.WriteLine("3. Editar alumno\n");
                Console.WriteLine("4. Eliminar alumno\n");
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
                        AddStudent();
                        break;
                    case 2:
                        ReadStudents();
                        break;
                    case 3:
                        EditStudent();
                        break;
                    case 4:
                        DeleteStudent();
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

        public static void AddStudent()
        {
            Console.Clear();
            using var context = new DataContext();

            Console.Write("Ingrese el nombre del alumno: ");
            var nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese el apellido del alumno: ");
            var apellido = Console.ReadLine();
            if (string.IsNullOrEmpty(apellido))
            {
                Console.WriteLine("El apellido no puede estar vacío.");
                return;
            }

            Console.Write("Ingrese la matricula del alumno (Numero unico): ");
            if (!int.TryParse(Console.ReadLine(), out int matricula))
            {
                Console.WriteLine("Matricula no válida.");
                return;
            }

            if (context.Alumnos.Any(a => a.Matricula == matricula))
            {
                Console.WriteLine("La matricula ya existe. Por favor, ingrese una matricula única.");
                return;
            }

            Console.WriteLine("Cursos disponibles: ");
            var cursos = context.Cursos.ToList();
            foreach (var curso in cursos)
            {
                Console.WriteLine($"ID: {curso.IdCurso}, Nombre: {curso.Nombre}");
            }

            Console.Write("Ingrese el ID del curso asignado: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("ID de curso no válido.");
                return;
            }

            var cursoSeleccionado = context.Cursos.FirstOrDefault(c => c.IdCurso == idCurso);
            if (cursoSeleccionado == null)
            {
                Console.WriteLine("Curso no encontrado.");
                return;
            }


            var alumno = new Alumno
            {
                Nombre = nombre!,  
                Apellido = apellido!,
                Matricula = matricula,
                Curso = cursoSeleccionado,
                IdCurso = idCurso
            };

            context.Alumnos.Add(alumno);
            context.SaveChanges();
            Console.WriteLine("Alumno agregado exitosamente.");
            Program.Pausar();
        }

        public static void ReadStudents()
        {
            Console.Clear();
            using var context = new DataContext();

            var alumnos = context.Alumnos
                .Include(a => a.Curso)
                .ToList();

            Console.WriteLine("Lista de alumnos:");
            foreach (var alumno in alumnos)
            {
                Console.WriteLine($"ID: {alumno.IdPersona}Nombre: {alumno.Nombre}, Apellido: {alumno.Apellido}, Matricula: {alumno.Matricula}, Curso: {alumno.Curso.Nombre}");
            }
            Program.Pausar();
        }

        public static void EditStudent()
        {
            Console.Clear();
            using var context = new DataContext();
            var alumnos = context.Alumnos.ToList();

            Console.Write("Ingrese la matricula del alumno a editar: ");
            var matriculaInput = Console.ReadLine();
            if (!int.TryParse(matriculaInput, out int matricula))
            {
                Console.WriteLine("Matricula no válida.");
                return;
            }
            
            var alumno = context.Alumnos.Include(a => a.Curso).FirstOrDefault(a => a.Matricula == matricula);

            if (alumno == null)
            {
                Console.WriteLine("Alumno no encontrado.");
                return;
            }
            
            
            Console.WriteLine($"Alumno encontrado: {alumno.Nombre} {alumno.Apellido}");

            Console.Write("Nuevo nombre del alumno(Dejar vacio si no desea cambiarlo): ");
            var nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                alumno.Nombre = nuevoNombre!;
            }
            

            Console.Write("Nuevo apellido del alumno(Dejar vacio si no desea cambiarlo): ");
            var nuevoApellido = Console.ReadLine();
            if (string.IsNullOrEmpty(nuevoApellido))
            {
                alumno.Apellido = nuevoApellido!;
            }
            

            Console.Write("Nuevo ID de curso asignado(Dejar vacio si no desea cambiarlo): ");
            var nuevoIDCurso = Console.ReadLine();
            if (!int.TryParse(nuevoIDCurso, out int idCurso))
            {
                alumno.IdCurso = int.Parse(nuevoIDCurso);
            }


            context.SaveChanges();
            Console.WriteLine("Alumno editado exitosamente.");
            Program.Pausar();

            }
        }
                 

        public static void DeleteStudent()
        {
            Console.Clear();
            using var context = new DataContext();
            var alumnos = context.Alumnos.ToList();

            Console.Write("Ingrese la matricula del alumno a eliminar: ");
            var matriculaInput = Console.ReadLine();
            if (!int.TryParse(matriculaInput, out int matricula))
            {
                Console.WriteLine("Matricula no válida.");
                return;
            }
   
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
                Program.Pausar();
            }
        }
    }
}
