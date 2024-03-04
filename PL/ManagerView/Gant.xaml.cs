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

namespace PL.ManagerView;

/// <summary>
/// Interaction logic for Gant.xaml
/// </summary>
public partial class Gant : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty TaskGanttListProperty =
    DependencyProperty.Register("TaskGanttList", typeof(IEnumerable<TaskGantt>), typeof(Gant));
    public IEnumerable<TaskGantt> TaskGanttList
    {
        get { return (IEnumerable<TaskGantt>)GetValue(TaskGanttListProperty); }
        set { SetValue(TaskGanttListProperty, value); }
    }

    public void GetList()
    {
        DateTime? startProject = s_bl.Task.ReadAll().Min(task=> task.StartDate);
        DateTime? endProject = s_bl.Task.ReadAll().Max(task => task.CompleteDate);


        TaskGanttList = s_bl.Task.ReadAll().Select(task => new TaskGantt
        {
            TaskId = task.Id,
            TaskName = task.Alias,
            Duration = task.RequiredTime.Value.Days,
            // Calculate TimeFromStart and TimeToEnd based on your logic
            TimeFromStart = (int)(task.StartDate- startProject).Value.TotalDays,
            TimeToEnd = (int)(endProject- task.CompleteDate).Value.TotalDays
        }).ToList();

    }
    public class TaskGantt
    {
        public int TaskId;
        public string TaskName; 
        public int Duration;  //משך הזמן בימים
        public int TimeFromStart; //
        public int TimeToEnd;    
    }
     
    public Gant()
    {
        InitializeComponent();
    }
}
