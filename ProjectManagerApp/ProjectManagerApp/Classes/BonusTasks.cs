using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class BonusTasks
    {
        public static void BonusMenu()
        {
            Console.Clear();
            Console.WriteLine("\n1 - Sortiranje zadataka od najkraceg do najduzeg\n2 - Sortiranje zadataka po prioritetu" + 
                "\n3 - Povratak na pocetni izbornik");

            var option = 0;
            do
            {
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option );

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        SortTaskByDuration();
                        return;
                    case 2:
                        Console.Clear();
                        SortTaskByPriority();
                        return;
                    case 3:
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Pogresan unos, pokusajte ponovo");
                        break;
                }

            } while (true);
        }
        private static void SortTaskByDuration()
        {
            Console.WriteLine("\nPopis projekata i zadataka sortiranih po trajanju:\n");

            foreach (var project in Program.projects)
            {
                Console.WriteLine($"\nProjekt: {project.Key.projectName}");

                var sortedTasks = project.Value.OrderBy(task => task.expectedDurationMinutes).ToList();

                foreach (var task in sortedTasks)
                {
                    Console.WriteLine($"- Zadatak: {task.taskName}, Trajanje: {task.expectedDurationMinutes} minuta");
                }
            }
        }

        private static void SortTaskByPriority()
        {
            Console.WriteLine("\nPopis projekata i zadataka sortiranih po prioritetu:\n");

            foreach (var project in Program.projects)
            {
                Console.WriteLine($"\nProjekt: {project.Key.projectName}");

                var sortedTasks = project.Value.OrderBy(task => task.priority).ToList();

                foreach (var task in sortedTasks)
                {
                    Console.WriteLine($"- Zadatak: {task.taskName}, Prioritet: {task.priority}");
                }
            }

        }
    }
}
