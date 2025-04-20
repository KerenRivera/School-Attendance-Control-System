using System.Xml.Serialization;
using Sistema_de_Gestion_de_asistencias.Helpers;

namespace Sistema_de_Gestion_de_asistencias
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
                Console.Write("5. Gestionar clases\n");
                Console.Write("6. Mostrar reporte de Asistencia semanal\n");
                Console.Write("7. Salir\n");
                Console.Write("Elige una opción: ");

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
                        Assignment_ProfessorHelper.ShowSubmenu();
                        break;
                    case 6:
                        Report.ShowSubmenu();
                        break;
                    case 7:
                        running = false;
                        Console.WriteLine("¡Gracias por usar el Sistema de Asistencia Escolar!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }

        }
      
    }

}

