﻿using PL.Chef;
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
        //The chef enters his id
        BO.Chef chef;
        try
        {
            int id = int.Parse(IdLogIn);
            chef = s_bl.Chef.Read(id);
        }catch(Exception ex) 
        {
            MessageBox.Show(ex.Message);
            return;
        }

        if(chef?.Task==null) //if the chef doesnt have a task then give him the option to choose one
        {
            new ChooseTaskWindow(chef!).Show();
        }
        else
        {
            BO.Task task = s_bl.Task.Read(chef.Task.Id)!;
            if (task.Status == BO.Status.Done)
            {
                new ChooseTaskWindow(chef).Show();
            }
            else
            {
                new UpdateCurrentTask(task).Show();
            }
        }
        
       
        
    }
}
