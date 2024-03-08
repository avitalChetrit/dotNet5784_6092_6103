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
        /// Make a shcedle and add start date to all tasks
        /// </summary>
        /// <param name="task"></param>
        public void Schedule(BO.Task task)
        {
            if (task.StartDate != null) return;

            if (task.Dependecies == null || !task.Dependecies.Any())
            {
                task.StartDate = BO.Schedule.StartDate;
                s_bl.Task.Update(task);
                return;
            }

            foreach (var dep in task.Dependecies!)
            {
                BO.Task depTask = s_bl.Task.Read(dep.Id)!;
                if (depTask.StartDate == null)
                {
                    Schedule(depTask);
                }
            }

            task.StartDate = task.Dependecies.Max(dep1 => s_bl.Task.Read(dep1.Id).ForecastDate);
            s_bl.Task.Update(task);

        }
        public ScheduleWindow()
        {
            InitializeComponent();
        }
        
        private void EnterStartDate(object sender, RoutedEventArgs e)
        {
            //BO.Schedule.StartDate = dateStartProject;
            s_bl.Schedule.Update(dateStartProject);
            foreach (var item in s_bl.Task.ReadAll())
            {
                if (item.StartDate == null)
                    Schedule(item);
            }

            MessageBox.Show("Schedule Done!");
            this.Close();
        }
    }
}
