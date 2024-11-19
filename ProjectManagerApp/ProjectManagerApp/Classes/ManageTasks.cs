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
        public static void AddTask(Project project)
        {
            var taskName = GetName(project);

            var description = ProjectActions.GetDesc();

            var deadline = GetDeadline();

            var expectedDuration = GetExpectedMinutes();

            var newTask = new Task(taskName, description, deadline, TaskStatus.Active, expectedDuration, project.projectName, project.GetId());
            Program.projects[project].Add(newTask);

            Console.WriteLine($"Zadatak: {newTask.taskName} uspjesno dodan u projekt: {project.projectName}");

        }

        private static int GetExpectedMinutes()
        {
            Console.WriteLine("\nUnesite očekivano trajanje zadatka u minutama:");
            int expectedDurationMinutes;
            do
            {
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
            string taskName;
            do
            {
                taskName = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(taskName))
                {
                    Console.WriteLine("Naziv zadatka ne može biti prazan. Molimo unesite naziv:");
                    continue;
                }

                if (Program.projects[project].Any(t => t.taskName.Equals(taskName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Zadatak s tim nazivom već postoji u projektu. Molimo unesite drugi naziv:");
                    continue;
                }

                break;
            } while (true);

            return taskName;
        }
    }
}
