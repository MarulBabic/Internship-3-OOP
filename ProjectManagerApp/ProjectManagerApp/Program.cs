﻿using ProjectManagerApp.Classes;
using Task = ProjectManagerApp.Classes.Task;

namespace ProjectManagerApp
{
    internal class Program
    {
        public static Dictionary<Project,List<Task>> projects = new Dictionary<Project,List<Task>>();
        static void Main(string[] args)
        {
            var project1 = new Project("Projekt 1", "izrada 1.projekta",DateTime.Now.AddDays(-30),DateTime.Now.AddDays(30),ProjectStatus.Active);
            var project2 = new Project("Projekt 2", "izrada 2.projekta",DateTime.Now.AddDays(-60),DateTime.Now.AddDays(10),ProjectStatus.Active);
            var project3 = new Project("Projekt 3", "izrada 3.projekta",DateTime.Now.AddDays(-20),DateTime.Now.AddDays(20),ProjectStatus.Active);

            var task1 = new Task("zadatak 1", "zadatak 1 projekt 1", DateTime.Now.AddDays(8), TaskStatus.Active, 550, project1.projectName, project1.GetId(),Priority.Low);
            var task2 = new Task("zadatak 2", "zadatak 2 projekt 1", DateTime.Now.AddDays(10), TaskStatus.Active, 650, project1.projectName, project1.GetId());
            var task3 = new Task("zadatak 3", "zadatak 3 projekt 1", DateTime.Now.AddDays(12), TaskStatus.Active, 400, project1.projectName, project1.GetId(),Priority.High);

            var task4 = new Task("zadatak 4", "zadatak 4 projekt 2", DateTime.Now.AddDays(1), TaskStatus.Delayed, 800, project2.projectName, project2.GetId());
            var task5 = new Task("zadatak 5", "zadatak 5 projekt 2", DateTime.Now.AddDays(2), TaskStatus.Finished, 600, project2.projectName, project2.GetId());

            var task6 = new Task("zadatak 6", "zadatak 6 projekt 3", DateTime.Now.AddDays(6), TaskStatus.Active, 700, project3.projectName, project3.GetId());
            var task7 = new Task("zadatak 7", "zadatak 7 projekt 3", DateTime.Now.AddDays(12), TaskStatus.Finished, 300, project3.projectName, project3.GetId(),Priority.High);
            var task8 = new Task("zadatak 8", "zadatak 8 projekt 3", DateTime.Now.AddDays(2), TaskStatus.Finished, 640, project3.projectName, project3.GetId(),Priority.Low);


            projects[project1] = new List<Task> { task1, task2,task3 };
            projects[project2] = new List<Task> { task4, task5 };
            projects[project3] = new List<Task> { task6,task7,task8 };

            Menu.MainMenu();
        }
    }
}
