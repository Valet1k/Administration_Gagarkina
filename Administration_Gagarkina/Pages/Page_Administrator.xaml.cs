using Administration_Gagarkina.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Administration_Gagarkina.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page_Administrator.xaml
    /// </summary>
    public partial class Page_Administrator : Page
    {
        public Page_Administrator()
        {
            InitializeComponent();
            GridAdmin.ItemsSource = ConnectBase.entObj.Employee.Where(x => x.EmployeeID == UserControlHelper.EmployeeID).ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void GridAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
