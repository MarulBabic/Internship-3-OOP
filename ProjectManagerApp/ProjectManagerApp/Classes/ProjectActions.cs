using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectManagerApp.Classes
{
    public class ProjectActions
    {
        public static void FilterProjectByStatus()
        {
            ProjectStatus status = EditProjectStatus();

            var filteredProjects = Program.projects.Where(p => p.Key.status == status).ToList();

            if (filteredProjects.Count == 0)
            {
                Console.WriteLine($"\nNema projekata sa statusom: {status}");
            }
            else
            {
                Console.WriteLine($"\nProjekti sa statusom {status}:\n");
                foreach (var project in filteredProjects)
                {
                    Console.WriteLine($"Projekt: {project.Key.projectName}, Status: {project.Key.status}, Opis: {project.Key.projectDescription}");
                }
            }

        }

        public static ProjectStatus EditProjectStatus()
        {
            Console.WriteLine("\nUnesite status: (aktivan,zavrsen,ceka)");
            var input = string.Empty;
            do
            {
                Console.Write("\nUnos: ");
                input = Console.ReadLine().Trim().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("\nUnos nesmije biti prazan,unesite status: (aktivan,zavrsen,ceka)");
                    continue;
                }

                if (input != "aktivan" && input != "zavrsen" && input != "ceka")
                {
                    Console.WriteLine("\nPogresan unos pokusajte ponovno: (aktivan,zavrsen,ceka)");
                    continue;
                }
                break;

            } while (true);

            ProjectStatus status;
            switch (input)
            {
                case "aktivan":
                    status = ProjectStatus.Active;
                    break;
                case "zavrsen":
                    status = ProjectStatus.Finished;
                    break;
                case "ceka":
                    status = ProjectStatus.Pending;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return status;

        }

        public static void GetTasksForNext7Days()
        {
            DateTime dateNow = DateTime.Now;
            bool hasTasks = false;

            Console.WriteLine("\nZadaci koji imaju rok završetka u sljedećih 7 dana:"); 
            foreach (var project in Program.projects)
            {
                foreach(var task in project.Value)
                {
                    if(task.deadline >= dateNow && task.deadline <= dateNow.AddDays(7))
                    {
                        hasTasks = true;
                        PrintTaskDetails(task);
                    }
                }
            }

            if (!hasTasks)
            {
                Console.WriteLine("Nema zadataka koji imaju rok završetka u sljedećih 7 dana.");
            }
        }

        public static void DeleteProject()
        {
            Console.WriteLine("\nUnesite naziv projekta kojeg zelite izbrisati\n");

            ManageTasks.PrintProjectsNames();

            Project projectToDelete = FindProject();

            var confirm = Confirm();

            if (!confirm)
            {
                Console.WriteLine("\nOdustajete od izbora, povratak na pocetni izbornik");
                return;
            }
            Program.projects.Remove(projectToDelete);
            Console.WriteLine($"\nProjekt \"{projectToDelete.projectName}\" uspješno obrisan.");
        }
        public static void AddNewProject()
        {
            var projectName = GetName();

            var description = GetDesc();

            var dateOfStart = GetDate(true);

            var dateOfEnd = GetDate(false);

            while (dateOfEnd < dateOfStart)
            {
                Console.WriteLine("\nDatum završetka ne može biti prije datuma početka. Unesite ponovo datum završetka.");
                dateOfEnd = GetDate(false);  
            }

            var project = new Project(projectName,description, dateOfStart, dateOfEnd);
            Program.projects[project] = new List<Task> { };

            Console.WriteLine("\nProjekt uspjesno kreiran");

        }

        public static DateTime GetDate(bool isStart)
        {
            DateTime inputDate;
            if (isStart)
            {
                Console.WriteLine("\nUnesite datum početka projekta (dd-MM-yyyy):");
            }
            else
            {
                Console.WriteLine("\nUnesite datum završetka projekta (dd-MM-yyyy):");
            }

            do
            {
                Console.Write("\nUnos: ");
                var input = Console.ReadLine();
                if (!DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out inputDate))
                {
                    Console.WriteLine("\nUnesite valjan datum u formatu (dd-MM-yyyy)");
                    continue;
                }
                if(inputDate < DateTime.Now.Date)
                {
                    Console.WriteLine("\nDatum nemoze biti raniji od trenutnog,odaberite drugi datum");
                    continue;
                }
                break;
            } while (true);
            return inputDate;
        }

        public static string GetDesc()
        {
            var desc = string.Empty;
            Console.WriteLine("\nUnesite opis:");
            do
            {
                Console.Write("\nUnos: ");
                desc = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(desc))
                {
                    Console.WriteLine("\nOpis nesmije biti prazan, unesite opis");
                    continue;
                }
                break;
            } while (true);

            return desc;
        }

        public static string GetName()
        {
            Console.WriteLine("\nUnesite naziv projekta:");
            var projectName = ManageTasks.NameCheck();
            do
            {
                if (Program.projects.Keys.Any(p => p.projectName.Equals(projectName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("\nProjekt s ovim nazivom već postoji. Molimo unesite drugi naziv:");
                    projectName = ManageTasks.NameCheck();
                    continue;
                }
                break;
            }
            while (true);

            return projectName;
        }

        public static Project FindProject()
        {
            Project project = null;
            do
            {
                string projectName = ManageTasks.NameCheck();
                project = Program.projects.Keys.FirstOrDefault(p => p.projectName.Equals(projectName, StringComparison.OrdinalIgnoreCase));

                if (project == null)
                {
                    Console.WriteLine("\nProjekt s tim nazivom nije pronađen. Molimo unesite točan naziv projekta:");
                    continue;
                }
                break;
            } while (true);

            return project;
        }

        public static bool Confirm()
        {
            Console.WriteLine("\nJeste li sigurni da zelite izbrisati zadatak ili projekt(da ; ne)");
            string choice = string.Empty;

            do
            {
                Console.Write("\nUnos: ");
                choice = Console.ReadLine().Trim().ToLower();
                if(choice != "da" && choice != "ne")
                {
                    Console.WriteLine("\nPogresan unos unesite da ili ne");
                    continue;
                }
                break;
            } while (true);

            return choice == "da" ? true : false;
        }

        public static void PrintAllProjectsWithTasks()
        {
            if (Program.projects.Count == 0)
            {
                Console.WriteLine("Nema dostupnih projekata.");
                return;
            }

            foreach (var project in Program.projects)
            {
                PrintProjectDetail(project.Key);

                PrintTasksForProject(project.Key);
            }
        }

        public static void PrintTasksForProject(Project project)
        {
            if (Program.projects[project].Count == 0)
            {
                Console.WriteLine("   Ovaj projekt nema zadataka.");
            }
            else
            {
                Console.WriteLine("\n   Zadaci unutar projekta:");
                foreach (var task in Program.projects[project])
                {
                    PrintTaskDetails(task);
                }
            }
        }

        public static void PrintTaskDetails(Task task)
        {
            Console.WriteLine("   ----------------------------------");
            Console.WriteLine($"   Naziv zadatka       : {task.taskName}");
            Console.WriteLine($"   Opis zadatka        : {task.taskDescription}");
            Console.WriteLine($"   Rok za izvrsenje    : {task.deadline.ToShortDateString()}");
            Console.WriteLine($"   Status zadatka      : {task.status}");
            Console.WriteLine($"   Ocekivano trajanje  : {task.expectedDurationMinutes} minuta");
            Console.WriteLine("   ----------------------------------");
        }

        public static void PrintProjectDetail(Project project)
        {
            Console.WriteLine("\n------------------------------------------");
            Console.WriteLine($"Naziv projekta      : {project.projectName}");
            Console.WriteLine($"Opis projekta       : {project.projectDescription}");
            Console.WriteLine($"Datum pocetka       : {project.startDate.ToShortDateString()}");
            Console.WriteLine($"Datum zavrsetka     : {project.finishDate.ToShortDateString()}");
            Console.WriteLine($"Status projekta     : {project.status}");
            Console.WriteLine("------------------------------------------");
        }
    }
}
