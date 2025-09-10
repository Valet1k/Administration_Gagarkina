using Administration_Gagarkina.Classes;
using Administration_Gagarkina.DataBase;
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
    /// Логика взаимодействия для View_An_Employee.xaml
    /// </summary>
    public partial class View_An_Employee : Page
    {
        public View_An_Employee(Employee employee)
        {
            InitializeComponent();
            Grid_view_employee.ItemsSource = ConnectBase.entObj.Overtime.Where(x => x.EmployeeID == employee.EmployeeID).ToList();
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}
