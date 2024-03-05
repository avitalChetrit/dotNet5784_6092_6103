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
            new ScheduleWindow().Show();
        }
    }
}
