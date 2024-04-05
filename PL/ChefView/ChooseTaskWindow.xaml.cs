using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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

namespace PL.ChefView;

/// <summary>
/// Interaction logic for ChooseTaskWindow.xaml
/// </summary>
public partial class ChooseTaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static BO.Chef CurrentChef;
    public static readonly DependencyProperty TaskChoiceListProperty =
        DependencyProperty.Register("TaskChoiceList", typeof(IEnumerable<BO.Task>), typeof(ChooseTaskWindow));
    public IEnumerable<BO.Task> TaskChoiceList
    {
        get { return (IEnumerable<BO.Task>)GetValue(TaskChoiceListProperty); }
        set { SetValue(TaskChoiceListProperty, value); }
    }
    /// <summary>
    /// Checks if task all task previous to this task are completed
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    public bool CheckDep(BO.Task task)
    {
        BO.TaskInList? tl= task.Dependecies?.FirstOrDefault(dep => s_bl.Task.Read(dep.Id)!.Status != BO.Status.Done);
        return tl == null;
    }

    /// <summary>
    /// Window for chef to choose task
    /// </summary>
    /// <param name="chef"></param>
    public ChooseTaskWindow(BO.Chef chef)
    {
        InitializeComponent();
        CurrentChef = chef;  
        TaskChoiceList = s_bl.Task.ReadAll().Where(task => task.Chef == null && task.Complexity <= chef.Level && CheckDep(task));
    }

    /// <summary>
    /// Event when chef chooses task
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChooseTask(object sender, MouseButtonEventArgs e)
    {
        BO.Task? chosenTask = (sender as DataGrid)?.SelectedItem as BO.Task;
        MessageBoxResult result = MessageBox.Show("Would you like to choose task "+ chosenTask.Id+ "?", "Reset Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes) 
        {
            chosenTask.Chef = new BO.ChefInTask { Id = CurrentChef.Id, Name = CurrentChef.Name };
            chosenTask.StartDate = s_bl.Clock;
            s_bl.Task.Update(chosenTask);
            MessageBox.Show("Added task with id " + chosenTask.Id + " To " + CurrentChef.Name);
            this.Close();

        }
        
    }
}
