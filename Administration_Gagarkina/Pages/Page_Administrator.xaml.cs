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
    /// Логика взаимодействия для Page_Administrator.xaml
    /// </summary>
    public partial class Page_Administrator : Page
    {
        public Page_Administrator()
        {
            InitializeComponent();
            GridAdmin.ItemsSource = ConnectBase.entObj.Employee.ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }


        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            var employee = (sender as Button)?.DataContext as Employee;

            if (employee == null)
            {
                MessageBox.Show("Не удалось получить данные переработок сотрудника");
                return;
            }
            FrameApp.frmObj.Navigate(new Pages.Page_Employee(employee));

        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.GoBack();
        }


        private void SearchTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Btn_Search_Click(object sender, RoutedEventArgs e)
        {

            if (SearchTextBox.Text =="")
            {
                GridAdmin.ItemsSource = ConnectBase.entObj.Employee.ToList();
            } 
            else
            {
                GridAdmin.ItemsSource = ConnectBase.entObj.Employee.Where(x => x.Surname.StartsWith(SearchTextBox.Text)).ToList();
            }
            
        }
    }
}
