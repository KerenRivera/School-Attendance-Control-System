using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class StudentHelper
    {
        public static void ShowSubmenu()
        {
            Console.WriteLine("1. Agregar Alumno\n");
            Console.WriteLine("2. Editar Alumno\n");
            Console.WriteLine("3. Eliminar Alumno\n");
            Console.WriteLine("4. Atrás\n");
            var option = Convert.ToInt32(Console.ReadLine());

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
                    Console.WriteLine("Regresando al menú principal...");
                    Program.MainMenu();
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                    break;
            }
        }

        public static void AddStudent()
        {
            Console.Write("Ingrese el nombre del alumno: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese el apellido del alumno: ");
            string apellido = Console.ReadLine();
            Console.Write("Ingrese el curso asignado: ");
            string Curso = Console.ReadLine();

            using (var context = new DbContext())
            {
                var alumno = new Alumno
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Curso = Curso
                };

                context.Alumnos.Add(alumno);
                context.SaveChanges();
                Console.WriteLine("Alumno agregado exitosamente.");
            }
        }

        public static void EditStudent()
        {
            Console.Write("Ingrese la matricula del alumno a editar: ");
            int matricula = int.Parse(Console.ReadLine());

            using (var context = new DbContext())
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
                    string nuevoNombre = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoNombre))
                    {
                        alumno.Nombre = nuevoNombre;
                    }
                    Console.Write("Nuevo apellido del alumno(Dejar vacio si no desea cambiarlo): ");
                    string nuevoApellido = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoApellido))
                    {
                        alumno.Apellido = nuevoApellido;
                    }
                    Console.Write("Nuevo ID de curso asignado(Dejar vacio si no desea cambiarlo): ");
                    string nuevoIDCurso = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nuevoIDCurso))
                    {
                        alumno.IdCurso = int.Parse(nuevoIDCurso);
                    }
                    context.SaveChanges();
                    Console.WriteLine("Alumno editado exitosamente.");
                }
            }
        }

        public static void DeleteStudent()
        {
            Console.Write("Ingrese la matricula del alumno a eliminar: ");
            int matricula = int.Parse(Console.ReadLine());
            if (matricula == 0)
            {
                Console.WriteLine("Matricula no valida.");
                return;
            }
            using (var context = new DbContext())
            {
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
                }
            }
        }
    }
}
