using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Domain;
using Sistema_de_Gestion_de_asistencias.Persistence;
using static Sistema_de_Gestion_de_asistencias.Domain.Asistencia;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class AssistanceHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE ASISTENCIAS------------\n");
                Console.WriteLine("1. Registrar Asistencia\n");
                Console.WriteLine("2. Visualizar el historial de asistencias\n");
                Console.WriteLine("3. Volver al menú principal\n");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AddAssistance();
                        break;
                    case 2:
                        ViewAssistanceHistory();
                        break;
                    case 3:
                        salir = true;
                        Console.WriteLine("Regresando al menú principal");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una opción válida.");
                        break;
                }

            } while (!salir);
        }

        public static void AddAssistance()
        {
            Console.WriteLine("Ingrese la matricula del estudiante que desea registrar");
            if (!int.TryParse(Console.ReadLine(), out int matricula))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
                return;
            }
            using var context = new DataContext() ;
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
            //char estadoAnswer = Convert.ToChar(Console.ReadLine());
            char? estadoAnswerInput = Console.ReadLine()?.FirstOrDefault();
            if (estadoAnswerInput == null)
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un carácter válido.");
                return;
            }
            char estadoAnswer = estadoAnswerInput.Value;

            EstadoAsistencia estado;

            switch (estadoAnswer)
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
                IdCurso = (int)alumno.IdCurso,
                Alumno = alumno,
                Curso = alumno.Curso,
                Estado = estado,
                Fecha = DateTime.Now
            };
            context.Asistencias.Add(nuevaAsistencia);
            context.SaveChanges();
            Console.WriteLine("Asistencia registrada exitosamente.\n");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

        }

        public static void ViewAssistanceHistory()
        {
            using var context = new DataContext();
            {
                var asistencias = context.Asistencias
                    .Include(a => a.Alumno)
                    .Include(a => a.Curso)
                    .ToList();

                foreach (var a in asistencias)
                {
                    Console.WriteLine($"Alumno: {a.Alumno.Nombre} {a.Alumno.Apellido} | Curso: {a.Curso.Nombre} | Estado: {a.Estado} | Fecha: {a.Fecha}");
                }
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();


            }
        }
    }
}
