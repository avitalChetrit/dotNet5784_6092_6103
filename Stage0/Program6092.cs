using System;
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6092();
            Welcome6103();
            Console.ReadKey();
        }

        static partial void Welcome6103();
        private static void Welcome6092()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            //Console.WriteLine("{0}, welcome to my first console aplication",userName);
            Console.WriteLine(userName + ", welcome to my first console aplication ");
        }
        
    }
}

//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        Console.WriteLine("Hello, World!");
//    }
//}
