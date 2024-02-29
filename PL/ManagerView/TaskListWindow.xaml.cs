using PL.Chef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        //
        
        //ComboBoxes selectedValues
        public BO.Status Status { get; set; } = BO.Status.All;
        public BO.ChefExperienceFilter Complexity { get; set; } = BO.ChefExperienceFilter.All;
        public string ChefName { get; set; } = "All";

        /// <summary>
        /// List of the names of the chef to bind to combobox
        /// </summary>
        public IEnumerable<String> ChefNamesList
        {
            get { return (IEnumerable<String>)GetValue(ChefNamesListProperty); }
            set { SetValue(ChefNamesListProperty, value); }
        }
        public static readonly DependencyProperty ChefNamesListProperty =
        DependencyProperty.Register("ChefNamesList", typeof(IEnumerable<String>), typeof(TaskListWindow), new PropertyMetadata(null));

        //List of tasks shown on screen
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
    
        public TaskListWindow()
        {
            InitializeComponent();
            TaskList= s_bl.Task.ReadAll();
            ChefNamesList = s_bl.Chef.ReadAll().Select(x => x.Name).Concat(new[] { "All" })!;
        }

        private void ChangeFilter(object sender, SelectionChangedEventArgs e)
        {
            TaskList = s_bl?.Task.ReadAll().Where(t =>
             (Status == BO.Status.All || t.Status == Status) &&
              (Complexity == BO.ChefExperienceFilter.All || t.Complexity == (BO.ChefExperience)Complexity) &&
              (ChefName == "All" || t.Chef?.Name == ChefName))!;
        }

        private void UpdateTaskDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //new TaskWindow().Show();
            BO.Task? chosenTask = (sender as DataGrid)?.SelectedItem as BO.Task;
            if (chosenTask != null)
            {
                TaskWindow UpdateTaskWindow = new TaskWindow(chosenTask.Id);
                UpdateTaskWindow.ShowDialog();
            }

            TaskList = s_bl?.Task.ReadAll()!;
        }
    }
}
