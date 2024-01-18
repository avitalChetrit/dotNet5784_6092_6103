using Dal;
using DalApi;
using DO;
using System.Xml.Linq;
using Task = DO.Task;

namespace DalTest
{
    internal class Program
    {
        public static double? ReadNum()
        {
            string? input = Console.ReadLine();
            double? num = double.Parse(input);
            return num;
        }

        //functions get objects and prints them
        public static void printChef(Chef c)
        {
            Console.WriteLine("ChefId: " + c.ChefId);
            Console.WriteLine("Experience: " + c.Level);
            Console.WriteLine("Name: " + c.Name);
            Console.WriteLine("Email: " + c.Email);
            Console.WriteLine("Cost: " + c.Cost);
            Console.WriteLine();
        }
        public static void printTask(Task task)
        {
            Console.WriteLine("id: " + task.Id);
            Console.WriteLine("Alias: " + task.Alias);
            Console.WriteLine("Description: " + task.Description);
            Console.WriteLine("IsMilestone: " + task.IsMilestone);
            Console.WriteLine("Complexity: " + task.Complexity);
            Console.WriteLine("CreatedAtDate: " + task.CreatedAtDate);
            Console.WriteLine("RequiredTime: " + task.RequiredTime);
            Console.WriteLine("StartDate: " + task.StartDate);
            Console.WriteLine("ScheduledDate: " + task.ScheduledDate);
            Console.WriteLine("DeadLineDate: " + task.DeadLineDate);
            Console.WriteLine("CompleteDate: " + task.CompleteDate);
            Console.WriteLine("Deliverables: " + task.Deliveables);
            Console.WriteLine("Remarks: " + task.Remarks);
            Console.WriteLine("ChefId: " + task.ChefId);
            Console.WriteLine();

        }
        public static void printDependency(Dependency d)
        {
            Console.WriteLine("Id:" + d.Id);
            ; Console.WriteLine("PreTask: " + d.PreTask);
            Console.WriteLine("CurrTask: " + d.CurrTask);
            Console.WriteLine();
        }

        //functions get input from user and return an object using the input
        public static Chef inputAndCreateChef(int ChefId)
        {   
            Console.Write("Enter Level (Beginner/Advanced/Expert): ");
            ChefExperience Level = Enum.Parse<ChefExperience>(Console.ReadLine());

            Console.Write("Enter Name: ");
            string? Name = Console.ReadLine();

            Console.Write("Enter Email: ");
            string? Email = Console.ReadLine();

            Console.Write("Enter Cost: ");
            double? Cost = ReadNum();

            Chef c = new(ChefId, Level, Name, Email, Cost);
            return c;  
        }
        public static Task inputAndCreateTask(int id = 0)
        {
            // INPUT
            int Id = 0;

            Console.Write("Enter Alias: ");
            string? Alias = Console.ReadLine();

            Console.Write("Enter Description: ");
            string? Description = Console.ReadLine();

            Console.Write("IsMilestone (true/false): ");
            bool IsMilestone = bool.Parse(Console.ReadLine());

            Console.Write("Complexity (Beginner/Intermediate/Advanced): ");
            ChefExperience? Complexity = Enum.Parse<ChefExperience>(Console.ReadLine());

            Console.Write("Enter CreatedAtDate (yyyy-MM-dd): ");
            DateTime? CreatedAtDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter RequiredTime (hh:mm:ss): ");
            TimeSpan? RequiredTime = TimeSpan.Parse(Console.ReadLine());

            Console.Write("Enter StartDate (yyyy-MM-dd): ");
            DateTime? StartDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter ScheduledDate (yyyy-MM-dd): ");
            DateTime? ScheduledDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter DeadLineDate (yyyy-MM-dd): ");
            DateTime? DeadLineDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter CompleteDate (yyyy-MM-dd): ");
            DateTime? CompleteDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Deliverables: ");
            string? Deliveables = Console.ReadLine();

            Console.Write("Enter Remarks: ");
            string? Remarks = Console.ReadLine();

            Console.Write("Enter ChefId: ");
            int? ChefId = (int)ReadNum();

            Task tCreate = new Task(Id, Alias, Description, IsMilestone, Complexity, 
                            CreatedAtDate, RequiredTime, StartDate, ScheduledDate, DeadLineDate, 
                            CompleteDate, Deliveables, Remarks, ChefId);
            return tCreate;
        }
        public static Dependency? inputAndCreateDependency(int id=0)
        {

            Console.Write("Enter PreTask: ");
            int? PreTask = (int)ReadNum();

            Console.Write("Enter CurrTask: ");
            int? CurrTask = (int)ReadNum();

            Dependency d = new Dependency(id, PreTask, CurrTask);
            return d;
        }

        public static void switchFunChef()
        {
            //sub menu for chef
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1.Exit\n" + "2.Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            
            int choice = (int)ReadNum();

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                    Console.Write("Enter id: ");
                    int ChefId = (int)ReadNum(); // unique
                    Chef c = inputAndCreateChef(ChefId);
                    s_dal!.Chef.Create(c);
                    break;

                case 3: //Read
                    Console.WriteLine("Enter id: ");
                    int id = (int)ReadNum();
                    Chef? chef = s_dal!.Chef.Read(id);
                    if (chef == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        printChef(chef);
                    break;
                    
                case 4: //ReadAll
                    List<Chef> lCh = s_dal!.Chef.ReadAll().ToList<Chef>();
                    foreach (var _chef in lCh)
                    {
                        printChef(_chef);
                    }
                    break;

                case 5: //Update
                    //print the object to update(and then update it)
                    Console.WriteLine("Enter id: ");
                    int ChefUpdateId = (int)ReadNum();

                    Chef? chefUpdate = s_dal!.Chef.Read(ChefUpdateId);
                    if (chefUpdate == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printChef(chefUpdate);

                    chefUpdate = inputAndCreateChef(ChefUpdateId);                    
                    if (   chefUpdate.Level == null || chefUpdate.Name == null 
                        || chefUpdate.Email == null || chefUpdate.Cost == null)
                        break;

                    s_dal!.Chef.Update(chefUpdate);
                    break;

                default:
                    break;
            }
        }
        public static void switchFunTask()
        {
            //sub menu for Task
            Console.WriteLine("Choose a method to preform:");
            Console.WriteLine("1.Exit\n" + "2.Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            
            int choice = (int)ReadNum();

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                      
                    Task tCreate = inputAndCreateTask();

                    s_dal!.Task.Create(tCreate);
                    break;

                case 3: //Read
                    Console.WriteLine("Enter id:");
                    int id = (int)ReadNum(); 
                    Task? tRead = s_dal!.Task.Read(id);
                    if (tRead == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        printTask(tRead);
                    break;

                case 4: //ReadAll
                    List<Task> lTa = s_dal!.Task.ReadAll().ToList<Task>();
                    foreach (var _task in lTa)
                        printTask(_task);
                    break;

                case 5: //Update
                    //print the object to update (and then update it)
                    Console.WriteLine("Enter id");
                    int TaskIdUpdate = (int)ReadNum();

                    Task taskUpdate = s_dal!.Task.Read(TaskIdUpdate);
                    if (taskUpdate == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printTask(taskUpdate);

                    Task taskUpdateNew = inputAndCreateTask(TaskIdUpdate);
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

                        s_dal!.Task.Update(taskUpdateNew);
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
            Console.WriteLine("1.Exit\n" + "2.Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n");
            int choice = (int)ReadNum();

            switch (choice)
            {
                case 1: //Exit
                    break;

                case 2: //Create
                    Dependency dCreate = inputAndCreateDependency();
                    s_dal!.Dependency.Create(dCreate);
                    break;

                case 3: //Read
                    Console.WriteLine("Enter id:");
                    int id = (int)ReadNum();
                    Dependency? dRead = s_dal!.Dependency.Read(id);
                    if (dRead == null)
                        Console.WriteLine("Doesn't Exist");
                    else
                        printDependency(dRead);
                    break;

                case 4: //ReadAll
                    List<Dependency> lDe = s_dal!.Dependency.ReadAll().ToList<Dependency>();
                    foreach (var _dependency in lDe)
                    {
                        printDependency(_dependency);
                    }
                    break;

                case 5: //Update
                    //print the object to update(and then update it)
                    Console.WriteLine("Enter id");
                    int dependencyIdUpdate = (int)ReadNum(); ;
                    Dependency? dependencyUpdate = s_dal!.Dependency.Read(dependencyIdUpdate);
                    if (dependencyUpdate == null)
                    {
                        Console.WriteLine("Doesn't Exist");
                        break;
                    }
                    else
                        printDependency(dependencyUpdate);

                    // the updated.
                    Dependency dependencyUpdateNew = inputAndCreateDependency(dependencyIdUpdate);
                    if (dependencyUpdateNew.Id == null || dependencyUpdateNew.PreTask == null || dependencyUpdateNew.CurrTask == null)
                        break;
                    s_dal!.Dependency.Update(dependencyUpdateNew);
                    break;

                default:
                    break;
            }
        }

        //private static IChef? s_dalChef = new ChefImplementation(); //stage 1
        //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        //private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1

        //static readonly IDal s_dal = new DalList(); //stage 2
        static readonly IDal s_dal = new DalXml(); //stage 3



        static void Main()
        {
            //try    // stage 1
            //{
            //    Initialization.Do(s_dal);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            int choice = 1;
            string? input = null;

            while (choice != 0)
            {
                Console.WriteLine("Choose an entity you'd like to check:");
                Console.WriteLine("0.Exit\n" + "1.Chef\n" + "2.Task\n" + "3.Dependency\n" + "4.Initialization\n");
                choice=(int)ReadNum();

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

                        case 4://sub-menu Initialization
                            Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                            if (ans == "Y") //stage 3
                            {
                                XElement rootElem = null;
                                XmlTools.SaveListToXMLElement(rootElem, "dependencys");
                                Initialization.Do(s_dal); //stage 2
                            }
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