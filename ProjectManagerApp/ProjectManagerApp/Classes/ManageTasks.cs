using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class ManageTasks
    {

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("\n1 - Prikaz detalja odabranog zadatka\n2 - Uredivanje statusa zadatka" +
                "\n3 - Povratak na pocetni izbornik");

            var choice = 0;
            do
            {
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out choice );

                switch (choice)
                {
                    case 1:
                        ShowTaskDetails();
                        return;
                    case 2:
                        EditTaskStatus();
                        return;
                    case 3:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("\nPogresan odabir, pokusajte ponovno");
                        break;
                }
            } while (true);
        }

        private static void EditTaskStatus()
        {
            Console.WriteLine("\nUnesite naziv projekta\n");

            PrintProjectsNames();

            Project project = ManageSingleProject.CheckProjectStatus();

            if (Program.projects[project].Count == 0)
            {
                Console.WriteLine("\nIzabrani projekt nema zadataka");
                return;
            }

            Console.WriteLine("\nUnesite naziv zadatka");

            PrintTasksNames(project);

            var taskName = NameCheck();
            var task = CheckTaskStatus(project,taskName);

            TaskStatus status;

            do {
                status = GetTaskStatus(task);
                if (task.status == status)
                {
                    Console.WriteLine("\nZadatak vec ima taj status,pokusajte sa drugim");
                    continue;
                }
                break;
            } while (true);

            task.status = status;
            Console.WriteLine($"\nStatus zadatka: {task.taskName} promijenjen u {task.status}");

            CheckFinalProjectStatus(project);

        }

        public static void CheckFinalProjectStatus(Project project)
        {
            bool allTasksFinished = Program.projects[project].All(task => task.status == TaskStatus.Finished);

            if (allTasksFinished)
            {
                project.status = ProjectStatus.Finished;
                Console.WriteLine($"\nProjekt {project.projectName} je sada označen kao završen.");
            }
        }

        private static TaskStatus GetTaskStatus(Task task)
        {
            Console.WriteLine("\nUnesite status: (aktivan,zavrsen,odgoden)");
            var input = string.Empty;
            do
            {
                Console.Write("\nUnos: ");
                input = Console.ReadLine().Trim().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("\nUnos nesmije biti prazan,unesite status: (aktivan,zavrsen,odgoden)");
                    continue;
                }

                if (input != "aktivan" && input != "zavrsen" && input != "odgoden")
                {
                    Console.WriteLine("\nPogresan unos pokusajte ponovno: (aktivan,zavrsen,odgoden)");
                    continue;
                }
                break;

            } while (true);

            TaskStatus status;
            switch (input)
            {
                case "aktivan":
                    status = TaskStatus.Active;
                    break;
                case "zavrsen":
                    status = TaskStatus.Finished;
                    break;
                case "ceka":
                    status = TaskStatus.Delayed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return status;
        }

        private static Task CheckTaskStatus(Project project,string taskName)
        {
            Console.WriteLine("\nUnesite naziv zadatka\n");

            PrintTasksNames(project);

            Task task;

            do
            {
                task = FindTask(project, taskName);

                if (task.status == TaskStatus.Finished)
                {
                    Console.WriteLine("\nNemoguće mijenjati status zadatka koji je već završen. Odaberite drugi zadatak.");
                    taskName = NameCheck();

                    continue;
                }

                break;
            } while (true);

            return task;
        }

        private static void ShowTaskDetails()
        {
            Console.WriteLine("\nUnesite naziv projekta");

            PrintProjectsNames();

            Project project = ProjectActions.FindProject();

            if (Program.projects[project].Count == 0)
            {
                Console.WriteLine("\nIzabrani projekt nema zadataka");
                return;
            }

            Console.WriteLine("\nUnesite naziv zadatka\n");

            PrintTasksNames(project);


            var taskName = NameCheck();

            var task = FindTask(project, taskName);

            ProjectActions.PrintTaskDetails(task);
        }

        public static void PrintProjectsNames()
        {
            foreach (var project in Program.projects)
            {
                Console.WriteLine($"{project.Key.projectName}");
            }
        }

        public static void PrintTasksNames(Project project)
        {
            foreach (var task in Program.projects[project])
            {
                Console.WriteLine($"{task.taskName}");
            }
        }

        public static void GetExpectedDuration(Project project)
        {
            var totalDuration = 0.0;
            foreach (var task in Program.projects[project])
            {
                if (task.status == TaskStatus.Active)
                {
                    totalDuration += task.expectedDurationMinutes;
                }
            }

            if (totalDuration > 0.0) {
                Console.WriteLine($"\nUkupno ocekivano vrijeme za sve aktivne zadatke unutar projekta: {project.projectName}, je: {totalDuration}");
            }
            else
            {
                Console.WriteLine("\nNema aktivnih zadataka unutar projekta");
            }

        }

        public static void DeleteTask(Project project)
        {
            Console.WriteLine("\nUnesite naziv zadatka kojeg zelite izbrisati");

            ProjectActions.PrintTasksForProject(project);

            var taskName = NameCheck();
           
            var task = FindTask(project,taskName);

            bool confirm = ProjectActions.Confirm();

            if (!confirm)
            {
                Console.WriteLine("\nOdustajete od izbora, povratak na pocetni izbornik");
                return;
            }

            Program.projects[project].Remove(task);

            Console.WriteLine($"\n----Zadatak uspjesno izbrisan.----");
        }


        public static Task FindTask(Project project,string taskName)
        {
            Task task;
            do
            {
                task = Program.projects[project].FirstOrDefault(u => u.taskName == taskName);
                if (task == null)
                {
                    Console.WriteLine("\nNema Zadatka s tim imenom, pokusajte sa drugim");
                    taskName = NameCheck();
                    continue;
                }

                break;

            } while (true);

            return task;
        }

        public static string NameCheck()
        {
            var name = string.Empty;
            do
            {
                Console.Write("\nUnos: ");
                name = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("\nNaziv ne može biti prazan. Molimo unesite naziv:");
                    continue;
                }
                break;
            } while (true);

            return name;
        }

        public static void AddTask(Project project)
        {
            var taskName = GetName(project);

            var description = ProjectActions.GetDesc();

            var deadline = GetDeadline();

            var expectedDuration = GetExpectedMinutes();

            var newTask = new Task(taskName, description, deadline, TaskStatus.Active, expectedDuration, project.projectName, project.GetId());
            Program.projects[project].Add(newTask);

            Console.WriteLine($"\nZadatak: {newTask.taskName} uspjesno dodan u projekt: {project.projectName}");

        }

        private static int GetExpectedMinutes()
        {
            Console.WriteLine("\nUnesite očekivano trajanje zadatka u minutama:");
            int expectedDurationMinutes;
            do
            {
                Console.Write("\nUnos: ");
                var durationInput = Console.ReadLine().Trim();

                if (int.TryParse(durationInput, out expectedDurationMinutes) && expectedDurationMinutes > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Molimo unesite ispravan broj minuta (pozitivan cijeli broj):");
                }
            } while (true);

            return expectedDurationMinutes;
        }

        private static DateTime GetDeadline()
        {
            Console.WriteLine("\nUnesite rok za izvršenje (dd-MM-yyyy):");
            DateTime inputDate;
            do
            {
                Console.Write("\nUnos: ");
                var input = Console.ReadLine();
                if (!DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out inputDate))
                {
                    Console.WriteLine("Unesite valjan datum u formatu (dd-MM-yyyy)");
                    continue;
                }
                if (inputDate < DateTime.Now.Date)
                {
                    Console.WriteLine("Datum nemoze biti raniji od trenutnog,odaberite drugi datum");
                    continue;
                }
                break;
            } while (true);
            return inputDate;
        }

        private static string GetName(Project project)
        {
            Console.WriteLine("\nUnesite naziv zadatka:");
            var taskName = NameCheck();
            do
            {
                if (Program.projects[project].Any(t => t.taskName.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Zadatak s tim nazivom već postoji u projektu. Molimo unesite drugi naziv:");
                    taskName = NameCheck();
                    continue;
                }

                break;
            } while (true);

            return taskName;
        }
    }
}
