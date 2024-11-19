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
                    DeleteTaskFromProject();
                    break;
                case 6:
                    CalculateTotalExpectedTimeForActiveTasks();
                    break;
                default:
                    Console.WriteLine("\nPogresan unos, pokusajte ponovo");
                    break;
            }
        }

        public static void CalculateTotalExpectedTimeForActiveTasks()
        {
            Console.WriteLine("\nUnesite naziv projekta da bi prikazali ukupno ocekivano vrijeme za sve aktivne zadatke u projektu\n");

            ManageTasks.PrintProjectsNames();

            Project project = ProjectActions.FindProject();

            ManageTasks.GetExpectedDuration(project);
        }

        public static void DeleteTaskFromProject()
        {
            Console.WriteLine("\nUnesite naziv projekta iz kojeg zelite izbrisati zadatak\n");
            
            ManageTasks.PrintProjectsNames();

            Project project = CheckProjectStatus();

            ManageTasks.DeleteTask(project);

        }

        public static void AddTaskForProject()
        {
            Console.WriteLine("\nUnesite naziv projekta u koji zelite dodati zadatak\n");

            ManageTasks.PrintProjectsNames();

            Project project = CheckProjectStatus();
            
            ManageTasks.AddTask(project);
        }

        public static Project CheckProjectStatus()
        {
            Project project;

            do
            {
                project = ProjectActions.FindProject();

                if (project.status == ProjectStatus.Finished)
                {
                    Console.WriteLine($"\nProjekt: {project.projectName} ima status: zavrsen, pokusajte ponovo");
                    continue;
                }

                break;
            } while (true);

            return project;
        }

        public static void EditProjectStatus()
        {
            Console.WriteLine("\nUnesite naziv projekta kojem zelite urediti status\n");
            
            ManageTasks.PrintProjectsNames();

            Project project = CheckProjectStatus();

            ProjectStatus projectStatus;
            do
            {
                projectStatus = ProjectActions.EditProjectStatus();
                if (project.status == projectStatus)
                {
                    Console.WriteLine("\nProjekt vec ima taj status,pokusajte sa drugim");
                    continue;
                }
                break;
            } while (true);
            project.status = projectStatus;

            Console.WriteLine($"\nStatus projekta: {project.projectName} promijenjen u {project.status}");
        }

        public static void ViewProjectDetails()
        {
            Console.WriteLine("\nUnesite naziv projekta kojeg zelite vidjeti\n");

            ManageTasks.PrintProjectsNames();

            Project project = ProjectActions.FindProject();

            ProjectActions.PrintProjectDetail(project);
        }

        public static void PrintAllTasksForProject()
        {
            Console.WriteLine("\nUnesite naziv projekta za prikaz zadataka:\n");

            ManageTasks.PrintProjectsNames();

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
    }  
}
