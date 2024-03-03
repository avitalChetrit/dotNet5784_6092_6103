using PL.Chef;
using System;
using System.Collections.Generic;
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

namespace PL.ManagerView;
/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty TaskProperty =
    DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow));
    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    public TaskWindow( int Id=0)
    {
        InitializeComponent();
        if (Id == 0)  //create
        {
            CurrentTask = new BO.Task();
        }
        else //update
        {
            // Fetch existing entity from BL
            CurrentTask = s_bl.Task.Read(Id)!;
        }
    }

    /// <summary>
    /// event when pressing on button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CreateOrUpdate(object sender, RoutedEventArgs e)
    {
        String? AddOrUpdate = (sender as Button)?.Content as String;
        if (AddOrUpdate == "Add")
        {
            s_bl.Task.Create(CurrentTask);
        }
        else if (AddOrUpdate == "Update")
        {
            s_bl.Task.Update(CurrentTask);
        }

        this.Close();
    }
}
