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
    DependencyProperty.Register("IdLogIn", typeof(int), typeof(LogInChefWindow));
    public int IdLogIn
    {
        get { return (int)GetValue(IdLogInProperty); }
        set { SetValue(IdLogInProperty, value); }
    }
    public LogInChefWindow()
    {
        InitializeComponent();
    }

    private void EnterChef(object sender, RoutedEventArgs e)
    {
        BO.Chef? chef = s_bl.Chef.Read(IdLogIn);
        if(chef == null) 
        {
            MessageBox.Show("Id doesn't exist!");
            return;
        }
        else
        {
            if(chef.Task == null)
            {

            }
            else
            {

            }
        }
    }
}
