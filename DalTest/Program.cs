using Dal;
using DalApi;
using DO;
using System.Linq.Expressions;

namespace DalTest
{
    internal class Program
    {
        static void switchFunChef()
        {
            //sub menu for chef
            Console.WriteLine("בחר את המתודה לבצע:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n" );
            int choice = Console.Read();

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                    Console.WriteLine("Enter id, Level(Beginner/Advanced/Expert), Name and cost:");
                    int ChefId = Console.Read();                 //unique  
                    ChefExperience Level = (ChefExperience)Console.Read();
                    string? Name = Console.ReadLine();
                    double? Cost = Console.Read();
                    Chef c = new Chef(ChefId, Level, Name, Cost);
                    s_dalChef!.Create(c);

                    break;
                case 3: //Read
                    Console.WriteLine("Enter id:");
                    int id = Console.Read();
                    Chef? chef = s_dalChef.Read(id);
                    if (chef == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        Console.WriteLine(chef.ChefId + chef.Level + chef.Name + chef.Cost);

                    break;
                case 4: //ReadAll
                    List<Chef> lCh = s_dalChef!.ReadAll();
                    foreach (var _chef in lCh)
                    {
                        Console.WriteLine(_chef.ChefId + _chef.Level + _chef.Name + _chef.Cost);
                    }

                    break;
                case 5: //Update
                    //print the object to update(and then update it)
                    Console.WriteLine("Enter id");
                    int ChefId1 = Console.Read();
                    Chef? chef1 = s_dalChef.Read(ChefId1);
                    if (chef1 == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        Console.WriteLine(chef1.ChefId + chef1.Level + chef1.Name + chef1.Cost);

                    // the update
                    Console.WriteLine("Enter Level(Beginner/Advanced/Expert), Name and cost:");
                    ChefExperience? Level1 = (ChefExperience)Console.Read();
                    string? Name1 = Console.ReadLine();
                    double? Cost1 = Console.Read();
                    if (Level1 == null || Name1 == null || Cost1 == null)
                        break;
                    Chef ch = new Chef(ChefId1, Level1, Name1, Cost1);
                    s_dalChef!.Update(ch);

                    break;

                //case 6: //Delete
                //    Console.WriteLine("Enter id:");
                //    int idDel = Console.Read();
                //    s_dalChef!.Delete(idDel);

                //    break;
                default:
                    break;
            }
        }
        private static IChef? s_dalChef = new ChefImplementation(); //stage 1
        //private static ICourse? s_dalCourse = new CourseImlementation(); //stage 1
        private static IDependency? s_dalDependencys = new DependencyImplementation(); //stage 1
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalChef, /*s_dalCourse,*/ s_dalDependencys);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            int choice = 1;
            int subChoice;

            while (choice != 0)
            {
                Console.WriteLine("בחר ישות שברצונך לבדוק:");
                Console.WriteLine("0. Exit\n" + "1. Chef\n" + "2.Task\n" + "3.Link\n");
                choice = Console.Read();
                //if(choice!=0)
                //{
                //    Console.WriteLine("בחר את המתודה לבצע:");
                //    Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n" + "6.Delete\n");
                //    subChoice = Console.Read();
                //}
                try
                {
                    switch (choice)//main menu
                    {
                        case 1://sub-menu chef

                            break;

                        case 2://sub-menu tasks

                            break;

                        case 3://sub-menu link

                            break;
                        case 0://exit
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }



            }

        }
    }
}
