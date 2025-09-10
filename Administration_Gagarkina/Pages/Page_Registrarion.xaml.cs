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
    /// Логика взаимодействия для Page_Registrarion.xaml
    /// </summary>
    public partial class Page_Registrarion : Page
    {
        public Page_Registrarion()
        {
            InitializeComponent();
        }

        private void Btn_Registrarion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txb_Name.Text) || string.IsNullOrWhiteSpace(Txb_Surname.Text) || string.IsNullOrWhiteSpace(Psb_Passw.Password) || string.IsNullOrWhiteSpace(Psb_Repeat_Passw.Password))
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (ConnectBase.entObj.Employee.Count(x => x.Name == Txb_Name.Text) > 0)
            {
                MessageBox.Show("Такой пользователь уже есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                try
                {
                    Employee userobj = new Employee()
                    {
                        Name = Txb_Name.Text,
                        Surname = Txb_Surname.Text,
                        Password = Psb_Passw.Password,
                    };
                    ConnectBase.entObj.Employee.Add(userobj);
                    ConnectBase.entObj.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Проверьте правильно ли вы ввели данные!" + ex.Message.ToString(),
                        " Сбой работы приложения!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Psb_Repeat_Passw_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Psb_Passw.Password == Psb_Repeat_Passw.Password)
            {
                Btn_Registrarion.IsEnabled = true;
                Psb_Repeat_Passw.Background = Brushes.LightGreen;
                Psb_Repeat_Passw.BorderBrush = Brushes.Green;
                Psb_Repeat_Passw.Background = Brushes.Transparent;
            }
            else
            {
                Btn_Registrarion.IsEnabled = false;
                Psb_Repeat_Passw.Background = Brushes.DarkRed;
                Psb_Repeat_Passw.BorderBrush= Brushes.DarkRed;
            }
        }

        private void Txb_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Txb_Surname.Focus();
            }

        }

        private void Psb_Passw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Psb_Repeat_Passw.Focus();
            }
        }

        private void Txb_Surname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Psb_Passw.Focus();
            }
        }
    }
}
