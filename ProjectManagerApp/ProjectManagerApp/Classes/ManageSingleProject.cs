using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class ManageSingleProject
    {
        public static void Menu()
        {
            Console.WriteLine("\n1 - Ispis svih zadataka unutar odabranog projekta\n" +
                  "2 - Prikaz detalja odabranog projekta\n" +
                  "3 - Uređivanje statusa projekta\n" +
                  "4 - Dodavanje zadatka unutar projekta\n" +
                  "5 - Brisanje zadatka iz projekta\n" +
                  "6 - Prikaz ukupno očekivanog vremena potrebnog za sve aktivne zadatke u projektu\n");


            var option = 0;
            Console.Write("\nUnos: ");
            int.TryParse(Console.ReadLine(), out option);

            switch (option) {
                case 1:
                    PrintAllTasksForProject();
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
                case 5:
                    
                    break;
                case 6:
                    
                    break;
                default:
                    Console.WriteLine("\nPogresan unos, pokusajte ponovo");
                    break;
            }
        }

        public static void PrintAllTasksForProject()
        {
            Console.WriteLine("\nUnesite naziv projekta za prikaz zadataka:\n");

            foreach(var project in Program.projects)
            {
                Console.WriteLine($"{project.Key.projectName}");
            }

            string projectName = string.Empty;
            Project selectedProject = null;

            do
            {
                Console.Write("\nUnos: ");
                projectName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(projectName))
                {
                    Console.WriteLine("Naziv projekta ne može biti prazan. Molimo unesite naziv projekta:");
                    continue;
                }

                selectedProject = Program.projects.Keys.FirstOrDefault(p => p.projectName.Equals(projectName, StringComparison.OrdinalIgnoreCase));

                if (selectedProject == null)
                {
                    Console.WriteLine("Projekt s tim nazivom nije pronađen. Molimo unesite točan naziv projekta:");
                    continue;
                }

                break;

            } while (true);

            var tasks = Program.projects[selectedProject];

            if (tasks.Count == 0)
            {
                Console.WriteLine("Ovaj projekt nema zadataka.");
            }
            else
            {
                ProjectActions.PrintTasksForProject(selectedProject);
            }
        }
    }
}
