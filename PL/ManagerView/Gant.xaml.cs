using PL.Chef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static PL.ManagerView.Gant;

namespace PL.ManagerView;

/// <summary>
/// Interaction logic for Gant.xaml
/// </summary>
public partial class Gant : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty TaskGanttListProperty =
    DependencyProperty.Register("TaskGanttList", typeof(List<TaskGantt>), typeof(Gant));
    public List<TaskGantt> TaskGanttList
    {
        get { return (List<TaskGantt>)GetValue(TaskGanttListProperty); }
        set { SetValue(TaskGanttListProperty, value); }
    }

    public void GetList()
    {
        DateTime? startProject = s_bl.Task.ReadAll().Min(task => task.StartDate);
        DateTime? endProject = s_bl.Task.ReadAll().Max(task => task.ForecastDate);


        TaskGanttList = s_bl.Task.ReadAll().Select(task => new TaskGantt
        {
            TaskId = task.Id,
            TaskName = task.Alias,
            Duration = task.RequiredTime.Value.Days,
            // Calculate TimeFromStart and TimeToEnd based on your logic
            TimeFromStart = (int)(task.StartDate - startProject).Value.TotalDays,
            TimeToEnd = (int)(endProject - task.ForecastDate).Value.TotalDays
        }).ToList();

        //  TaskGanttList = new List<TaskGantt>()
        //{new TaskGantt() {TaskId = 1,TaskName = "T1",Duration = 30, TimeFromStart = 20,    TimeToEnd = 7},
        //new TaskGantt() {TaskId = 2,TaskName = "T2",Duration = 50, TimeFromStart = 60,    TimeToEnd = 4},
        //new TaskGantt() { TaskId = 3, TaskName = "T3", Duration = 70, TimeFromStart = 10, TimeToEnd = 13 }
        //}//.ToList();
    }
    public class TaskGantt
    {
        public int TaskId;
        public string TaskName; 
        public int Duration;  //משך הזמן בימים
        public int TimeFromStart; //
        public int TimeToEnd;    
    }
     
    public Gant() //ctor
    {
        InitializeComponent();
        GetList();

       // TaskGantt t1 = new TaskGantt { TaskId = 21, TaskName = "aa", Duration = 200, TimeFromStart = 100, TimeToEnd = 100 };
        
        //TaskGanttList.Add(t1);
    }
}


//public GanttCharWindow()
//{
//    InitializeComponent();

//    //GanttTasksList = (from task in s_bl.Task.ReadAllFullTasksDetails()
//    //                 select convertTaskToGanttTask(task)).ToList();
//    GanttTasksList = new List<TaskGantt>()
//      {new TaskGantt() {taskID = 1,taskName = "T1",duration = 30, timeFromStart = 20,    timeToEnd = 7},
//      new TaskGantt() {taskID = 2,taskName = "T2",duration = 50, timeFromStart = 60,    timeToEnd = 4},
//      new TaskGantt() { taskID = 3, taskName = "T3", duration = 70, timeFromStart = 10, timeToEnd = 13 }
//      };

//}