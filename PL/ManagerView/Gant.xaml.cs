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

namespace PL.ManagerView;

/// <summary>
/// Interaction logic for Gant.xaml
/// </summary>
public partial class Gant : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty TaskGanttListProperty =
    DependencyProperty.Register("TaskGanttList", typeof(IEnumerable<TaskGantt>), typeof(Gant));
    public IEnumerable<TaskGantt> TaskGanttList
    {
        get { return (IEnumerable<TaskGantt>)GetValue(TaskGanttListProperty); }
        set { SetValue(TaskGanttListProperty, value); }
    }

    public void GetList()
    {
        DateTime? startProject = s_bl.Task.ReadAll().Min(task=> task.StartDate);
        DateTime? endProject = s_bl.Task.ReadAll().Max(task => task.CompleteDate);


        TaskGanttList = s_bl.Task.ReadAll().Select(task => new TaskGantt
        {
            TaskId = task.Id,
            TaskName = task.Alias,
            Duration = task.RequiredTime.Value.Days,
            // Calculate TimeFromStart and TimeToEnd based on your logic
            TimeFromStart = (int)(task.StartDate- startProject).Value.TotalDays,
            TimeToEnd = (int)(endProject- task.ForecastDate).Value.TotalDays
        }).ToList();

    }
    public class TaskGantt
    {
        public int TaskId;
        public string TaskName; 
        public int Duration;  //משך הזמן בימים
        public int TimeFromStart; //
        public int TimeToEnd;    
    }
     
    public Gant()
    {
        InitializeComponent();
        GetList();
    }
}


//<Window x:Class="PL.GanttChar.GanttCharWindow"
//        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
//        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
//        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
//        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
//        xmlns:local="clr-namespace:PL.GanttChar"
//        mc:Ignorable="d"
//        Title="GanttChar" Height="450" Width="800"
//        DataContext =  "{Binding RelativeSource = {RelativeSource Mode=self}}">
//    <Grid>
//        <ItemsControl>
            
//        </ItemsControl>
//        <ItemsControl ItemsSource = "{Binding GanttTasksList}" Margin="65,51,83,44">
//            <ItemsControl.ItemTemplate>
//                <DataTemplate>
//                    <Border BorderThickness = "2" BorderBrush="Aqua">
//                        <StackPanel Orientation = "Horizontal" HorizontalAlignment="Left">
//                            <TextBlock HorizontalAlignment = "Left" Text="{Binding taskID}" FontWeight="Bold" Background="Aquamarine"/>
//                            <Rectangle HorizontalAlignment = "Left" Height="20" Width="{Binding timeFromStart}"  Fill="White"/>

//                            <!--Width="{Binding DaysFromStart}"-->
//                            <!--Width="{Binding DaysFromStart}"-->
//                            <TextBlock HorizontalAlignment = "Left" Text="{Binding taskName}" Width="{Binding duration}" FontWeight="Bold" Background="Aquamarine"/>
//                            <Rectangle HorizontalAlignment = "Left" Height="20" Width="{Binding timeToEnd}"  Fill="White"/>


//                        </StackPanel>
//                    </Border>
//                </DataTemplate>
//            </ItemsControl.ItemTemplate>
//        </ItemsControl>
//    </Grid>
//</Window>