using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Persistence;
using Sistema_de_Gestion_de_asistencias.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class ClassHelper
    {
        public static void ShowSubmenu()
        {
            bool salir = false;
            do
            {
                Console.WriteLine("------------GESTION DE CLASES------------\n");
                Console.WriteLine("1. Crear clase");
                Console.WriteLine("2. Ver clases");
                Console.WriteLine("3. Editar clase");
                Console.WriteLine("4. Eliminar clase");
                Console.WriteLine("5. Volver al menú principal");  
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
                        EditClass();
                        break;
                    case 4:
                        DeleteClass();
                        break;
                    case 5:
                        salir = true;
                        Console.WriteLine("Volviendo al menu principal");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }
            } while (!salir);
        } 

        public static void AssignClass()
        {
            Console.Clear();

            using var context = new DataContext();
            
            var maestros = context.Maestros.ToList(); 

            Console.WriteLine("Lista de maestros:");
            foreach (var m in maestros)
            {
                Console.WriteLine($"ID: {m.IdPersona}, Nombre: {m.Nombre}");
            }


            Console.Write("ID del maestro: ");
            if (!int.TryParse(Console.ReadLine(), out int idMaestro))
            {
                Console.WriteLine("ID del maestro no válido.");
                return;
            }          

            var materias = context.Materias.ToList(); 

            Console.WriteLine("Lista de materias:");
            foreach (var mat in materias)
            {
                Console.WriteLine($"ID: {mat.IdMateria}, Nombre: {mat.Nombre}");
            }


            Console.Write("ID de la materia: ");
            if (!int.TryParse(Console.ReadLine(), out int idMateria))
            {
                Console.WriteLine("ID de la materia no válido.");
                return;
            }


            var cursos = context.Cursos.ToList();

            Console.WriteLine("Lista de cursos:");
            foreach (var c in cursos)
            {
                Console.WriteLine($"ID: {c.IdCurso}, Nombre: {c.Nombre}");
            }


            Console.Write("ID del curso: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("ID del curso no válido.");
                return;
            }


            Console.Write("Horario (dd/MM-yyyy HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime horario))
            {
                Console.WriteLine("Horario no válido.");
                return;
            }


            Console.Write("¿Es titular? (S/N): ");
            string? inputEsTitular = Console.ReadLine();
            bool esTitular = inputEsTitular?.ToUpper() == "S";


            var maestro = context.Maestros.FirstOrDefault(m => m.IdPersona == idMaestro);
            var materia = context.Materias.FirstOrDefault(m => m.IdMateria == idMateria);
            var curso = context.Cursos.FirstOrDefault(c => c.IdCurso == idCurso);

            if (maestro == null || materia == null || curso == null)
            {
                Console.WriteLine("Error: Maestro, Materia o Curso no encontrado.");
                return;
            }


            var clase = new Clase
            {
                IdMateria = materia.IdMateria,
                IdCurso = curso.IdCurso,
                Horario = horario,
                EsTitular = esTitular,
                Maestro = maestro,
                Curso = curso,
                Materia = materia
            };

            context.Clases.Add(clase);
            context.SaveChanges();
            Console.WriteLine("Clase asignada exitosamente.");
            Program.Pausar();
        }

        public static void ListClasses()
        {
            Console.Clear();
            Console.WriteLine("-----¿Por qué campo desea filtar?-----");
            Console.WriteLine("1. Fecha");
            Console.WriteLine("2. Maestro");
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
                var clases = context.Clases
                    .Include(c => c.Maestro)
                    .Include(c => c.Materia)
                    .Include(c => c.Curso)
                    .AsQueryable();

                switch (opcionFiltro)
                {
                    case 1:
                        Console.Write("Ingrese la fecha (dd/MM/yyyy): ");
                        string? inputFecha = Console.ReadLine();
                        if (!DateTime.TryParse(inputFecha, out var fecha))
                        {
                            Console.WriteLine("Fecha no válida. Intente de nuevo.");
                            return;
                        }
                        clases = clases.Where(c => c.Horario.Date == fecha.Date);
                        break;

                    case 2:
                        Console.Write("Nombre del maestro: ");
                        var nombre = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombre))
                        {
                            Console.WriteLine("El nombre no puede estar vacío. Intente de nuevo.");
                            return;
                        }
                        clases = clases.Where(c => 
                            (c.Maestro.Nombre + " " + c.Maestro.Apellido)
                            .Contains(nombre, StringComparison.CurrentCultureIgnoreCase));
                        break;

                    case 3:
                        Console.Write("Nombre del curso: ");
                        var nombreCurso = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nombreCurso))
                        {
                            Console.WriteLine("El nombre del curso no puede estar vacío. Intente de nuevo.");
                            return;
                        }
                        var curso = nombreCurso;
                        clases = clases.Where(c =>
                            c.Curso.Nombre.Contains(nombreCurso, StringComparison.CurrentCultureIgnoreCase));

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
                        //Mostrar todos
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

                foreach (var clase in lista)
                {
                    Console.WriteLine($"Clase ID: {clase.IdClase}");
                    Console.WriteLine($"Docente: {clase.Maestro.Nombre} {clase.Maestro.Apellido}");
                    Console.WriteLine($"Materia: {clase.Materia.Nombre}");
                    Console.WriteLine($"Curso: {clase.Curso.Nombre}");
                    Console.WriteLine($"Horario: {clase.Horario}");
                    Console.WriteLine($"Titular: {(clase.EsTitular == true ? "Sí" : "No")}");
                    Console.WriteLine("-----------------------------------");
                    Program.Pausar();
                }
            }                     
        }

        public static void EditClass()
        {
            Console.Clear();

            using var context = new DataContext();

            var clases = context.Clases.ToList();
            Console.WriteLine("Ingrese el ID de la clase que desea editar");

            if (!int.TryParse(Console.ReadLine(), out int idClase))
            {
                Console.WriteLine("ID de clase no válido.");
                return;
            }

            var clase = context.Clases
                .Include(c => c.Maestro)
                .Include(c => c.Materia)
                .Include(c => c.Curso)
                .FirstOrDefault(c => c.IdClase == idClase);

            if (clase == null)
            {
                Console.WriteLine("Clase no encontrada.");
                return;
            }

            Console.WriteLine($"Editando la clase del maestro {clase.Maestro.Nombre} {clase.Maestro.Apellido}");
            Console.WriteLine($"Horario actual: {clase.Horario:dd/MM/yyyy hh:mm}");
            Console.Write("Nuevo horario (Presione la tecla enter para mantener el horario actual): ");
            var inpHorario = Console.ReadLine();

            if (!string.IsNullOrEmpty(inpHorario) && DateTime.TryParse(inpHorario, out var nuevoHorario))
            {
                clase.Horario = nuevoHorario;
            }

            Console.WriteLine($"¿Es titular actualmente?: {(clase.EsTitular == true ? "Sí" : "No")}");
            Console.Write("¿Desea cambiar puesto de titular? (S/N): ");
            var answerTitular = Console.ReadLine();

            if (!string.IsNullOrEmpty(answerTitular))
            {
                clase.EsTitular = answerTitular.Trim().ToUpper() == "S";               
            }

            Console.WriteLine($"Materia actual: {clase.Materia.Nombre}");
            Console.Write("¿Desea cambiar la materia? (S/N): ");
            var answerMateria = Console.ReadLine();

            if (!string.IsNullOrEmpty(answerMateria) && answerMateria.Trim().ToUpper() == "S")
            {
                Console.WriteLine("Lista de materias:");
                var materias = context.Materias.ToList();
                foreach (var mat in materias)
                {
                    Console.WriteLine($"ID: {mat.IdMateria}, Nombre: {mat.Nombre}");
                }

                Console.Write("ID de la nueva materia: ");
                if (int.TryParse(Console.ReadLine(), out int idMateria))
                {
                    var materia = context.Materias.FirstOrDefault(m => m.IdMateria == idMateria);
                    if (materia != null)
                    {
                        clase.Materia = materia;
                        Console.WriteLine($"Materia cambiada a '{materia.Nombre}'");
                    }
                    else
                    {
                        Console.WriteLine("Materia no encontrada.");
                        return;
                    }

                }

            }

            Console.WriteLine($"Curso actual: {clase.Curso.Nombre}");
            Console.Write("Nuevo ID de curso (Presione la tecla enter para mantener el actual): ");
            var inpCurso = Console.ReadLine();
            if (!string.IsNullOrEmpty(inpCurso) && int.TryParse(inpCurso, out int nuevoIdCurso))
            {
                var nuevocurso = context.Cursos.FirstOrDefault(c => c.IdCurso == nuevoIdCurso);
                if (nuevocurso != null)
                {
                    clase.IdCurso = nuevoIdCurso;
                    clase.Curso = nuevocurso;
                    Console.WriteLine($"Curso cambiado a '{nuevocurso.Nombre}'");
                }
                else
                {
                    Console.WriteLine("Curso no encontrado.");
                    return;
                }
            }

            context.SaveChanges();
            Console.WriteLine("Clase actualizada exitosamente.");

        }


        public static void DeleteClass()
        {
            Console.Clear();
            using var context = new DataContext();

            Console.WriteLine("Ingrese el ID de la clase a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int idClase))
            {
                Console.WriteLine("ID de clase no válido.");
                return;
            }

            var clase = context.Clases
                .Include(c => c.Maestro)
                .Include(c => c.Materia)
                .Include(c => c.Curso)
                .FirstOrDefault(c => c.IdClase == idClase);

            if (clase == null)
            {
                Console.WriteLine("Clase no encontrada.");
                return;
            }

            Console.WriteLine($"Clase encontrada:");
            Console.WriteLine($"Maestro: {clase.Maestro.Nombre} {clase.Maestro.Apellido}");
            Console.WriteLine($"Materia: {clase.Materia.Nombre}");
            Console.WriteLine($"Curso: {clase.Curso.Nombre}");
            Console.WriteLine($"Horario: {clase.Horario}");
            Console.WriteLine($"Titular: {(clase.EsTitular == true ? "Sí" : "No")}");

            Console.Write("¿Está seguro de que desea eliminar esta clase? (S/N): ");
            var confirma = Console.ReadLine()?.Trim().ToUpper();

            if (confirma == "S")
            {
                context.Clases.Remove(clase);
                context.SaveChanges();
                Console.WriteLine("Clase eliminada exitosamente.");
            }
            else
            {
                Console.WriteLine("Eliminación cancelada.");
            }
        }
    }
}
