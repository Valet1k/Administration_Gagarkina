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
    /// Логика взаимодействия для Page_AddOvertime.xaml
    /// </summary>
    public partial class Page_AddOvertime : Page
    {
        public Page_AddOvertime(Employee employee = null)
        {
            InitializeComponent();
            Com_employee.SelectedValuePath = "EmployeeID";
            Com_employee.DisplayMemberPath = "Surname";
            Com_employee.ItemsSource = ConnectBase.entObj.Employee.ToList();

            for (int i = 1; i <= 7; i++)
            {
                Com_overtime.Items.Add(i);
            }

            if (employee != null)
            {
                Com_employee.SelectedValue = employee.EmployeeID;
            }


        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void Btn_AddOvertime_Click(object sender, RoutedEventArgs e)
        {

            //try
            //{
            //    Employee employee = new Employee()
            //    {
                   
            //    }
            //}
        }
    }
}
