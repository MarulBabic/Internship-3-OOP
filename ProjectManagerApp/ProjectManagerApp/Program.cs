using ProjectManagerApp.Classes;
using Task = ProjectManagerApp.Classes.Task;

namespace ProjectManagerApp
{
    internal class Program
    {
        public static Dictionary<Project,List<Task>> projects = new Dictionary<Project,List<Task>>();
        static void Main(string[] args)
        {
            var project1 = new Project("Prvi projekt", "izrada 1.projekta",DateTime.Now.AddDays(-30),DateTime.Now.AddDays(30),ProjectStatus.Active);
            var project2 = new Project("Drugi projekt", "izrada 2.projekta",DateTime.Now.AddDays(-60),DateTime.Now.AddDays(10),ProjectStatus.Active);
            var project3 = new Project("Treci projekt", "izrada 3.projekta",DateTime.Now.AddDays(-20),DateTime.Now.AddDays(20),ProjectStatus.Pending);

            var task1 = new Task("zadatak 1 projekta 1", "izrada 1.zadatka", DateTime.Now.AddDays(8), TaskStatus.Active, 550, project1.projectName, project1.GetId());
            var task2 = new Task("zadatak 2 projekta 1", "izrada 2.zadatka", DateTime.Now.AddDays(10), TaskStatus.Active, 600, project1.projectName, project1.GetId());

            var task3 = new Task("zadatak 1 projekta 2", "izrada 1.zadatka", DateTime.Now.AddDays(1), TaskStatus.Delayed, 800, project2.projectName, project2.GetId());
            var task4 = new Task("zadatak 2 projekta 2", "izrada 2.zadatka", DateTime.Now.AddDays(2), TaskStatus.Finished, 600, project2.projectName, project2.GetId());

            var task5 = new Task("zadatak 1 projekta 3", "izrada 1.zadatka", DateTime.Now.AddDays(14), TaskStatus.Active, 600, project3.projectName, project3.GetId());
            var task6 = new Task("zadatak 2 projekta 3", "izrada 2.zadatka", DateTime.Now.AddDays(12), TaskStatus.Finished, 600, project3.projectName, project3.GetId());
            var task7 = new Task("zadatak 3 projekta 3", "izrada 3.zadatka", DateTime.Now.AddDays(2), TaskStatus.Finished, 600, project3.projectName, project3.GetId());


            projects[project1] = new List<Task> { task1, task2 };
            projects[project2] = new List<Task> { task3, task4 };
            projects[project3] = new List<Task> { task5,task6,task7 };

            Menu.MainMenu();
        }
    }
}
