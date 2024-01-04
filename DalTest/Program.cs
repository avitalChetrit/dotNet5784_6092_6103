using Dal;
using DalApi;
using System.Linq.Expressions;

namespace DalTest
{
    internal class Program
    {
        private static IChef? s_dalChef = new ChefImplementation(); //stage 1
        //private static ICourse? s_dalCourse = new CourseImlementation(); //stage 1
        //private static ILink? s_dalLinks = new LinkImplementation(); //stage 1
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalChef/*, s_dalCourse, s_dalLinks*/);
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
            int choice=1;
            int subChoice;

            while(choice!=0)
            {
                Console.WriteLine("בחר ישות שברצונך לבדוק:");
                Console.WriteLine("0. Exit\n" +"1. Chef\n" +"2.Task\n" +"3.Link\n");
                choice=Console.Read();
                if(choice!=0)
                {
                    Console.WriteLine("בחר את המתודה לבצע:");
                    Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n" + "6.Delete\n");
                    subChoice = Console.Read();
                }

                switch (choice)
                {

                    case 1:
                        break;

                    case 2: Console.WriteLine();
                    break;
                }


            }

        }
    }
}
