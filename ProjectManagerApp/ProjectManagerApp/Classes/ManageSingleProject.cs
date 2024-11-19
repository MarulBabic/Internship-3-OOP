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
                    ViewProjectDetails();
                    break;
                case 3:
                    EditProjectStatus();
                    break;
                case 4:
                    AddTaskForProject();
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

        public static void AddTaskForProject()
        {
            Console.WriteLine("Unesite naziv projekta u koji zelite dodati zadatak");
            foreach (var projectt in Program.projects)
            {
                Console.WriteLine($"{projectt.Key.projectName}");
            }

            Project project = ProjectActions.FindProject();

            ManageTasks.AddTask(project);

        }

        public static void EditProjectStatus()
        {
            Console.WriteLine("Unesite naziv projekta kojem zelite urediti status");
            foreach (var projectt in Program.projects)
            {
                Console.WriteLine($"{projectt.Key.projectName}");
            }

            Project project = ProjectActions.FindProject();
            ProjectStatus projectStatus;
            do
            {
                projectStatus = ProjectActions.EditProjectStatus();
                if (project.status == projectStatus)
                {
                    Console.WriteLine("Projekt vec ima taj status,pokusajte sa drugim");
                    continue;
                }
                break;
            } while (true);
            project.status = projectStatus;

            Console.WriteLine($"Status projekta: {project.projectName} promijenjen u {project.status}");
        }

        public static void ViewProjectDetails()
        {
            Console.WriteLine("\nUnesite naziv projekta kojeg zelite vidjeti\n");

            foreach (var projectt in Program.projects)
            {
                Console.WriteLine($"{projectt.Key.projectName}");
            }

            Project project = null;
            do
            {
                string projectName = ProjectNameCheck();
                project = Program.projects.Keys.FirstOrDefault(p => p.projectName.Equals(projectName, StringComparison.OrdinalIgnoreCase));

                if (project == null)
                {
                    Console.WriteLine("Projekt s tim nazivom nije pronađen. Molimo unesite točan naziv projekta:");
                    continue;
                }
                break;
            } while (true);

            ProjectActions.PrintProjectDetail(project);
        }

        public static void PrintAllTasksForProject()
        {
            Console.WriteLine("\nUnesite naziv projekta za prikaz zadataka:\n");

            foreach (var project in Program.projects)
            {
                Console.WriteLine($"{project.Key.projectName}");
            }

            Project selectedProject = ProjectActions.FindProject();

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

        public static string ProjectNameCheck()
        {
            var projectName = string.Empty;
            do
            {
                Console.Write("\nUnos: ");
                projectName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(projectName))
                {
                    Console.WriteLine("Naziv projekta ne može biti prazan. Molimo unesite naziv projekta:");
                    continue;
                }
                break;
            }while(true);

            return projectName;
        }
    }  
}
