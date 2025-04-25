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
                //Console.Clear();
                Console.WriteLine("------------GESTION DE ASISTENCIAS------------\n");
                Console.WriteLine("1. Registrar Asistencia\n");
                Console.WriteLine("2. Visualizar el historial de asistencias\n");
                Console.WriteLine("3. Editar Asistencia\n");
                Console.WriteLine("4. Eliminar Asistencia\n");
                Console.WriteLine("5. Volver al menú principal\n");
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
                        EditAssistance();
                        break;
                    case 4:
                        DeleteAssistance();
                        break;
                    case 5:
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
            Console.Clear();
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

            if (TryGetEstado(out EstadoAsistencia estado)) return;

            var nuevaAsistencia = new Asistencia
            {
                IdPersona = alumno.IdPersona,
                IdCurso = (int)alumno.IdCurso,
                Alumno = alumno,
                Curso = alumno.Curso,
                Estado = estado,
                Fecha = DateTime.Today
            };

            context.Asistencias.Add(nuevaAsistencia);
            context.SaveChanges();

            Console.WriteLine("Asistencia registrada exitosamente.\n");
            Program.Pausar();

        }

        public static void ViewAssistanceHistory()
        {
            Console.Clear();
            using var context = new DataContext();
            {
                var asistencias = context.Asistencias
                    .Include(a => a.Alumno)
                    .Include(a => a.Curso)
                    .ToList();

                if (!asistencias.Any())
                {
                    Console.WriteLine("No hay asistencias registradas.");
                    Program.Pausar();
                    return;
                }

                    foreach (var a in asistencias)
                {
                    Console.WriteLine($"Alumno: {a.Alumno.Nombre} {a.Alumno.Apellido} | Curso: {a.Curso.Nombre} | Estado: {a.Estado} | Fecha: {a.Fecha}");
                }
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();


            }
        }

        public static void EditAssistance()
        {
            Console.Clear();
            Console.WriteLine("Ingrese la matricula del estudiante que desea editar: ");

            if (!int.TryParse(Console.ReadLine(), out int matricula))
            {
                Console.WriteLine("Matricula inválida.");
                return;
            }

            using var context = new DataContext();
            var asistencias = context.Asistencias
                .Include(a => a.Alumno)
                .Include(a => a.Curso)
                .Where(a => a.Alumno.Matricula == matricula)
                .ToList();

            if (!asistencias.Any())
            {
                Console.WriteLine("Este estudiante aún no tiene asistencias registradas.");
                return;
            }

            for (int i = 0; i < asistencias.Count; i++)
            {
                var a = asistencias[i];
                Console.WriteLine($"{i + 1}. Alumno: {a.Alumno.Nombre} {a.Alumno.Apellido} | Curso: {a.Curso.Nombre} | Estado: {a.Estado} | Fecha: {a.Fecha}");
            }

            Console.WriteLine("Seleccione el número de la asistencia que desea editar: ");
            if (!int.TryParse(Console.ReadLine(), out int asistenciaIndex) || asistenciaIndex < 1 || asistenciaIndex > asistencias.Count)
            {
                Console.WriteLine("Número de asistencia inválido.");
                return;
            }

            var asistenciaSeleccionada = asistencias[asistenciaIndex - 1];
            if (!TryGetEstado(out EstadoAsistencia nuevoEstado)) return;

            asistenciaSeleccionada.Estado = nuevoEstado;
            context.SaveChanges();

            Console.WriteLine("Asistencia editada exitosamente.");
            Program.Pausar();

        }

        public static void DeleteAssistance()
        {
            Console.Clear();
            Console.WriteLine("Ingrese la matricula del estudiante que desea eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int matricula))
            {
                Console.WriteLine("Matricula inválida.");
                return;
            }

            using var context = new DataContext();
            var asistencias = context.Asistencias
                .Include(a => a.Alumno)
                .Include(a => a.Curso)
                .Where(a => a.Alumno.Matricula == matricula)
                .ToList();


            if (!asistencias.Any())
            {
                Console.WriteLine("Este estudiante aún no tiene asistencias registradas.");
                return;
            }


            for (int i = 0; i < asistencias.Count; i++)
            {
                var a = asistencias[i];
                Console.WriteLine($"{i + 1}.| Curso: {a.Curso.Nombre} | Estado: {a.Estado} | Fecha: {a.Fecha}");
            }


            Console.WriteLine("Seleccione el número de la asistencia que desea eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int asistenciaIndex) || asistenciaIndex < 1 || asistenciaIndex > asistencias.Count)
            {
                Console.WriteLine("Número de asistencia inválido.");
                return;
            }

            var asistenciaSeleccionada = asistencias[asistenciaIndex - 1];
            context.Asistencias.Remove(asistenciaSeleccionada);
            context.SaveChanges();

            Console.WriteLine("Asistencia eliminada exitosamente.");
            Program.Pausar();
        }

        public static bool TryGetEstado(out EstadoAsistencia estado)
        {
            estado = default;
            Console.WriteLine("Seleccione el estado de asistencia: [P] Presente | [A] Ausente | [E] Excusa | [T] Tarde");
            char? input = Console.ReadLine()?.ToUpper().FirstOrDefault();

            switch (input)
            {
                case 'P': estado = EstadoAsistencia.Presente; return true;
                case 'A': estado = EstadoAsistencia.Ausente; return true;
                case 'E': estado = EstadoAsistencia.Excusa; return true;
                case 'T': estado = EstadoAsistencia.Tarde; return true;
                default: Console.WriteLine("Entrada inválida."); return false;
            }
        }

    }
}
