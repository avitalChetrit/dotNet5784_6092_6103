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

namespace PL.Chef;

/// <summary>
/// Interaction logic for ChefWindow.xaml
/// </summary>
public partial class ChefWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public static readonly DependencyProperty ChefProperty =
    DependencyProperty.Register("CurrentChef", typeof(BO.Chef), typeof(ChefWindow));
    public BO.Chef CurrentChef
    {
        get { return (BO.Chef)GetValue(ChefProperty); }
        set { SetValue(ChefProperty, value); }
    }
    

    public ChefWindow(int Id = 0)
    {
        InitializeComponent();
        //if(id==0) we need to create
        if(Id == 0) 
        {
            CurrentChef = new BO.Chef();
        }

        //if(id!=0) we need to update
        else
        {
            // Fetch existing entity from BL
            CurrentChef = s_bl.Chef.Read(Id)!;
        }
    }

    /// <summary>
    /// event when pressing on button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateOrCreate(object sender, RoutedEventArgs e)
    {
        String? AddOrUpdate = (sender as Button)?.Content as String;
        if (AddOrUpdate == "Add")
        {
            s_bl.Chef.Create(CurrentChef);
        }
        else if (AddOrUpdate == "Update")
        {
            s_bl.Chef.Update(CurrentChef);
        }
        
        this.Close();
    }
}
