using System.Xml.Serialization;
using Sistema_de_Gestion_de_asistencias.Domain;
using Sistema_de_Gestion_de_asistencias.Helpers;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gestion_de_asistencias.Persistence;

namespace Sistema_de_Gestion_de_asistencias
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=localhost,1433;Database=SistemaAsistencia;User Id=sa;Password=Jw#ilovepizza9;TrustServerCertificate=True;")
                .Options;

            using (var context = new DataContext(options))
            {
                DataSeeder.Seed(context);
            }

            Console.WriteLine("Datos de prueba insertados.");
            MainMenu();
        }

        public static void MainMenu()
        {
            //var context = new Persistence.DataContext();
            Console.WriteLine("¡Bienvenido al Sistema de Asistencia!\n");

            bool running = true;

            while (running)
            {
                Console.WriteLine("¿Qué necesita realizar?\n");

                Console.Write("---------------MENU PRINCIPAL--------------- \n");
                Console.Write("1. Gestionar estudiantes\n");
                Console.Write("2. Gestionar maestros\n");
                Console.Write("3. Gestionar cursos\n");
                Console.Write("4. Gestionar asistencias\n");
                Console.Write("5. Gestionar materias\n");
                Console.Write("6. Gestionar clases\n");
                Console.Write("7. Mostrar reporte de Asistencia semanal\n");
                Console.Write("8. Salir\n");
                Console.Write("Seleccioné una opción: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        StudentHelper.ShowSubmenu();
                        break;
                    case 2:
                        ProfessorHelper.ShowSubmenu();
                        break;
                    case 3:
                        CourseHelper.ShowSubmenu();
                        break;
                    case 4:
                        AssistanceHelper.ShowSubmenu();
                        break;
                    case 5:
                        SubjectHelper.ShowSubmenu();
                        break;
                    case 6:
                        ClassHelper.ShowSubmenu();
                        break;
                    case 7:
                        Report.ShowSubmenu();
                        break;
                    case 8:
                        running = false;
                        Console.WriteLine("¡Gracias por usar el Sistema de Asistencia Escolar!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }

        public static void Pausar()
        {
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}

           


    





