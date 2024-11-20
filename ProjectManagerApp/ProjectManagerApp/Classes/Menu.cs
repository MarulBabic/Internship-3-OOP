using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerApp.Classes
{
    public class Menu
    {
       public static void MainMenu()
        {
            do
            {
                Console.WriteLine("\n1 - Ispis svih projekata sa zadacima\n2 - Dodavanje novog projekta\n3 - Brisanje projekta" + 
                    "\n4 - Prikaz svih zadataka s rokom u sljedecih 7 dana\n5 - Prikaz projekata filtriranih po statusu(akitvni,zavrseni,na cekanju)"+
                    "\n6 - Upravljanje pojedinim projektom\n7 - Upravljanje pojedinim zadatkom\n8 - Sortiranje zadataka");

                var option = 0;
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option );

                switch (option)
                {
                    case 1:
                        ProjectActions.PrintAllProjectsWithTasks();
                        break;
                    case 2:
                        Console.Clear();
                        ProjectActions.AddNewProject();
                        break;
                    case 3:
                        Console.Clear();
                        ProjectActions.DeleteProject();
                        break;
                    case 4:
                        Console.Clear();
                        ProjectActions.GetTasksForNext7Days();
                        break;
                    case 5:
                        Console.Clear();
                        ProjectActions.FilterProjectByStatus();
                        break;
                    case 6:
                        ManageSingleProject.Menu();
                        break;
                    case 7:
                        ManageTasks.Menu();
                        break;
                    case 8:
                        BonusTasks.BonusMenu();
                        break;
                    default:
                        Console.WriteLine("\nPogresan unos, pokusajte ponovo");
                        break;
                }


            } while (true);
        }
    }
}
