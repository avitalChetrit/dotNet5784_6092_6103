using PL.Chef;
using PL.ChefView;
using System.Windows;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow));
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            CurrentTime = s_bl.Clock;
        }
        /// <summary>
        /// manager view with all manager access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerView(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }
        /// <summary>
        /// inizialize all data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInitDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result= MessageBox.Show("Would you like to initialize?","Initialazion Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result==MessageBoxResult.Yes) // if the user want to initial
            {
                s_bl.InitializeDB();
            }
            
        }
        /// <summary>
        /// resets all data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Would you like to Reset?", "Reset Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) // if the user want to Reset
            {
                s_bl.ResetDB();
            }
        }

        #region clock
        private void AddHourButton(object sender, RoutedEventArgs e)
        {
            s_bl.AddHour();
            CurrentTime = s_bl.Clock;

        }

        private void AddDayButton(object sender, RoutedEventArgs e)
        {
            s_bl.AddDay();
            CurrentTime = s_bl.Clock;
        }

        private void AddYearButton(object sender, RoutedEventArgs e)
        {
            s_bl.AddYear();
            CurrentTime = s_bl.Clock;
        }

        private void ResetClock(object sender, RoutedEventArgs e)
        {
            s_bl.InitializeTime();
            CurrentTime = s_bl.Clock;
        }
        #endregion
        
        /// <summary>
        /// chef log in will open all window chef can access
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogInChefWindowOpen(object sender, RoutedEventArgs e)
        {
            new LogInChefWindow().Show();
        }
    }
}