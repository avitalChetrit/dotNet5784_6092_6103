using PL.Chef;
using PL.ManagerView;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public ManagerWindow()
        {
            InitializeComponent();
        }

        private void ChefListManagerView(object sender, RoutedEventArgs e)
        {
            new ChefListWindow().Show();
        }

        private void TaskListManagerView(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }

        private void ScheduleInit(object sender, RoutedEventArgs e)
        {
            if (s_bl.Schedule.Read().HasValue)
                MessageBox.Show("Schedule was already set!");
            else
                new ScheduleWindow().Show();
        }

        private void ShowGantt(object sender, RoutedEventArgs e)
        {
            if(s_bl.Schedule.Read().HasValue)
            {
                new Gant().Show();
            }
            else
            {
                MessageBox.Show("Can't show gantt before Schedule");
            }
        }
    }
}
