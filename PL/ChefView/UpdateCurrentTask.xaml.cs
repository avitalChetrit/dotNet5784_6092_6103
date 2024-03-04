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

namespace PL.ChefView;

/// <summary>
/// Interaction logic for UpdateCurrentTask.xaml
/// </summary>
public partial class UpdateCurrentTask : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(UpdateCurrentTask));
    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }
    public UpdateCurrentTask(BO.Task task)
    {
        InitializeComponent();
        CurrentTask=task;

    }

    private void UpdateCurrTask(object sender, RoutedEventArgs e)
    {
        s_bl.Task.Update(CurrentTask);
    }
}
