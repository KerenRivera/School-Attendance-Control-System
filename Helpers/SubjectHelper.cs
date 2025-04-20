using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Domain;
using static Sistema_de_Gestion_de_asistencias.Domain.Materia;
using Sistema_de_Gestion_de_asistencias.Persistence;

namespace Sistema_de_Gestion_de_asistencias.Helpers
{
    public class SubjectHelper
    {
        public static void AddSubject()
        {
            Console.WriteLine("Nombre de la nueva materia: ");
            string? input = Console.ReadLine();
            string nombre = input?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            using var context = new DataContext();
            if (context.Materias.Any(m => m.Nombre.ToLower() == nombre.ToLower()))
            {
                Console.WriteLine("Ya existe una materia con ese nombre.");
                return;
            }
            
            var nuevaMateria = new Materia { Nombre = nombre };
            context.Materias.Add(nuevaMateria);
            context.SaveChanges();
            Console.WriteLine($"Materia '{nombre}' agregada exitosamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            
        }
    }
}
