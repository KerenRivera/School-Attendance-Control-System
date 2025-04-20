using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Persistence;
using Sistema_de_Gestion_de_asistencias.Domain;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class Report
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("----- Menú de reportes ------");
                Console.WriteLine("1. Reporte semanal de asistencia");
                Console.WriteLine("2. Volver al menú principal");
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
                        WeeklyAssisttanceReport();
                        break;
                    case 2:
                        salir = true;
                        Console.WriteLine("Volviendo al menú principal");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }   while (salir != false);
        }

        public static void WeeklyAssisttanceReport()
        {
            Console.WriteLine("----- Reporte Semanal de Asistencia -----");
            Console.Write("Ingrese fecha de inicio (yyyy-MM-dd): ");

            string? input = Console.ReadLine();
            if (!DateTime.TryParse(input, out DateTime inicio))
            {
                Console.WriteLine("Fecha no válida.");
                return;
            }

            DateTime fin = inicio.AddDays(5);

            using var context = new DataContext();
            {
                var asistencias = context.Asistencias
                    .Include(a => a.Alumno)
                    .Include(a => a.Curso)
                    .Include(a => a.Estado)
                    .Where(a => a.Fecha >= inicio && a.Fecha <= fin)
                    .ToList();

                if (asistencias.Count == 0)
                {
                    Console.WriteLine("No hay registros para esta semana.");
                    Console.ReadKey();
                    return;
                }

                var grupos = asistencias
                    .GroupBy(a => a.Curso?.Nombre)
                    .ToList();

                foreach (var grupo in grupos)
                {
                    Console.WriteLine($"\nCurso: {grupo.Key}");
                    var porEstudiante = grupo.GroupBy(a => a.Alumno);

                    foreach (var estudiante in porEstudiante)
                    {
                        int total = estudiante.Count();
                        int asistenciasOk = estudiante.Count(a => a.Estado == Asistencia.EstadoAsistencia.Presente);
                        Console.WriteLine($"Alumno: {estudiante.Key?.Nombre} {estudiante.Key?.Apellido} - {asistenciasOk}/{total} asistencias");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
