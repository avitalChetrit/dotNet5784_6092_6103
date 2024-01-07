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
            //Console.WriteLine(task.Id + task.Alias + task.Description + task.IsMilestone + task.Complexity + task.CreatedAtDate + task.RequiredTime,
            //            task.StartDate + task.ScheduledDate + task.DeadLineDate + task.CompleteDate + task.Deliveables + task.Remarks + task.ChefId);

        }

        public static Dependency? inputAndCreateDependency()
        {
            Console.WriteLine("Enter preTask and currTask:");
            int Id = 0;
            int? preTask = Console.Read();
            int? currTask = Console.Read();
            Dependency d = new Dependency(Id, preTask, currTask);
            return d;
        }
        void printDependency(Dependency d)
        {
            Console.WriteLine(d.Id+ d.preTask+ d.currTask);
        }

        static void switchFunChef()
        {
            //sub menu for chef
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            int choice = Console.Read();

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                    Console.WriteLine("Enter id, Level(Beginner/Advanced/Expert), Name, Email and cost:");
                    int ChefId = Console.Read();                 //unique  
                    ChefExperience Level = (ChefExperience)Console.Read();
                    string? Name = Console.ReadLine();
                    string? Email = Console.ReadLine();
                    double? Cost = Console.Read();
                    Chef c = new(ChefId, Level, Name, Email, Cost);
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
                    string? Email1 = Console.ReadLine();
                    double? Cost1 = Console.Read();
                    if (Level1 == null || Name1 == null || Email1 == null || Cost1 == null)
                        break;
                    Chef ch = new Chef(ChefId1, Level1, Name1, Email1, Cost1);
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
        static void switchFunTask()
        {
            //sub menu for Task
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            int choice = Console.Read();

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
        static void switchFunDependency()
        {
            //sub menu for dependency
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1. Exit\n" + "2. Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            int choice = Console.Read();

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
                    int id = Console.Read();
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
                    int dependencyIdUpdate = Console.Read();
                    Dependency? dependencyUpdate = s_dalDependency.Read(dependencyIdUpdate);
                    if (dependencyUpdate == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printDependency(dependencyUpdate);

                    // the updated.
                    Dependency dependencyUpdateNew = inputAndCreateDependency();
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

            while (choice != 0)
            {
                Console.WriteLine("Choose an entity you'd like to check:");
                Console.WriteLine("0. Exit\n" + "1. Chef\n" + "2.Task\n" + "3.Dependency\n");
                choice = Console.Read();

                try
                {
                    switch (choice)//main menu
                    {
                        case 1://sub-menu chef
                            switchFunChef();
                            break;

                        case 2://sub-menu tasks
                            switchFunTask();
                            break;

                        case 3://sub-menu Dependency
                            switchFunDependency();
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


