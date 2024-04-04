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

namespace PL.ManagerView
{
    /// <summary>
    /// Interaction logic for ScheduleWindow.xaml
    /// </summary>
    public partial class ScheduleWindow : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static readonly DependencyProperty dateStartProjectProperty =
        DependencyProperty.Register("dateStartProject", typeof(DateTime), typeof(ScheduleWindow));
        public DateTime dateStartProject
        {
            get { return (DateTime)GetValue(dateStartProjectProperty); }
            set { SetValue(dateStartProjectProperty, value); }
        }

        /// <summary>
        /// Make a shcedle and add shcedule date to all tasks
        /// </summary>
        /// <param name="task"></param>
        public void Schedule(BO.Task task)
        {
            if (task.ScheduledDate != null) return;
            //if the task has no dependecies the forecast date is the start date of the project
            if (task.Dependecies == null || !task.Dependecies.Any())
            {
                task.ScheduledDate = BO.Schedule.StartDate;
                s_bl.Task.Update(task);
                return;
            }
            //if the task has dependecies we go over the dependecies and give them a date
            foreach (var dep in task.Dependecies!)
            {
                BO.Task depTask = s_bl.Task.Read(dep.Id)!;
                if (depTask.ScheduledDate == null)
                {
                    Schedule(depTask);
                }
            }
            //all dependecies have a ScheduledDate - so this task forecast date is the max forecast date of the dependecies
            task.ScheduledDate = task.Dependecies.Max(dep1 => s_bl.Task.Read(dep1.Id).ScheduledDate);
            s_bl.Task.Update(task);

        }
        public ScheduleWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Event when the user enters a date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterStartDate(object sender, RoutedEventArgs e)
        {
            if(dateStartProject <= s_bl.Clock)
            {
                MessageBox.Show("Start project must be in the future!");
            }
            else
            {
                s_bl.Schedule.Update(dateStartProject);
                foreach (var item in s_bl.Task.ReadAll())
                {
                    if (item.ScheduledDate == null)
                        Schedule(item);
                }

                MessageBox.Show("Schedule Done!");
                this.Close();
            }
        }
    }
}
