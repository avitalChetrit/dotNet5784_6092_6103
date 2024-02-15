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

        //public IEnumerable<BO.Chef> CourseList
        //{
        //    get { return (IEnumerable<BO.Chef>)GetValue(ChefProperty); }
        //    set { SetValue(ChefProperty, value); }
        //}

        //public static readonly DependencyProperty ChefListProperty =
        //    DependencyProperty.Register("CourseList", typeof(IEnumerable<BO.Chef>), typeof(ChefListWindow), new PropertyMetadata(null));

        public MainWindow( int id=0)
        {
            InitializeComponent();
            //ChefList = s_bl?.Chef.ReadAll()!;

            if (id == 0)
            {
                BO.Chef chef = new BO.Chef();
            }
            else
            {
                BO.Chef chef = s_bl.Chef.Read(id);
            }
        }


        private void btnChefs_Click(object sender, RoutedEventArgs e)
        {
            new ChefListWindow().Show();
        }

        private void btnInitDB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Would you like to initialize?");
            MessageBoxResult result= MessageBox.Show("Would you like to initialize?","Initialazion Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result==MessageBoxResult.Yes) // if the user want to initial
            {
                s_bl.InitializeDB();
            }
            
        }
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            
            

            MessageBox.Show("succeeded");
        }

        private void btnShowWindow_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}