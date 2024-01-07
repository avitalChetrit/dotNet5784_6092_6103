using Dal;
using DalApi;
using DO;
//using System.Linq.Expressions;
//using System;
using Task = DO.Task;

namespace DalTest
{
    internal class Program
    {
        public static Task inputAndCreateTask()
        {
            Console.WriteLine("Enter alias,Description,IsMilestone,Complexity,CreatedAtDate," +
                        "RequiredTime,StartDate,ScheduledDate,DeadLineDate,CompleteDate,Deliveables,Remarks,ChefId :");

            //INPUT
            int Id = 0;
            string? Alias = Console.ReadLine();
            string? Description = Console.ReadLine();
            bool IsMilestone = false;
            ChefExperience? Complexity = (ChefExperience)Console.Read();
            DateTime? CreatedAtDate = DateTime.Parse(Console.ReadLine());
            TimeSpan? RequiredTime = TimeSpan.Parse(Console.ReadLine());
            DateTime? StartDate = DateTime.Parse(Console.ReadLine());
            DateTime? ScheduledDate = DateTime.Parse(Console.ReadLine());
            DateTime? DeadLineDate = DateTime.Parse(Console.ReadLine());
            DateTime? CompleteDate = DateTime.Parse(Console.ReadLine());
            string? Deliveables = Console.ReadLine();
            string? Remarks = Console.ReadLine();
            int? ChefId = Console.Read();
            ///

            Task tCreate = new Task(Id, Alias, Description, IsMilestone, Complexity, CreatedAtDate, RequiredTime,
                StartDate, ScheduledDate, DeadLineDate, CompleteDate, Deliveables, Remarks, ChefId);
            return tCreate;
        }
        public static void printTask(Task task)
        {
            Console.WriteLine(task.Id);
            Console.WriteLine(task.Alias);
            Console.WriteLine(task.Description);
            Console.WriteLine(task.IsMilestone);
            Console.WriteLine(task.Complexity);
            Console.WriteLine(task.CreatedAtDate);
            Console.WriteLine(task.RequiredTime);
            Console.WriteLine(task.StartDate);
            Console.WriteLine(task.ScheduledDate);
            Console.WriteLine(task.DeadLineDate);
            Console.WriteLine(task.CompleteDate);
            Console.WriteLine(task.Deliveables);
            Console.WriteLine(task.Remarks);
            Console.WriteLine(task.ChefId);

        }
        public static void printChef(Chef c)
        {
            Console.WriteLine(c.ChefId);
            Console.WriteLine("Beginner");
            Console.WriteLine(c.Name);
            Console.WriteLine(c.Email);
            Console.WriteLine(c.Cost);
             
        }

        public static Dependency? inputAndCreateDependency(int id=0)
        {
            Console.WriteLine("Enter preTask and currTask:");
            int Id = id;
            int? preTask = (int)ReadNum();
            int? currTask = (int)ReadNum();
            Dependency d = new Dependency(Id, preTask, currTask);
            return d;
        }
        public static void printDependency(Dependency d)
        {
            Console.WriteLine("Id:"+d.Id);
;           Console.WriteLine("preTask:"+d.preTask);
            Console.WriteLine("currTask:"+d.currTask);
            Console.WriteLine();
        }

        public static double? ReadNum()
        {
            string? input = Console.ReadLine();
            double? num = double.Parse(input);
            return num;
        }

        public static void switchFunChef()
        {
            //sub menu for chef
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            string? input = Console.ReadLine();
            int choice = int.Parse(input);

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                    Console.WriteLine("Enter id, Level(Beginner/Advanced/Expert), Name, Email and cost:");

                    int ChefId = (int)ReadNum();                 //unique  
                    ChefExperience Level = ChefExperience.Beginner;
                    string? Name = Console.ReadLine();
                    string? Email = Console.ReadLine();
                    double? Cost = ReadNum();


                    Chef c = new(ChefId, Level, Name, Email, Cost);
                    s_dalChef!.Create(c);

                    break;
                case 3: //Read
                    Console.WriteLine("Enter id:");
                    int id = (int)ReadNum();
                    Chef? chef = s_dalChef.Read(id);
                    if (chef == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        printChef(chef);

                    break;
                case 4: //ReadAll
                    List<Chef> lCh = s_dalChef!.ReadAll();
                    foreach (var _chef in lCh)
                    {
                        printChef(_chef);
                    }

                    break;
                case 5: //Update
                    //print the object to update(and then update it)
                    Console.WriteLine("Enter id");
                    int ChefId1 = (int)ReadNum();
                    Chef? chef1 = s_dalChef.Read(ChefId1);
                    if (chef1 == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printChef(chef1);

                    // the update
                    Console.WriteLine("Enter Level(Beginner/Advanced/Expert), Name, Email and cost:");
                    ChefExperience? Level1 = ChefExperience.Beginner;
                    string? Name1 = Console.ReadLine();
                    string? Email1 = Console.ReadLine();
                    double? Cost1 = ReadNum();
                    if (Level1 == null || Name1 == null || Email1 == null || Cost1 == null)
                        break;
                    Chef ch = new Chef(ChefId1, Level1, Name1, Email1, Cost1);
                    s_dalChef!.Update(ch);

                    break;

                default:
                    break;
            }
        }
        public static void switchFunTask()
        {
            //sub menu for Task
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            string? input = Console.ReadLine();
            int choice = int.Parse(input);

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                      
                    Task tCreate = inputAndCreateTask();

                    s_dalTask!.Create(tCreate);
                    break;

                case 3: //Read
                    Console.WriteLine("Enter id:");
                    int id = Console.Read();
                    Task? tRead = s_dalTask.Read(id);
                    if (tRead == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        printTask(tRead);
                    break;

                case 4: //ReadAll
                    List<Task> lTa = s_dalTask!.ReadAll();
                    foreach (var _task in lTa)
                        printTask(_task);
                    break;

                case 5: //Update
                    //print the object to update (and then update it)
                    Console.WriteLine("Enter id");
                    int TaskIdUpdate = Console.Read();

                    Task taskUpdate = s_dalTask.Read(TaskIdUpdate);
                    if (taskUpdate == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printTask(taskUpdate);

                    Task taskUpdateNew = inputAndCreateTask();
                    if (
                        taskUpdateNew.Alias == null ||
                        taskUpdateNew.Description == null ||
                        taskUpdateNew.IsMilestone == null ||
                        taskUpdateNew.Complexity == null ||
                        taskUpdateNew.CreatedAtDate == null ||
                        taskUpdateNew.RequiredTime == null ||
                        taskUpdateNew.StartDate == null ||
                        taskUpdateNew.ScheduledDate == null ||
                        taskUpdateNew.DeadLineDate == null ||
                        taskUpdateNew.CompleteDate == null ||
                        taskUpdateNew.Deliveables == null ||
                        taskUpdateNew.Remarks == null ||
                        taskUpdateNew.ChefId == null)
                    {
                        break;
                    }
                    else
                    {

                        s_dalTask!.Update(taskUpdateNew);
                        break;
                    }

                default:
                    break;
            }
        }
        public static void switchFunDependency()
        {
            //sub menu for dependency
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            int choice = (int)ReadNum();

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                    Dependency dCreate = inputAndCreateDependency();
                    s_dalDependency!.Create(dCreate);
                    break;

                case 3: //Read
                    Console.WriteLine("Enter id:");
                    int id = (int)ReadNum();
                    Dependency? dRead = s_dalDependency.Read(id);
                    if (dRead == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        printDependency(dRead);
                    break;

                case 4: //ReadAll
                    List<Dependency> lDe = s_dalDependency!.ReadAll();
                    foreach (var _dependency in lDe)
                    {
                        printDependency(_dependency);
                    }
                    break;

                case 5: //Update
                    //print the object to update(and then update it)
                    Console.WriteLine("Enter id");
                    int dependencyIdUpdate = (int)ReadNum(); ;
                    Dependency? dependencyUpdate = s_dalDependency.Read(dependencyIdUpdate);
                    if (dependencyUpdate == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printDependency(dependencyUpdate);

                    // the updated.
                    Dependency dependencyUpdateNew = inputAndCreateDependency(dependencyIdUpdate);
                    if (dependencyUpdateNew.Id == null || dependencyUpdateNew.preTask == null || dependencyUpdateNew.currTask == null)
                        break;
                    s_dalDependency!.Update(dependencyUpdateNew);
                    break;

                default:
                    break;
            }
        }

        private static IChef? s_dalChef = new ChefImplementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalChef, s_dalTask, s_dalDependency);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            int choice = 1;
            string? input = null;

            while (choice != 0)
            {
                Console.WriteLine("Choose an entity you'd like to check:");
                Console.WriteLine("0. Exit\n" + "1. Chef\n" + "2.Task\n" + "3.Dependency\n");
                input = Console.ReadLine();
                choice=int.Parse(input);

                try
                {

                    switch (choice)//main menu
                    {
                        case 0://exit
                            break;

                        case 1://sub-menu chef
                            switchFunChef();
                            break;

                        case 2://sub-menu tasks
                            switchFunTask();
                            break;

                        case 3://sub-menu Dependency
                            switchFunDependency();
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