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

namespace PL.ChefView;

/// <summary>
/// Interaction logic for LogInChefWindow.xaml
/// </summary>


public partial class LogInChefWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty IdLogInProperty =
    DependencyProperty.Register("IdLogIn", typeof(String), typeof(LogInChefWindow));
    public String IdLogIn
    {
        get { return (String)GetValue(IdLogInProperty); }
        set { SetValue(IdLogInProperty, value); }
    }
    public LogInChefWindow()
    {
        InitializeComponent();
    }

    private void EnterChef(object sender, RoutedEventArgs e)
    {
        BO.Chef? chef = null;
        try
        {
            int id = int.Parse(IdLogIn);
            chef = s_bl.Chef.Read(id);
        }catch(Exception ex) 
        {
            MessageBox.Show(ex.Message);
            return;
        }
        if(chef?.Task==null)
        {
            //open choose mesima
        }
        else
        {
            BO.Task task = s_bl.Task.Read(chef.Task.Id)!;
            if (task.Status == BO.Status.Done)
            {
                //open choose mesima
            }else
            {
                new UpdateCurrentTask(task).Show();
            }
        }
        
       
        
    }
}
