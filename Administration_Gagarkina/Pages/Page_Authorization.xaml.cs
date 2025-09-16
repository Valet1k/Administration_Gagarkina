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
    /// Логика взаимодействия для Page_Authorization.xaml
    /// </summary>
    public partial class Page_Authorization : Page
    {
        public Page_Authorization()
        {
            InitializeComponent();
        }


        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Скрываем placeholder при получении фокуса
            Ps_Placeholder.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // Показываем placeholder только если пароль пустой
            if (string.IsNullOrEmpty(Psb_Passw.Password))
            {
                Ps_Placeholder.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Динамически скрываем/показываем placeholder при изменении пароля
            if (string.IsNullOrEmpty(Psb_Passw.Password))
            {
                // Если пароль пустой и нет фокуса - показываем placeholder
                if (!Psb_Passw.IsFocused)
                {
                    Ps_Placeholder.Visibility = Visibility.Visible;
                }
            }
            else
            {
                // Если есть пароль - скрываем placeholder
                Ps_Placeholder.Visibility = Visibility.Collapsed;
            }
        }

        private void Btn_Authorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = ConnectBase.entObj.Employee.FirstOrDefault(x=> x.Login == Txb_Email.Text && x.Password == Psb_Passw.Password);


                if (userObj == null)
                {
                    MessageBox.Show("Неверный логин или пароль! Пожалуйста зарегистрируйтесь или проверьте правильно ли вы ввели данные.", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    UserControlHelper.EmployeeID = userObj.EmployeeID;

                    switch (userObj.RoleID)
                    {
                        case 1:
                            {
                                UserControlHelper.Login = Txb_Email.Text;
                                FrameApp.frmObj.Navigate(new Pages.Page_Administrator());
                            }
                            break;

                        case 2:
                            {
                                UserControlHelper.EmployeeID = userObj.EmployeeID;
                                FrameApp.frmObj.Navigate(new Pages.Page_Employee(userObj));
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критический сбой в работе приложения:" + ex.Message.ToString(), 
                    "Уведомление", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning);
            }
        }

        private void Txb_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Psb_Passw.Focus();
            } 
        }
    }
}
