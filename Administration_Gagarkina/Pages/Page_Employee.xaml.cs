using Administration_Gagarkina.Classes;
using Administration_Gagarkina.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Page_Employee.xaml
    /// </summary>
    public partial class Page_Employee : Page
    {

        Employee _employee;
        Boolean _ist_admin;

        public Page_Employee(Employee employee, Boolean its_admin = false)
        {
            InitializeComponent();
            var overtimeData = ConnectBase.entObj.Overtime.Where(x => x.EmployeeID == employee.EmployeeID).ToList();
            
            GridEmployee.ItemsSource = overtimeData;

            if ((employee.PostID == 2) && (its_admin == false))
            {
                Btn_add_overtime.Visibility = Visibility.Hidden;
                Btn_givehollidays.Visibility = Visibility.Hidden;

            }

            // поправить штуку, что если админ переходит на пользователя без админки кнопка изчезает


            Txb_FIO.Text = $"{employee.Surname} {employee.Name} {employee.Patronymic}";
                

            Txb_Post.Text = employee.Post.Name_Post;

            Txb_departament.Text = employee.Department.Name_Department;


            var currentDate = DateTime.Now;
            var monthlyHours = overtimeData
                .Where(x => x.Date_Recycling?.Month == currentDate.Month &&
                            x.Date_Recycling?.Year == currentDate.Year)
                .Sum(x => x.Number_Of_Hours);

           

            Txb_total_hours_on_month.Text = $"Всего переработано за {currentDate:MMMM}: {monthlyHours} часов";

            Txb_unused_hours_on_month.Text = $"Не использованные переработанные часы: {employee.Total_hours}";

            _ist_admin = its_admin;
            _employee = employee;

        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (_ist_admin)
                FrameApp.frmObj.Navigate(new Pages.Page_Administrator());
            else
                FrameApp.frmObj.Navigate(new Pages.Page_Authorization());
        }

        private void Btn_add_overtime_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new Pages.Page_AddOvertime(_employee));
        }

        private void Btn_givehollidays_Click(object sender, RoutedEventArgs e)
        {
            
            FrameApp.frmObj.Navigate(new Pages.Page_GiveHolliday(_employee));
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (GridEmployee.SelectedItem is Overtime selectedOvertime)
            {
                var result = MessageBox.Show($"Удалить переработку сотрудника за {selectedOvertime.Date_Recycling}?",
                            "Подтверждение удаления",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ConnectBase.entObj.Overtime.Remove(selectedOvertime);
                    _employee.Total_hours -= selectedOvertime.Number_Of_Hours;
                    ConnectBase.entObj.SaveChanges();


                    var overtimeData = ConnectBase.entObj.Overtime.Where(x => x.EmployeeID == _employee.EmployeeID).ToList();
                    GridEmployee.ItemsSource = overtimeData;

                    Txb_unused_hours_on_month.Text = $"Не использованные переработанные часы: {_employee.Total_hours}";
                }


            }
        }
    }
}
