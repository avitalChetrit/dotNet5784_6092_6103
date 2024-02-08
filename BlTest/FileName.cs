using BO;
using Task = BO.Task;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace BlTest;

internal class FileName
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static Task inputAndCreateTask(int id = 0)
    {
        DateTime CreatedAtDate = DateTime.Today;
        BO.Status Status = BO.Status.Unscheduled;
        DateTime? StartDate = null;
        DateTime? ScheduledDate = null;
        DateTime? ForecastDate = null;
        DateTime? CompleteDate = null;

        // INPUT
        Console.Write("Enter Id: ");
        int? Id = int.Parse(Console.ReadLine());

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
        List < BO.TaskInList > dependencies = new List<BO.TaskInList>();
        string[] dependenciesId = Console.ReadLine().Split(',');
        foreach (var dependency in dependenciesId)
        {
            Task task = s_bl.Task.Read(int.Parse(dependency));
            if (task == null)
                throw new BlWrongInputException("Wrong Input");
            dependencies.Add(new BO.TaskInList { Id = task.Id, Alias = task.Alias,
            Description = task.Description, Status = s_bl!.Task.findStat(task) });
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



    static void MainPlanning()
    {
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine("Choose an entity you'd like to check:");
            Console.WriteLine("0.Exit\n" + "1.Chef\n" + "2.Task\n");
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
            if(Sheduled.level== ScheduleLevel.Planning)
            {
                MainPlanning();
            }
        }
    }
}