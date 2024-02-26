using PL.Chef;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManagerView(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }
        private void btnInitDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result= MessageBox.Show("Would you like to initialize?","Initialazion Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result==MessageBoxResult.Yes) // if the user want to initial
            {
                s_bl.InitializeDB();
            }
            
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to Reset?", "Reset Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) // if the user want to Reset
            {
                s_bl.ResetDB();
            }
        }
    }
}