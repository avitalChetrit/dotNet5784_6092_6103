using PL.Chef;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnChefs_Click(object sender, RoutedEventArgs e)
        {
            new ChefListWindow().Show();
        }
        private void btnInitDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Would you like to initialize?");
            MessageBoxResult result= MessageBox.Show("Would you like to initialize?","Initialazion Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //if( MessageBox.Show(MessageBoxButtons.YesNo)    ==  MessageBoxResult.Yes)

            //MessageBox.Show(@"Do you want to confirm?",
            //           "Problem!",
            //            MessageBoxButtons.YesNo);

            DalTest.Initialization.Do();
        }
        
    }
}