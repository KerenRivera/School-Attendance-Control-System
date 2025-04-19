using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Sistema_de_Gestion_de_asistencias.Asistencia;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class AssistanceHelper
    {
        public static void ShowSubmenu()
        {
            int option;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE ASISTENCIAS------------\n");
                Console.WriteLine("1. Registrar Asistencia\n");
                Console.WriteLine("2. Visualizar el historial de asistencias\n");
                Console.WriteLine("3. Volver al menú principal\n");
                Console.Write("Seleccione una opción: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddAssistance();
                        break;
                    case 2:
                        ViewAssistanceHistory();
                        break;
                    case 3:
                        Program.MainMenu();
                        Console.WriteLine("Regresando al menú principal");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                        
                }

            } while (option != 3);

        }

        public static void AddAssistance()
        {
            Console.WriteLine("Ingrese la matricula del estudiante que desea registrar");
            int matricula = Convert.ToInt32(Console.ReadLine());

            using (var context = new DbContext())
            {
                var alumno = context.Alumnos.Include(a => a.Curso).FirstOrDefault(a => a.Matricula == matricula);
                if (alumno == null)
                {
                    Console.WriteLine("Alumno no encontrado");
                    return;
                }
                Console.WriteLine("Seleccione el estado de asistencia: \n");
                Console.WriteLine("P - Presente");
                Console.WriteLine("A - Ausente");
                Console.WriteLine("E - Excusa");
                Console.WriteLine("T - Tarde");
                char estadoAnswer = Convert.ToChar(Console.ReadLine());

                EstadoAsistencia estado;

                switch(estadoAnswer)
                {
                    case 'P':
                        estado = EstadoAsistencia.Presente;
                        break;
                    case 'A':
                        estado = EstadoAsistencia.Ausente;
                        break;
                    case 'E':
                        estado = EstadoAsistencia.Excusa;
                        break;
                    case 'T':
                        estado = EstadoAsistencia.Tarde;
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        return;
                }

                var nuevaAsistencia = new Asistencia
                {
                    IdPersona = alumno.IdPersona,
                    IdCurso = alumno.IdCurso,
                    Alumno = alumno,
                    Curso = alumno.Curso,
                    Estado = estado
                };
                context.Asistencias.Add(nuevaAsistencia);
                context.SaveChanges();
                Console.WriteLine("Asistencia registrada exitosamente.");


            }

        }

        public static void ViewAssistanceHistory()
        {
            using var (context = new DbContext())
            {
                var asistencias = Context.Asistencias
                    .Include(a => a.Alumno)
                    .Include(a => a.Curso)
                    .ToList();

                foreach (var a in asistencias)
                {
                    Console.WriteLine($"Alumno: {a.Alumno.Nombre} {a.Alumno.Apellido} | Curso: {a.Alumno.Nombre} | Estado: {a.Estado}");
                }

            }
        }
    }
}
