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
                Console.Write("4. Registrar asistencia\n");
                Console.Write("5. Mostrar reporte de Asistencia semanal\n");
                Console.Write("6. Salir\n");
                Console.Write("Elige una opción: ");
                int choice = Convert.ToInt32(Console.ReadLine());


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

                        break;
                    case 5:

                        break;
                    case 6:
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

