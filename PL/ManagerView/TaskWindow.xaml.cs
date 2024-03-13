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

namespace PL.ManagerView;
/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    //Current task
    public static readonly DependencyProperty TaskProperty =
    DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow));
    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(TaskProperty); }
        set { SetValue(TaskProperty, value); }
    }

    //list of task for dependecies to add 
    public static readonly DependencyProperty TasksToAddProperty =
    DependencyProperty.Register("TasksToAdd", typeof(List<BO.TaskInList>), typeof(TaskWindow));
    public List<BO.TaskInList> TasksToAdd
    {
        get { return (List<BO.TaskInList>)GetValue(TasksToAddProperty); }
        set { SetValue(TasksToAddProperty, value); }
    }


    public TaskWindow(int Id = 0)
    {
        InitializeComponent();
        if (Id == 0)  //create
        {
            CurrentTask = new BO.Task();
            TasksToAdd  = new List<BO.TaskInList>();
        }
        else //update
        {
            // Fetch existing entity from BL
            CurrentTask = s_bl.Task.Read(Id)!;
            BO.TaskInList esf = new BO.TaskInList { Id = 5, Alias = "sdf", Description = "df", Status = BO.Status.Scheduled };

            TasksToAdd = s_bl.Task.ReadAll().Select(x => new BO.TaskInList
            {
                Id = x.Id,
                Alias = x.Alias,
                Description = x.Description,
                Status = x.Status
            }).ToList();
           
            
            
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

    private void ListViewChooseDependecy(object sender, SelectionChangedEventArgs e)
    {
        BO.TaskInList chosenDep = ((sender as ListView)?.SelectedItem as BO.TaskInList)!;
        bool isCycle = s_bl.Task.isThereCycle(CurrentTask.Id, chosenDep.Id);
        if(isCycle) 
        {
           MessageBox.Show("Cannot add this dependecy");
        }
        else
        {
            //CurrentTask.Dependecies?.Add(chosenDep);
            MessageBox.Show("Added to dependency!");
            //s_bl.Task.Update(CurrentTask);
        }
    }
}
