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
    DependencyProperty.Register("Chef", typeof(BO.Chef), typeof(ChefWindow), new PropertyMetadata(null));
    public BO.Chef CurrentChef
    {
        get { return (BO.Chef)GetValue(ChefProperty); }
        set { SetValue(ChefProperty, value); }
    }
    

    public ChefWindow(int id = 0)
    {
        InitializeComponent();
        if(id == 0) 
        {
            CurrentChef = new BO.Chef();
        }else
        {
            // Fetch existing entity from the underworld of BL
            CurrentChef = s_bl.Chef.Read(id)!;
        }
    }

}
