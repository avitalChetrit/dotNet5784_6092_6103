using DalApi;
using BO;
using Task = BO.Task;
using System.Xml.Linq;
using System.Reflection.Emit;
namespace BlTest;

internal class Program
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static void printChef(Chef c)
    {
        Console.WriteLine("ChefId: " + c.Id);
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
        Console.WriteLine("Complexity: " + task.Complexity);
        Console.WriteLine("CreatedAtDate: " + task.CreatedAtDate);
        Console.WriteLine("RequiredTime: " + task.RequiredTime);
        Console.WriteLine("StartDate: " + task.StartDate);
        Console.WriteLine("ScheduledDate: " + task.ScheduledDate);
        Console.WriteLine("ForecastDate: " + task.ForecastDate);
        Console.WriteLine("CompleteDate: " + task.CompleteDate);
        Console.WriteLine("Deliverables: " + task.Deliveables);
        Console.WriteLine("Remarks: " + task.Remarks);
        Console.WriteLine("Status: " + task.Status);
        if(task.Chef is not null) 
            Console.WriteLine("Chef In Task: Id="+ task.Chef.Id+ "Name= " + task.Chef.Name);
        Console.WriteLine("Task In List: ");
        foreach( var dep in task.Dependecies)
        {
            Console.WriteLine("Id= " + dep.Id +
                "Description= " + dep.Description +
                "Alias= " + dep.Alias +
                "Status= " + dep.Status);
        }
        Console.WriteLine();

    }
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
        if (Sheduled.level != ScheduleLevel.Planning)
            throw new BO.BlUnableToPreformActionInThisProjectStageException("Can't Create chef In This Project Stage");

        DateTime CreatedAtDate = DateTime.Today;
        BO.Status Status = BO.Status.Unscheduled;
        DateTime? StartDate = null;
        DateTime? ScheduledDate = null;
        DateTime? ForecastDate = null;
        DateTime? CompleteDate = null;

        // INPUT

        Console.Write("Enter Description: ");
        string? Description = Console.ReadLine();

        Console.Write("Enter Alias: ");
        string? Alias = Console.ReadLine();

        Console.Write("Enter Required Time (in format hh:mm:ss): ");
        TimeSpan RequiredTime = TimeSpan.Parse(Console.ReadLine());

        Console.Write("Enter Deliverables: ");
        String Deliveables = Console.ReadLine();

        Console.Write("Enter Remarks: ");
        String Remarks = Console.ReadLine();

        Console.Write("Enter Chef Complexity (Low/Medium/High): ");
        BO.ChefExperience Complexity = (BO.ChefExperience)Enum.Parse(typeof(BO.ChefExperience), Console.ReadLine());


        Console.Write("Dependencies (comma-separated, as integers): ");
        List<BO.TaskInList> dependencies = new List<BO.TaskInList>();
        string[] dependenciesId = Console.ReadLine().Split(',');
        foreach (var dependency in dependenciesId)
        {
            Task task = s_bl.Task.Read(int.Parse(dependency));
            if (task == null)
                throw new BlWrongInputException("Wrong Input");
            dependencies.Add(new BO.TaskInList
            {
                Id = task.Id,
                Alias = task.Alias,
                Description = task.Description,
                Status = s_bl!.Task.findStat(task.Id)
            });
        }

        return new Task()
        {
            Id = id,
            Alias = Alias,
            Description = Description,
            Complexity = (BO.ChefExperience)Complexity,
            CreatedAtDate = CreatedAtDate,
            RequiredTime = RequiredTime,
            StartDate = StartDate,
            ScheduledDate = ScheduledDate,
            CompleteDate = CompleteDate,
            Deliveables = Deliveables,
            Remarks = Remarks,
        };


    }

    public static Task inputAndUpdateTask(int id = 0)
    {
        
            DateTime? CreatedAtDate = null;
            BO.Status? Status = null;
            DateTime? StartDate = null;
            DateTime? ScheduledDate = null;
            DateTime? ForecastDate = null;
            DateTime? CompleteDate = null;
            TimeSpan? RequiredTime = null;
            List<BO.TaskInList> dependencies = null;

        // INPUT

            Console.Write("Enter Description: ");
            string? Description = Console.ReadLine();

            Console.Write("Enter Alias: ");
            string? Alias = Console.ReadLine();

            Console.Write("Enter Deliverables: ");
            String Deliveables = Console.ReadLine();

            Console.Write("Enter Remarks: ");
            String Remarks = Console.ReadLine();

            Console.Write("Enter Chef Complexity (Low/Medium/High): ");
            BO.ChefExperience Complexity = (BO.ChefExperience)Enum.Parse(typeof(BO.ChefExperience), Console.ReadLine());

            return new Task()
            {
                Id = id,
                Alias = Alias,
                Description = Description,
                Complexity = (BO.ChefExperience)Complexity,
                CreatedAtDate = CreatedAtDate,
                RequiredTime = RequiredTime,
                StartDate = StartDate,
                ScheduledDate = ScheduledDate,
                CompleteDate = CompleteDate,
                Deliveables = Deliveables,
                Remarks = Remarks,
            };
        
    }

    private static void switchFunChef()
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
                    printChef(chef);
                break;

            case 4: //ReadAll
                List<Chef> lCh = s_bl!.Chef.ReadAll().ToList<Chef>();
                foreach (var _chef in lCh)
                {
                    printChef(_chef);
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
                    printChef(chefUpdate);

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
    private static void switchFunTask()
    {
        ScheduleLevel lev = s_bl!.Sheduled.levelStatuas();
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
                    printTask(tRead); 
                break;

            case 4: //ReadAll
                List<Task> lTa = s_bl!.Task.ReadAll().ToList<Task>();
                foreach (var _task in lTa)
                    printTask(_task);
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
                    printTask(taskUpdate);

                if(Sheduled.level == ScheduleLevel.Mid)
                {
                    Console.WriteLine("Enter ScheduledDate: ");
                    DateTime d = DateTime.Parse(Console.ReadLine());
                    s_bl!.Task.UpdateDate(TaskIdUpdate, d);
                    s_bl!.Sheduled.levelStatuas();
                    break;
                }

                Task taskUpdateNew;
                if (Sheduled.level == ScheduleLevel.Planning)
                {
                    taskUpdateNew = inputAndCreateTask(TaskIdUpdate);
                }
                else
                    taskUpdateNew = inputAndUpdateTask(TaskIdUpdate);

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
        DateTime? start = null;

        while (choice != 0)
        {
            Console.WriteLine("Choose an entity you'd like to check:");
            Console.WriteLine("0.Exit\n" + "1.Chef\n" + "2.Task\n");
            if(Sheduled.level == ScheduleLevel.Planning)
            {
                Console.WriteLine("3.Initial Start Date\n");
            }
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

                    case 3://Initial Start Date
                        Sheduled.StartDate = DateTime.Parse(Console.ReadLine());
                        s_bl!.Sheduled.levelStatuas();
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
