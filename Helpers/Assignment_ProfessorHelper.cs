using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Persistence;
using Sistema_de_Gestion_de_asistencias.Domain;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class Assignment_ProfessorHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.Clear();
                Console.WriteLine("------------GESTION DE CLASES------------\n");
                Console.WriteLine("1. Crear clase");
                Console.WriteLine("2. Listar clases");
                Console.WriteLine("3. Volver al menú principal"); // corregir ortografia  
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        AssignClass();
                        break;
                    case 2:
                        ListClasses();
                        break;
                    case 3:
                        salir = true;
                        Console.WriteLine("Volviendo al menu principal");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            } while (salir != false);
        }

        public static void AssignClass()
        {
            Console.Write("ID del maestro: ");
            string? inputIdPersona = Console.ReadLine();
            if (!int.TryParse(inputIdPersona, out int idPersona))
            {
                Console.WriteLine("ID del maestro no válido.");
                return;
            }

            Console.Write("ID de la materia: ");
            string? inputIdMateria = Console.ReadLine();
            if (!int.TryParse(inputIdMateria, out int idMateria))
            {
                Console.WriteLine("ID de la materia no válido.");
                return;
            }

            Console.Write("ID del curso: ");
            string? inputIdCurso = Console.ReadLine();
            if (!int.TryParse(inputIdCurso, out int idCurso))
            {
                Console.WriteLine("ID del curso no válido.");
                return;
            }

            Console.Write("Horario (yyyy-MM-dd HH:mm): ");
            string? inputHorario = Console.ReadLine();
            if (!DateTime.TryParse(inputHorario, out DateTime horario))
            {
                Console.WriteLine("Horario no válido.");
                return;
            }

            Console.Write("¿Es titular? (S/N): ");
            string? inputEsTitular = Console.ReadLine();
            bool esTitular = inputEsTitular?.ToUpper() == "S";

            using var context = new DataContext();
            var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == idPersona);
            var materia = context.Materias.FirstOrDefault(m => m.IdMateria == idMateria);
            var curso = context.Cursos.FirstOrDefault(c => c.IdCurso == idCurso);

            if (maestro == null || materia == null || curso == null)
            {
                Console.WriteLine("Error: Maestro, Materia o Curso no encontrado.");
                return;
            }


            var asignacionDocente = new AsignacionDocente
            {
                //IdMaestro = maestro.IdPersona,
                IdMateria = materia.IdMateria,
                IdCurso = curso.IdCurso,
                Horario = horario,
                EsTitular = esTitular,
                Maestro = maestro,
                Curso = curso,
                Materia = materia
            };

            context.AsignacionesDocente.Add(asignacionDocente);
            context.SaveChanges();
            Console.WriteLine("Clase asignada exitosamente.");
        }

        public static void ListClasses()
        {

            Console.WriteLine("-----¿Por qué campo desea filtar?-----");
            Console.WriteLine("1. Fecha");
            Console.WriteLine("2. Docente");
            Console.WriteLine("3. Curso");
            Console.WriteLine("4. Todos");
            Console.Write("Seleccione una opción: ");
            if (!int.TryParse(Console.ReadLine(), out int opcionFiltro))
            {
                Console.WriteLine("Opción no válida. Intente de nuevo.");
                return;
            }

            using var context = new DataContext(); //check this
            {
                var clases = context.AsignacionesDocente
                    .Include(a => a.Maestro)
                    .Include(a => a.Materia)
                    .Include(a => a.Curso)
                    .AsQueryable();

                switch (opcionFiltro)
                {
                    case 1:
                        Console.Write("Ingrese la fecha (yyyy-MM-dd): ");
                        string? inputFecha = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(inputFecha) || !DateTime.TryParse(inputFecha, out var fecha))
                        {
                            Console.WriteLine("Fecha no válida. Intente de nuevo.");
                            return;
                        }
                        clases = clases.Where(c => c.Horario.Date == fecha.Date);
                        break;
                    case 2:
                        Console.Write("Nombre del docente: ");
                        //var nombre = (Console.ReadLine().ToLower
                        var nombre = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            Console.WriteLine("El nombre no puede estar vacío. Intente de nuevo.");
                            return;
                        }
                        clases = clases.Where(c => (c.Maestro.Nombre + " " + c.Maestro.Apellido).Contains(nombre, StringComparison.CurrentCultureIgnoreCase));
                        break;
                    case 3:
                        Console.Write("Nombre del curso: ");
                        var cursoInput = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(cursoInput))
                        {
                            Console.WriteLine("El nombre del curso no puede estar vacío. Intente de nuevo.");
                            return;
                        }
                        var curso = cursoInput.ToLower();
                        clases = clases.Where(c => c.Curso.Nombre.Contains(curso, StringComparison.CurrentCultureIgnoreCase));

                        Console.Write("¿Es titular? (S/N): ");
                        string? inputEsTitular = Console.ReadLine();
                        if (string.IsNullOrEmpty(inputEsTitular))
                        {
                            Console.WriteLine("Entrada no válida. Intente de nuevo.");
                            return;
                        }
                        bool esTitular = inputEsTitular.Equals("S", StringComparison.CurrentCultureIgnoreCase);
                        clases = clases.Where(c => c.EsTitular == esTitular);

                        break;
                    case 4:

                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        return;
                }

                var lista = clases.ToList();

                if (lista.Count == 0)
                {
                    Console.WriteLine("No se encontraron clases.");
                    return;
                }

                foreach (var clase in clases)
                {
                    Console.WriteLine($"Clase ID: {clase.Id}");
                    Console.WriteLine($"Docente: {clase.Maestro.Nombre}");
                    Console.WriteLine($"Materia: {clase.Materia.Nombre}");
                    Console.WriteLine($"Curso: {clase.Curso.Nombre}");

                    Console.WriteLine($"Horario: {clase.Horario}");
                    Console.WriteLine($"Titular: {(clase.EsTitular.HasValue && clase.EsTitular.Value ? "Sí" : "No")}");
                    Console.WriteLine("-----------------------------------");
                }
            }                     
        }
    }
}
