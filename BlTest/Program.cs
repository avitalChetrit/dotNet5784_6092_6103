using DalApi;
using BO;
using Task = BO.Task;
using System.Xml.Linq;
using System.Reflection.Emit;
namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static Chef inputAndCreateChef(int ChefId)
    {
        Console.Write("Enter Level (Beginner/Advanced/Expert): ");
        ChefExperience Level = Enum.Parse<ChefExperience>(Console.ReadLine());

        Console.Write("Enter Name: ");
        string? Name = Console.ReadLine();

        Console.Write("Enter Email: ");
        string? Email = Console.ReadLine();

        Console.Write("Enter Cost: ");
        double? Cost = double.Parse(Console.ReadLine());

        Chef c = new BO.Chef()
        {
            Id = ChefId,
            Name = Name,
            Email = Email,
            Level = Level,
            Cost = Cost,
            Task = null,
        };
        return c;
    }
    public static Task inputAndCreateTask(int id = 0)
    {
        // INPUT
        Console.Write("Enter Alias: ");
        string? Alias = Console.ReadLine();

        Console.Write("Enter Description: ");
        string? Description = Console.ReadLine();

        Console.Write("Complexity (Beginner/Intermediate/Advanced): ");
        ChefExperience? Complexity = Enum.Parse<ChefExperience>(Console.ReadLine());

        Console.Write("Enter CreatedAtDate (yyyy-MM-dd): ");
        DateTime CreatedAtDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter RequiredTime (hh:mm:ss): ");
        TimeSpan RequiredTime = TimeSpan.Parse(Console.ReadLine());

        Console.Write("Enter StartDate (yyyy-MM-dd): ");
        DateTime StartDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter ScheduledDate (yyyy-MM-dd): ");
        DateTime ScheduledDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter CompleteDate (yyyy-MM-dd): ");
        DateTime CompleteDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter Deliverables: ");
        string? Deliveables = Console.ReadLine();

        Console.Write("Enter Remarks: ");
        string? Remarks = Console.ReadLine();

        Console.Write("Enter ChefId: ");
        int? ChefId = int.Parse(Console.ReadLine());

        Task task = new Task()
        {
            Id = id,
            Alias= Alias, 
            Description = Description, 
            Complexity= (BO.ChefExperience)Complexity,
            CreatedAtDate= CreatedAtDate,
            RequiredTime=RequiredTime, 
            StartDate= StartDate, 
            ScheduledDate= ScheduledDate, 
            CompleteDate= CompleteDate, 
            Deliveables= Deliveables, 
            Remarks= Remarks, 
        };
        
        return task;
    }
    public static void switchFunChef()
    {
        //sub menu for chef
        Console.WriteLine("Choose a method to preform:");
        Console.WriteLine("1.Exit\n" + "2.Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n" + "6.Delete\n");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1: //Exit
                break;

            case 2: //Create
                Console.Write("Enter id: ");
                int ChefId = int.Parse(Console.ReadLine()); // unique
                Chef c = inputAndCreateChef(ChefId);
                s_bl!.Chef.Create(c);
                break;

            case 3: //Read
                Console.WriteLine("Enter id: ");
                int id = int.Parse(Console.ReadLine());
                Chef? chef = s_bl!.Chef.Read(id);
                if (chef == null)
                    Console.WriteLine("Doesn't Exist");
                else
                    Console.WriteLine(chef);
                break;

            case 4: //ReadAll
                List<Chef> lCh = s_bl!.Chef.ReadAll().ToList<Chef>();
                foreach (var _chef in lCh)
                {
                    Console.WriteLine(_chef);
                }
                break;

            case 5: //Update
                    //print the object to update(and then update it)
                Console.WriteLine("Enter id: ");
                int ChefUpdateId = int.Parse(Console.ReadLine());

                Chef? chefUpdate = s_bl!.Chef.Read(ChefUpdateId);
                if (chefUpdate == null)
                {
                    Console.WriteLine("Doesn't Exist");
                    break;
                }
                else
                    Console.WriteLine(chefUpdate);

                chefUpdate = inputAndCreateChef(ChefUpdateId);
                Console.WriteLine(" Enter Task id and its Alias: ");
                int IdTask=int.Parse(Console.ReadLine());
                string alias = Console.ReadLine();
                TaskInChef taskInChef = new TaskInChef()
                {
                    Id = IdTask,
                    Alias = alias
                };

                chefUpdate.Task = taskInChef;

                if (chefUpdate.Level == null || chefUpdate.Name == null
                    || chefUpdate.Email == null || chefUpdate.Cost == null)
                    break;

                s_bl!.Chef.Update(chefUpdate);
                break;

            case 6: //Delete
                Console.WriteLine("Enter id: ");
                int ChefDelteId = int.Parse(Console.ReadLine());

                Chef? chefDel = s_bl!.Chef.Read(ChefDelteId);
                if (chefDel == null)
                {
                    Console.WriteLine("Doesn't Exist");
                    break;
                }
                else
                    s_bl!.Chef.Delete(ChefDelteId);
                break;

            default:
                break;
        }
    }
    public static void switchFunTask()
    {
        //sub menu for Task
        Console.WriteLine("Choose a method to preform:");
        Console.WriteLine("1.Exit\n" + "2.Create\n" + "3.Read\n" + "4.ReadAll\n" + "5.Update\n" + "6.Delete\n");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1: //Exit
                break;

            case 2: //Create
                Task tCreate = inputAndCreateTask();
                s_bl!.Task.Create(tCreate);
                break;

            case 3: //Read
                Console.WriteLine("Enter id:");
                int id = int.Parse(Console.ReadLine());
                Task? tRead = s_bl!.Task.Read(id);
                if (tRead == null)
                    Console.WriteLine("Doesn't Exist");
                else
                    Console.WriteLine(tRead); 
                break;

            case 4: //ReadAll
                List<Task> lTa = s_bl!.Task.ReadAll().ToList<Task>();
                foreach (var _task in lTa)
                    Console.WriteLine(_task);
                break;

            case 5: //Update
                    //print the object to update (and then update it)
                Console.WriteLine("Enter id");
                int TaskIdUpdate = int.Parse(Console.ReadLine());

                Task taskUpdate = s_bl!.Task.Read(TaskIdUpdate);
                if (taskUpdate == null)
                {
                    Console.WriteLine("Doesn't Exist");
                    break;
                }
                else
                    Console.WriteLine(taskUpdate);

                Task taskUpdateNew = inputAndCreateTask(TaskIdUpdate);
                if (
                    taskUpdateNew.Alias == null ||
                    taskUpdateNew.Description == null ||
                    taskUpdateNew.Complexity == null ||
                    taskUpdateNew.CreatedAtDate == null ||
                    taskUpdateNew.RequiredTime == null ||
                    taskUpdateNew.StartDate == null ||
                    taskUpdateNew.ScheduledDate == null ||
                    taskUpdateNew.CompleteDate == null ||
                    taskUpdateNew.Deliveables == null ||
                    taskUpdateNew.Remarks == null)
                {
                    break;
                }
                else
                {
                    s_bl!.Task.Update(taskUpdateNew);
                    break;
                }

            case 6: //Delete
                Console.WriteLine("Enter id: ");
                int TaskDelId = int.Parse(Console.ReadLine());
                Task? TaskDel = s_bl!.Task.Read(TaskDelId);
                if (TaskDel == null)
                {
                    Console.WriteLine("Doesn't Exist");
                    break;
                }
                else
                    s_bl!.Task.Delete(TaskDelId);
                break;
            default:
                break;
        }
    }
    static void Main()
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

        int choice = 1;
        string? input = null;

        while (choice != 0)
        {
            Console.WriteLine("Choose an entity you'd like to check:");
            Console.WriteLine("0.Exit\n" + "1.Chef\n" + "2.Task\n" );
            choice = int.Parse(Console.ReadLine());

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

                    //case 3://sub-menu Initialization
                    //    Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                    //    string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                    //    if (ans == "Y") //stage 3
                    //    {
                    //        //Initialization.Do(s_dal); //stage 2
                    //        Initialization.Do(); //stage 4
                    //    }
                    //    break;

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
