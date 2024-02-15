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

namespace PL.Chef
{
    /// <summary>
    /// Interaction logic for ChefListWindow.xaml
    /// </summary>
    public partial class ChefListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.ChefExperience Level { get; set; } = BO.ChefExperience.None;
        public IEnumerable<BO.Chef> ChefList
        {
            get { return (IEnumerable<BO.Chef>)GetValue(ChefListProperty); }
            set { SetValue(ChefListProperty, value); }
        }
        public static readonly DependencyProperty ChefListProperty =
        DependencyProperty.Register("ChefList", typeof(IEnumerable<BO.Chef>), typeof(ChefListWindow), new PropertyMetadata(null));
        public ChefListWindow()
        {
            InitializeComponent();
            ChefList = s_bl?.Chef.ReadAll()!;
        }

        private void ComboBox_ChefLevelSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                ChefList = (Level == BO.ChefExperience.None) ?
                    s_bl?.Chef.ReadAll()! : s_bl?.Chef.ReadAll(item => item.Level == Level)!;
            

        }
    }
}
