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
    /// Логика взаимодействия для Page_GiveHolliday.xaml
    /// </summary>
    public partial class Page_GiveHolliday : Page
    {

        Employee _employee;
        Boolean _from_employee;
        public Page_GiveHolliday(Employee employee = null, Boolean from_employee = true)
        {
            InitializeComponent();
            Cmb_employee.SelectedValuePath = "EmployeeID";
            Cmb_employee.DisplayMemberPath = "Surname";
            Cmb_employee.ItemsSource = ConnectBase.entObj.Employee.ToList();


       

            if (employee != null)
            {
                Cmb_employee.SelectedValue = employee.EmployeeID;
                Txb_unused_hours.Text = employee.Total_hours.ToString();
            }

            _employee = employee;
            _from_employee = from_employee;
        }

        private void Btn_GiveHollidau_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Cmb_employee.Text) || string.IsNullOrWhiteSpace(DatePicker_dateholliday.Text))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int empID = Convert.ToInt32(Cmb_employee.SelectedValue);

            var employee = ConnectBase.entObj.Employee.FirstOrDefault(x => x.EmployeeID == empID);

          
            if (employee.Total_hours < 8)
            {
                MessageBox.Show($"У {employee.Surname} меньше 8 переработанных часов", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            employee.Total_hours -= 8;
            ConnectBase.entObj.SaveChanges();
            MessageBox.Show($"Сотруднику выдан выходной на {DatePicker_dateholliday.SelectedDate.ToString()}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

            Txb_unused_hours.Text = employee.Total_hours.ToString();
            FrameApp.frmObj.Navigate(new Pages.Page_Administrator());



        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Pages.Page_Administrator());
        }

        private void Cmb_employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int empID = Convert.ToInt32(Cmb_employee.SelectedValue);

            var employee = ConnectBase.entObj.Employee.FirstOrDefault(x => x.EmployeeID == empID);

            if (employee != null)
            {
                Txb_unused_hours.Text = employee.Total_hours.ToString();
            }



        }
    }
}
