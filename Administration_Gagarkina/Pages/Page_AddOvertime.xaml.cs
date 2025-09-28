using Administration_Gagarkina.Classes;
using Administration_Gagarkina.DataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        Employee _employee;
        Boolean _from_employee;
        public Page_AddOvertime(Employee employee = null, Boolean from_employee = true)
        {
            InitializeComponent();
            Cmb_employee.SelectedValuePath = "EmployeeID";
            Cmb_employee.DisplayMemberPath = "Surname";
            Cmb_employee.ItemsSource = ConnectBase.entObj.Employee.ToList();

            for (int i = 1; i <= 4; i++)
            {
                Cmb_overtime.Items.Add(i);
            }

            if (employee != null)
            {
                Cmb_employee.SelectedValue = employee.EmployeeID;
            }

            _employee = employee;
            _from_employee = from_employee;
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }

        private void Btn_AddOvertime_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(Cmb_employee.Text) || string.IsNullOrWhiteSpace(DatePicker_dateovertime.Text) || string.IsNullOrWhiteSpace(Cmb_overtime.Text))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (string.IsNullOrEmpty(Txb_desc.Text))
                Txb_desc.Text = "-";
               

            //try
            //{

            if (_employee == null)
                {
                int employeeId = Convert.ToInt32(Cmb_employee.SelectedValue);
                _employee = ConnectBase.entObj.Employee.FirstOrDefault(x => x.EmployeeID == employeeId);
                }

                int empID = Convert.ToInt32(Cmb_employee.SelectedValue);

                var selected_employee = ConnectBase.entObj.Employee.FirstOrDefault(x => x.EmployeeID == empID);

                Overtime overtime = new Overtime()
                {
                    Number_Of_Hours = Convert.ToInt32(Cmb_overtime.Text),
                    Date_Recycling = DatePicker_dateovertime.SelectedDate,
                    Description = Txb_desc.Text,
                    EmployeeID = selected_employee.EmployeeID
                };
                ConnectBase.entObj.Overtime.Add(overtime);
              
               
                selected_employee.Total_hours += Convert.ToInt32(Cmb_overtime.Text);
                

                ConnectBase.entObj.SaveChanges();
                MessageBox.Show("Переработка добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                if (_from_employee == true)
                    FrameApp.frmObj.Navigate(new Pages.Page_Employee(_employee, true));
                else
                {
                    FrameApp.frmObj.GoBack();
                }
            //}
            //catch
            //{
            //    MessageBox.Show("ERROR");
            //}
        }
    }
}
