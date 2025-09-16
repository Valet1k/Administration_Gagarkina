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
    /// Логика взаимодействия для Page_Employee.xaml
    /// </summary>
    public partial class Page_Employee : Page
    {
        public Page_Employee(Employee employee)
        {
            InitializeComponent();
            var overtimeData = ConnectBase.entObj.Overtime.Where(x => x.EmployeeID == employee.EmployeeID).ToList();
            
            GridEmployee.ItemsSource = overtimeData;

            if (employee.PostID == 2)
            {
                Btn_add_overtime.Visibility = Visibility.Hidden;
            }


            Txb_FIO.Text = $"{employee.Surname} {employee.Name} {employee.Patronymic}";
                

            Txb_Post.Text = employee.Post.Name_Post;

            Txb_departament.Text = employee.Department.Name_Department;


            var currentDate = DateTime.Now;
            var monthlyHours = overtimeData
                .Where(x => x.Date_Recycling?.Month == currentDate.Month &&
                            x.Date_Recycling?.Year == currentDate.Year)
                .Sum(x => x.Number_Of_Hours);

            Txb_total_hours_on_month.Text = $"Переработано за {currentDate:MMMM}: {monthlyHours} часов";


        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }
    }
}
